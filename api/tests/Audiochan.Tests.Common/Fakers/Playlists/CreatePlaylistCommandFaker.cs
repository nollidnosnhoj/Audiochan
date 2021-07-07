﻿using System;
using System.Collections.Generic;
using Audiochan.Core.Entities.Enums;
using Audiochan.Core.Features.Playlists.CreatePlaylist;
using Bogus;

namespace Audiochan.Tests.Common.Fakers.Playlists
{
    public sealed class CreatePlaylistCommandFaker : Faker<CreatePlaylistCommand>
    {
        public CreatePlaylistCommandFaker()
        {
            RuleFor(x => x.Title, f => f.Random.String2(15));
            RuleFor(x => x.Visibility, f => f.PickRandom<Visibility>());
        }
        
        public CreatePlaylistCommandFaker(ICollection<Guid> audioIds)
        {
            RuleFor(x => x.AudioIds, () => audioIds);
        }
    }
}