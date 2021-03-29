﻿using Audiochan.Core.Entities;

namespace Audiochan.Core.Common.Models
{
    public record MetaUserDto
    {
        public string Id { get; init; }
        public string Username { get; init; }
        public string Picture { get; init; }
    }
}