﻿using System;
using System.Linq;
using System.Linq.Expressions;
using Audiochan.API.Features.Auth.GetCurrentUser;
using Audiochan.API.Features.Users.GetProfile;
using Audiochan.Core.Constants;
using Audiochan.Core.Entities;
using FastExpressionCompiler;

namespace Audiochan.API.Mappings
{
    public static class UserMappings
    {
        public static ProfileViewModel MapToProfile(this User user, string userId, bool returnNullIfFail = false) =>
            ProfileProjection(userId).CompileFast(returnNullIfFail).Invoke(user);

        public static Expression<Func<User, CurrentUserViewModel>> CurrentUserProjection()
        {
            return user => new CurrentUserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                Username = user.UserName
            };
        }

        public static Expression<Func<User, ProfileViewModel>> ProfileProjection(string userId)
        {
            return user => new ProfileViewModel
            {
                Id = user.Id,
                Username = user.UserName,
                About = user.About ?? "",
                Picture = user.Picture != null
                    ? string.Format(MediaLinkInvariants.UserPictureUrl, user.Picture)
                    : null,
                Website = user.Website ?? "",
                AudioCount = user.Audios.Count,
                FollowerCount = user.Followers.Count,
                FollowingCount = user.Followings.Count,
                IsFollowing = userId.Length > 0
                    ? user.Followers.Any(f => f.ObserverId == userId)
                    : null
            };
        }
    }
}