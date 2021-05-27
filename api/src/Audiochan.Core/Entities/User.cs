﻿using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Audiochan.Core.Entities
{
    public sealed class User : IdentityUser
    {
        public User()
        {
            Audios = new HashSet<Audio>();
            Followings = new HashSet<FollowedUser>();
            Followers = new HashSet<FollowedUser>();
            RefreshTokens = new HashSet<RefreshToken>();
        }

        public User(string username, string email, DateTime joined) : this()
        {
            this.UserName = username;
            this.Email = email;
            this.Joined = joined;
            this.DisplayName = username;
        }

        public string DisplayName { get; set; } = null!;
        public string? Picture { get; set; }
        public string? About { get; set; }
        public string? Website { get; set; }
        public DateTime Joined { get; set; }
        public ICollection<Audio> Audios { get; set; }
        public ICollection<FollowedUser> Followings { get; set; }
        public ICollection<FollowedUser> Followers { get; set; }
        public ICollection<RefreshToken> RefreshTokens { get; set; }

        public void UpdateDisplayName(string? displayName)
        {
            if (!string.IsNullOrWhiteSpace(displayName))
            {
                if (string.Equals(this.UserName.Trim(), displayName.Trim(),
                    StringComparison.CurrentCultureIgnoreCase))
                {
                    this.DisplayName = displayName;
                }
            }
        }

        public void UpdateAbout(string? about)
        {
            if (about is not null)
                this.About = about;
        }

        public void UpdateWebsite(string? website)
        {
            if (website is not null)
                this.Website = website;
        }

        public void UpdatePicture(string picturePath)
        {
            if (!string.IsNullOrWhiteSpace(picturePath))
                this.Picture = picturePath;
        }
    }
}