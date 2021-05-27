﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Audiochan.API.Features.Audios.GetAudioList;
using Audiochan.Core.Interfaces;
using Audiochan.Core.Models;
using Audiochan.Core.Persistence;
using MediatR;

namespace Audiochan.API.Features.Audios.SearchAudios
{
    public record SearchAudiosRequest : IHasPage, IRequest<PagedListDto<AudioViewModel>>
    {
        public string Q { get; init; } = string.Empty;
        public string Tags { get; init; } = string.Empty;
        public int Page { get; init; }
        public int Size { get; init; }
    }

    public class SearchAudiosRequestHandler : IRequestHandler<SearchAudiosRequest, PagedListDto<AudioViewModel>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public SearchAudiosRequestHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedListDto<AudioViewModel>> Handle(SearchAudiosRequest request,
            CancellationToken cancellationToken)
        {
            var parsedTags = !string.IsNullOrWhiteSpace(request.Tags)
                ? request.Tags.Split(',')
                    .Select(t => t.Trim().ToLower())
                    .Where(t => !string.IsNullOrWhiteSpace(t))
                    .ToList()
                : new List<string>();
            
            return await _unitOfWork.Audios.GetPagedListBySpec(new SearchAudioSpecification(request.Q, parsedTags),
                request.Page, request.Size, cancellationToken: cancellationToken);
        }
    }
}