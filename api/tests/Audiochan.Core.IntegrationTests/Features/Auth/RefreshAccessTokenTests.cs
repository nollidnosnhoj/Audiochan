﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Audiochan.Core.Entities;
using Audiochan.Core.Features.Auth.Login;
using Audiochan.Core.Features.Auth.Refresh;
using Bogus;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Audiochan.Core.IntegrationTests.Features.Auth
{
    [Collection(nameof(SliceFixture))]
    public class RefreshAccessTokenTests
    {
        private readonly SliceFixture _fixture;

        public RefreshAccessTokenTests(SliceFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task ShouldSuccessfullyRefreshAccessToken()
        {
            // Assign
            var faker = new Faker();
            var username = faker.Random.String2(15);
            var password = faker.Internet.Password();
            var (userId, _) = await _fixture.RunAsUserAsync(username, password, Array.Empty<string>());
            
            // Act
            // 1. User logins into account. Receives access and refresh token.
            // 2. In the real world, the access token will expire. Once it expires, then the client needs
            //      to call the refresh token endpoint to get a new access token.
            var loginResult = await _fixture.SendAsync(new LoginCommand
            {
                Login = username,
                Password = password
            });
            var refreshResult = await _fixture.SendAsync(new RefreshTokenCommand
            {
                RefreshToken = loginResult.Data!.RefreshToken
            });

            // Assert
            var userRefreshTokens = await GetUserRefreshTokens(userId);
            refreshResult.IsSuccess.Should().Be(true);
            refreshResult.Data.Should().NotBeNull();
            userRefreshTokens.Count.Should().BeGreaterThan(0);
            userRefreshTokens.Should().Contain(x => x.Token == refreshResult.Data!.RefreshToken);
            userRefreshTokens.Should().NotContain(x => x.Token == loginResult.Data.RefreshToken);
        }
        
        private async Task<List<RefreshToken>> GetUserRefreshTokens(string userId)
        {
            return await _fixture.ExecuteDbContextAsync(dbContext =>
            {
                return dbContext.Users
                    .AsNoTracking()
                    .Include(u => u.RefreshTokens)
                    .Where(u => u.Id == userId)
                    .SelectMany(u => u.RefreshTokens)
                    .ToListAsync();
            });
        }
    }
}