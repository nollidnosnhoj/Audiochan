﻿using Xunit;

namespace Audiochan.IntegrationTests.Features.Audios
{
    [Collection(nameof(SliceFixture))]
    public class GetAudiosTests
    {
        private readonly SliceFixture _fixture;

        public GetAudiosTests(SliceFixture fixture)
        {
            _fixture = fixture;
        }
    }
}