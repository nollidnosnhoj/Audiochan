﻿using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Audiochan.Core.Common.Helpers;
using Audiochan.Core.Common.Models.Responses;
using Audiochan.Core.Common.Options;
using Audiochan.Core.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Audiochan.Core.Features.Audios.UpdatePicture
{
    public class UpdateAudioPictureRequestHandler : IRequestHandler<UpdateAudioPictureRequest, IResult<string>>
    {
        private readonly AudiochanOptions.StorageOptions _pictureStorageOptions;
        private readonly IApplicationDbContext _dbContext;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IStorageService _storageService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IImageService _imageService;

        public UpdateAudioPictureRequestHandler(IOptions<AudiochanOptions> options,
            IApplicationDbContext dbContext,
            IStorageService storageService,
            ICurrentUserService currentUserService,
            IImageService imageService,
            IDateTimeProvider dateTimeProvider)
        {
            _pictureStorageOptions = options.Value.ImageStorageOptions;
            _dbContext = dbContext;
            _storageService = storageService;
            _currentUserService = currentUserService;
            _imageService = imageService;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<IResult<string>> Handle(UpdateAudioPictureRequest request,
            CancellationToken cancellationToken)
        {
            var container = Path.Combine(_pictureStorageOptions.Container, "audios");
            var blobName = BlobHelpers.GetPictureBlobName(_dateTimeProvider.Now);
            try
            {
                var currentUserId = await _dbContext.Users
                    .Select(u => u.Id)
                    .SingleOrDefaultAsync(id => id == _currentUserService.GetUserId(), cancellationToken);

                if (string.IsNullOrEmpty(currentUserId))
                    return Result<string>.Fail(ResultError.Unauthorized);

                var audio = await _dbContext.Audios
                    .SingleOrDefaultAsync(a => a.Id == request.Id, cancellationToken);

                if (audio == null) return Result<string>.Fail(ResultError.NotFound);
                if (!audio.CanModify(currentUserId)) return Result<string>.Fail(ResultError.Forbidden);

                if (!string.IsNullOrEmpty(audio.Picture))
                {
                    await _storageService.RemoveAsync(audio.Picture, cancellationToken);
                    audio.UpdatePicture(string.Empty);
                }

                var image = await _imageService.UploadImage(request.ImageData, container, blobName, cancellationToken);
                audio.UpdatePicture(image.Path);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return Result<string>.Success(image.Url);
            }
            catch (Exception)
            {
                await _storageService.RemoveAsync(container, blobName, cancellationToken);
                throw;
            }
        }
    }
}