﻿using System.Threading;
using System.Threading.Tasks;
using Audiochan.API.Features.Audios.GetAudio;
using Audiochan.API.Features.Audios.SearchAudios;
using Audiochan.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Audiochan.API.Controllers
{
    [Route("api/[controller]")]
    public class SearchController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SearchController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("audios")]
        [ProducesResponseType(typeof(PagedListDto<AudioDetailViewModel>), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Search for audios", OperationId = "SearchAudio", Tags = new[] {"search"})]
        public async Task<IActionResult> SearchAudios([FromQuery] SearchAudiosRequest request,
            CancellationToken cancellationToken)
        {
            var results = await _mediator.Send(request, cancellationToken);
            return new JsonResult(results);
        }
    }
}