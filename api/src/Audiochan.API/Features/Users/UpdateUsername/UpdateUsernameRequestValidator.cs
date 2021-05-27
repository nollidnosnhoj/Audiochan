﻿using Audiochan.API.Extensions;
using Audiochan.Core.Settings;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace Audiochan.API.Features.Users.UpdateUsername
{
    public class UpdateUsernameRequestValidator : AbstractValidator<UpdateUsernameRequest>
    {
        public UpdateUsernameRequestValidator(IOptions<IdentitySettings> options)
        {
            RuleFor(req => req.NewUsername).Username(options.Value.UsernameSettings);
        }
    }
}