﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Audiochan.Core.Entities;

namespace Audiochan.Core.Repositories
{
    public interface ITagRepository : IGenericRepository<Tag>
    {
        Task<List<Tag>> GetAppropriateTags(List<string> tags, CancellationToken cancellationToken = default);
    }
}