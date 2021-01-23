﻿using System.Collections.Generic;

namespace Audiochan.Core.Features.Users.Models
{
    /// <summary>
    /// Used to return authenticated user information.
    /// </summary>
    public class CurrentUserViewModel
    {
        public string Id { get; init; } = null!;
        public string Username { get; init; } = null!;
        public string Email { get; init; } = null!;
        public ICollection<string> Roles { get; init; } = new List<string>();
    }
}