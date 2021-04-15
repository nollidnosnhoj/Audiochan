﻿using Audiochan.Core.Models.Requests;
using Audiochan.Core.Models.Responses;
using Audiochan.Core.Models.ViewModels;
using MediatR;

namespace Audiochan.Features.Audios.CreateAudio
{
    public class CreateAudioRequest : AudioAbstractRequest, IRequest<Result<AudioDetailViewModel>>
    {
        public string UploadId { get; init; }
        public string FileName { get; init; }
        public long FileSize { get; init; }
        public int Duration { get; init; }
    }
}