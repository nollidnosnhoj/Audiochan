﻿using FluentValidation;

namespace Audiochan.Core.Features.Audios.UpdateAudio
{
    public class UpdateAudioCommandValidator : AbstractValidator<UpdateAudioCommand>
    {
        public UpdateAudioCommandValidator()
        {
            When(req => req.Title is not null, () =>
            {
                RuleFor(req => req.Title)
                    .NotEmpty()
                    .WithMessage("Title is required.")
                    .MaximumLength(30)
                    .WithMessage("Title cannot be no more than 30 characters long.");
            });

            When(req => req.Description is not null, () =>
            {
                RuleFor(req => req.Description)
                    .MaximumLength(500)
                    .WithMessage("Description cannot be more than 500 characters long.");
            });

            When(req => req.Tags is not null, () =>
            {
                RuleFor(req => req.Tags)
                    .Must(u => u!.Count <= 10)
                    .WithMessage("Can only have up to 10 tags per audio upload.");
            });
        }
    }
}