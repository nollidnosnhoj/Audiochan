﻿using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Audiochan.Core.Common.Interfaces;
using Audiochan.Core.Common.Models;
using Audiochan.Core.Entities;
using Audiochan.Core.Services;
using MediatR;

namespace Audiochan.Core.Features.FavoriteAudios.SetFavoriteAudio
{
    public record SetFavoriteAudioCommand(Guid AudioId, string UserId, bool IsFavoriting) : IRequest<Result<bool>>
    {
    }
    
    public class SetFavoriteAudioCommandHandler : IRequestHandler<SetFavoriteAudioCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;

        public SetFavoriteAudioCommandHandler(IUnitOfWork unitOfWork, IDateTimeProvider dateTimeProvider)
        {
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<Result<bool>> Handle(SetFavoriteAudioCommand command, CancellationToken cancellationToken)
        {
            var audio = await _unitOfWork.Audios
                .LoadForSetFavorite(command.AudioId, command.UserId, cancellationToken);

            if (audio == null)
                return Result<bool>.NotFound<Audio>();

            var isFavoriting = command.IsFavoriting
                ? await Favorite(audio, command.UserId, cancellationToken)
                : await Unfavorite(audio, command.UserId, cancellationToken);

            _unitOfWork.Audios.Update(audio);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<bool>.Success(isFavoriting);
        }
        
        private Task<bool> Favorite(Audio target, string userId, CancellationToken cancellationToken = default)
        {
            var favoriter = target.Favorited.FirstOrDefault(f => f.UserId == userId);

            if (favoriter is null)
            {
                favoriter = new FavoriteAudio
                {
                    AudioId = target.Id,
                    UserId = userId,
                    FavoriteDate = _dateTimeProvider.Now
                };
                
                target.Favorited.Add(favoriter);
            }
            else if (favoriter.UnfavoriteDate is not null)
            {
                favoriter.FavoriteDate = _dateTimeProvider.Now;
                favoriter.UnfavoriteDate = null;
            }
            
            return Task.FromResult(true);
        }

        private Task<bool> Unfavorite(Audio target, string userId, CancellationToken cancellationToken = default)
        {
            var favoriter = target.Favorited.FirstOrDefault(f => f.UserId == userId);

            if (favoriter is not null)
            {
                favoriter.UnfavoriteDate = _dateTimeProvider.Now;
            }

            return Task.FromResult(false);
        }
    }
}