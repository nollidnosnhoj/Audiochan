﻿using Audiochan.Core.Common.Extensions;
using Audiochan.Core.Common.Settings;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace Audiochan.Core.Features.Users.UpdatePassword
{
    public class UpdatePasswordCommandValidator : AbstractValidator<UpdatePasswordCommand>
    {
        public UpdatePasswordCommandValidator(IOptions<IdentitySettings> options)
        {
            RuleFor(req => req.NewPassword)
                .NotEmpty()
                .WithMessage("New Password is required.")
                .NotEqual(req => req.CurrentPassword)
                .WithMessage("New password cannot be the same as the previous.")
                .Password(options.Value.PasswordSettings, "New Password");
        }
    }
}