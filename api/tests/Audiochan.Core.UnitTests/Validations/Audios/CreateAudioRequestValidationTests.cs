﻿using Audiochan.API.Features.Audios.CreateAudio;
using Audiochan.Core.Settings;
using Audiochan.Tests.Common.Builders;
using Audiochan.Tests.Common.Fakers.Audios;
using FluentAssertions;
using FluentValidation;
using FluentValidation.TestHelper;
using Microsoft.Extensions.Options;
using Xunit;

namespace Audiochan.Core.UnitTests.Validations.Audios
{
    public class CreateAudioRequestValidationTests
    {
        private readonly MediaStorageSettings.StorageSettings _audioStorageSettings;
        private readonly IValidator<CreateAudioRequest> _validator;

        public CreateAudioRequestValidationTests()
        {
            var options = Options.Create(new MediaStorageSettings
            {
                Audio = MediaStorageSettingBuilder.BuildAudioDefault()
            });
            _audioStorageSettings = options.Value.Audio;
            _validator = new CreateAudioRequestValidator(options);
        }

        [Fact]
        public void ShouldSuccessfullyValidateRequest()
        {
            var request = new CreateAudioRequestFaker().Generate();
            var result = _validator.TestValidate(request);
            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void ShouldBeInvalid_WhenUploadIdIsEmpty()
        {
            var request = new CreateAudioRequestFaker()
                .RuleFor(x => x.UploadId, string.Empty)
                .Generate();
            var result = _validator.TestValidate(request);
            result.ShouldHaveValidationErrorFor(x => x.UploadId);
        }

        [Fact]
        public void ShouldBeInvalid_WhenDurationIsEmpty()
        {
            var request = new CreateAudioRequestFaker()
                .RuleFor(x => x.Duration, () => default)
                .Generate();
            var result = _validator.TestValidate(request);
            result.ShouldHaveValidationErrorFor(x => x.Duration);
        }

        [Fact]
        public void ShouldBeInvalid_WhenFileSizeReachedOverLimit()
        {
            var maxSize = (int)_audioStorageSettings.MaximumFileSize;
            var request = new CreateAudioRequestFaker()
                .RuleFor(x => x.FileSize, maxSize + 100)
                .Generate();
            var result = _validator.TestValidate(request);
            result.ShouldHaveValidationErrorFor(x => x.FileSize);
        }

        [Fact]
        public void ShouldBeInvalid_WhenFileNameDoesNotHaveFileExtension()
        {
            var request = new CreateAudioRequestFaker()
                .RuleFor(x => x.FileName, "test")
                .Generate();
            var result = _validator.TestValidate(request);
            result.ShouldHaveValidationErrorFor(x => x.FileName);
        }
        
        [Fact]
        public void ShouldBeInvalid_WhenFileNameHasInvalidFileExtension()
        {
            var request = new CreateAudioRequestFaker()
                .RuleFor(x => x.FileName, f => f.System.FileName("jpg"))
                .Generate();
            var result = _validator.TestValidate(request);
            result.ShouldHaveValidationErrorFor(x => x.FileName);
        }

        [Fact]
        public void ShouldBeInvalid_WhenContentTypeIsInvalid()
        {
            var request = new CreateAudioRequestFaker()
                .RuleFor(x => x.ContentType, "image/jpeg")
                .Generate();
            var result = _validator.TestValidate(request);
            result.ShouldHaveValidationErrorFor(x => x.ContentType);
        }
    }
}