﻿using System;
using Audiochan.Core.Common.Models.Responses;

namespace Audiochan.Core.Features.Audios.GetAudioList
{
    public record AudioViewModel
    {
        public Guid Id { get; init; }
        public string Title { get; init; } = null!;
        public bool IsPublic { get; init; }
        public decimal Duration { get; init; }
        public string? Picture { get; init; }
        public DateTime Uploaded { get; init; }
        public string AudioUrl { get; init; } = null!;
        public MetaAuthorDto Author { get; init; } = null!;
    }
}