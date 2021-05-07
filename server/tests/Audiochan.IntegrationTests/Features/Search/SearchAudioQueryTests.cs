﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Audiochan.Core.Common.Builders;
using Audiochan.Core.Common.Helpers;
using Audiochan.Core.Entities.Enums;
using Audiochan.Core.Features.Audios.CreateAudio;
using Audiochan.Core.Features.Audios.SearchAudios;
using Audiochan.UnitTests;
using Bogus;
using FluentAssertions;
using Xunit;

namespace Audiochan.IntegrationTests.Features.Search
{
    [Collection(nameof(SliceFixture))]
    public class SearchAudioQueryTests
    {
        private readonly SliceFixture _fixture;

        public SearchAudioQueryTests(SliceFixture sliceFixture)
        {
            _fixture = sliceFixture;
        }

        [Fact]
        public async Task ShouldFilterAudio_BasedOnSearchTerm()
        {
            var random = new Randomizer();
            var (userId, _) = await _fixture.RunAsDefaultUserAsync();

            for (var i = 0; i < 10; i++)
            {
                var title = "testaudio" + i;
                if (i > 0 && i % 3 == 0)
                {
                    title = "EXAMPLE";
                    if (random.Int(1, 10) % 2 == 0)
                        title = "ABC123 " + title;
                    if (random.Int(1, 10) % 2 == 0)
                        title += " ABC123";
                }
                var audio = await new AudioBuilder()
                    .UseTestDefaults(userId)
                    .AddTitle(title)
                    .SetVisibility(Visibility.Public)
                    .BuildAsync();
                await _fixture.InsertAsync(audio);
            }

            var result = await _fixture.SendAsync(new SearchAudiosRequest
            {
                Q = "example"
            });

            result.Should().NotBeNull();
            result.Count.Should().Be(3);
            result.Items.Count.Should().Be(3);
        }

        [Fact]
        public async Task ShouldFilterAudio_BasedOnTags()
        {
            const int resultCount = 6;

            await _fixture.RunAsDefaultUserAsync();

            for (var i = 0; i < 10; i++)
            {
                var tags = new List<string>();

                if (i > 0 && i % 2 == 0)
                    tags.Add("testtag1");
                if (i > 0 && i % 3 == 0)
                    tags.Add("testtag2");

                await _fixture.SendAsync(new CreateAudioRequest
                {
                    Title = $"Test Song #{i + 1}",
                    UploadId = UploadHelpers.GenerateUploadId(),
                    FileName = "test.mp3",
                    Duration = 100,
                    FileSize = 100,
                    Tags = tags,
                    Visibility = Visibility.Public
                });
            }

            var result = await _fixture.SendAsync(new SearchAudiosRequest
            {
                Tags = "testtag1, testtag2"
            });

            result.Should().NotBeNull();
            result.Count.Should().Be(resultCount);
            result.Items.Count.Should().Be(resultCount);
        }
    }
}