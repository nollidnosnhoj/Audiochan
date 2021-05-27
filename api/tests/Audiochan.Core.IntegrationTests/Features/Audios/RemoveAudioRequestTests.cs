﻿using System;
using System.Threading.Tasks;
using Audiochan.API.Features.Audios.RemoveAudio;
using Audiochan.Core.Entities;
using Audiochan.Core.Models;
using Audiochan.Tests.Common.Fakers.Audios;
using FluentAssertions;
using Xunit;

namespace Audiochan.Core.IntegrationTests.Features.Audios
{
    [Collection(nameof(SliceFixture))]
    public class RemoveAudioRequestTests
    {
        private readonly SliceFixture _fixture;

        public RemoveAudioRequestTests(SliceFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task ShouldNotRemoveAudio_WhenUserCannotModify()
        {
            // Assign
            var (ownerId, _) =
                await _fixture.RunAsUserAsync("kopacetic", Guid.NewGuid().ToString(), Array.Empty<string>());

            var audio = new AudioFaker(ownerId).Generate();

            await _fixture.InsertAsync(audio);

            // Act
            await _fixture.RunAsDefaultUserAsync();

            var command = new RemoveAudioRequest(audio.Id);

            var result = await _fixture.SendAsync(command);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().Be(false);
            result.ErrorCode.Should().Be(ResultError.Forbidden);
        }

        [Fact]
        public async Task ShouldRemoveAudio()
        {
            var (ownerId, _) = await _fixture.RunAsDefaultUserAsync();
            var audio = new AudioFaker(ownerId).Generate();
            await _fixture.InsertAsync(audio);

            var command = new RemoveAudioRequest(audio.Id);
            var result = await _fixture.SendAsync(command);

            var created = await _fixture.FindAsync<Audio, Guid>(audio.Id);

            result.Should().NotBeNull();
            result.IsSuccess.Should().Be(true);

            created.Should().BeNull();
        }
    }
}