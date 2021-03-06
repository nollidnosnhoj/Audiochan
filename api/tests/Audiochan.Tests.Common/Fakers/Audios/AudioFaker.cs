﻿using Audiochan.Core.Entities;
using Audiochan.Core.Entities.Enums;
using Bogus;

namespace Audiochan.Tests.Common.Fakers.Audios
{
    public sealed class AudioFaker : Faker<Audio>
    {
        public AudioFaker(string userId, bool generateFakeAudioId = false)
        {
            if (generateFakeAudioId)
            {
                RuleFor(x => x.Id, f => f.Random.Guid());
            }
            RuleFor(x => x.UserId, userId);
            RuleFor(x => x.Title, f => f.Random.String2(3, 30));
            RuleFor(x => x.Description, f => f.Lorem.Sentences(2));
            RuleFor(x => x.Visibility, f => f.PickRandom<Visibility>());
            RuleFor(x => x.Size, f => f.Random.Number(1, 20_000_000));
            RuleFor(x => x.Duration, f => f.Random.Number(1, 300));
            RuleFor(x => x.File, f => f.Random.String2(12) + ".mp3");
            RuleFor(x => x.Tags, f => f.Make(f.Random.Number(1, 5), () => 
                    new Tag {Name = f.Random.String2(5, 10)}));
        }
    }
}