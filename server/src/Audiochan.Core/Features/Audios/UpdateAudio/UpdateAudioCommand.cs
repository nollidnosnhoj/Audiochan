﻿using System.Linq;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Audiochan.Core.Common.Enums;
using Audiochan.Core.Common.Models.Requests;
using Audiochan.Core.Common.Models.Responses;
using Audiochan.Core.Common.Options;
using Audiochan.Core.Features.Audios.GetAudio;
using Audiochan.Core.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Audiochan.Core.Features.Audios.UpdateAudio
{
    public record UpdateAudioCommand : AudioCommandRequest, IRequest<Result<AudioDetailViewModel>>
    {
        [JsonIgnore] public long Id { get; init; }
    }

    public class UpdateAudioCommandValidator : AbstractValidator<UpdateAudioCommand>
    {
        public UpdateAudioCommandValidator()
        {
            Include(new AudioCommandValidator());
        }
    }

    public class UpdateAudioCommandHandler : IRequestHandler<UpdateAudioCommand, Result<AudioDetailViewModel>>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly ICurrentUserService _currentUserService;
        private readonly ITagRepository _tagRepository;
        private readonly AudiochanOptions _audiochanOptions;

        public UpdateAudioCommandHandler(IApplicationDbContext dbContext,
            ICurrentUserService currentUserService,
            ITagRepository tagRepository,
            IOptions<AudiochanOptions> options)
        {
            _dbContext = dbContext;
            _currentUserService = currentUserService;
            _tagRepository = tagRepository;
            _audiochanOptions = options.Value;
        }

        public async Task<Result<AudioDetailViewModel>> Handle(UpdateAudioCommand request,
            CancellationToken cancellationToken)
        {
            var currentUserId = await _dbContext.Users
                .Select(u => u.Id)
                .SingleOrDefaultAsync(id => id == _currentUserService.GetUserId(), cancellationToken);

            if (string.IsNullOrEmpty(currentUserId))
                return Result<AudioDetailViewModel>.Fail(ResultError.Unauthorized);

            var audio = await _dbContext.Audios
                .Include(a => a.User)
                .Include(a => a.Tags)
                .SingleOrDefaultAsync(a => a.Id == request.Id, cancellationToken);

            if (audio == null)
                return Result<AudioDetailViewModel>.Fail(ResultError.NotFound);

            if (!audio.CanModify(currentUserId))
                return Result<AudioDetailViewModel>.Fail(ResultError.Forbidden);
            
            if (request.Tags.Count > 0)
            {
                var newTags = await _tagRepository.CreateTags(request.Tags, cancellationToken);

                audio.UpdateTags(newTags);
            }

            audio.UpdateTitle(request.Title);
            audio.UpdateDescription(request.Description);
            audio.UpdatePublicityStatus(request.Publicity);
            
            if (audio.Visibility == Visibility.Private)
                audio.SetPrivateKey();
            else
                audio.ClearPrivateKey();

            _dbContext.Audios.Update(audio);
            await _dbContext.SaveChangesAsync(cancellationToken);

            var viewModel = AudioDetailViewModel.MapFrom(audio, _audiochanOptions);

            return Result<AudioDetailViewModel>.Success(viewModel);
        }
    }
}