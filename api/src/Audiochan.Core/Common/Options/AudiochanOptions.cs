﻿using System.Collections.Generic;

namespace Audiochan.Core.Common.Options
{
    public record AudiochanOptions
    {
        public record UploadOptions
        {
            public long FileSize { get; init; }
            public List<string> FileExtensions { get; init; } = new();
        }

        public UploadOptions AudioUploadOptions { get; init; }
        public UploadOptions ImageUploadOptions { get; init; }
    }
}