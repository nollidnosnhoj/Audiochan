﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Audiochan.API.Extensions;
using Audiochan.API.Features.Audios.CreateAudio;
using Audiochan.API.Features.Audios.GetAudio;
using Audiochan.API.Features.Audios.GetAudioList;
using Audiochan.API.Features.Audios.RemoveAudio;
using Audiochan.API.Features.Audios.UpdateAudio;
using Audiochan.API.Features.Audios.UpdatePicture;
using Audiochan.API.Models;
using Audiochan.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Audiochan.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class AudiosController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AudiosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(Name = "GetAudios")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(PagedListDto<AudioViewModel>), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Returns a list of audios.", OperationId = "GetAudios", Tags = new[] {"audios"})]
        public async Task<IActionResult> GetList([FromQuery] GetAudioListRequest query,
            CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(query, cancellationToken));
        }

        [HttpGet("{audioId:guid}", Name = "GetAudio")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(AudioDetailViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorApiResponse), StatusCodes.Status404NotFound)]
        [SwaggerOperation(Summary = "Return an audio by ID.", OperationId = "GetAudio", Tags = new[] {"audios"})]
        public async Task<IActionResult> Get(Guid audioId, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAudioRequest(audioId), cancellationToken);
            return result != null
                ? Ok(result)
                : NotFound(ErrorApiResponse.NotFound("Audio was not found."));
        }

        [HttpPost(Name = "CreateAudio")]
        [ApiExplorerSettings(IgnoreApi = true)]
        [ProducesResponseType(typeof(AudioDetailViewModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Create([FromBody] CreateAudioRequest request,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return result.IsSuccess
                ? CreatedAtAction(nameof(Get), new {audioId = result.Data.Id}, result.Data)
                : result.ReturnErrorResponse();
        }

        [HttpPut("{audioId:guid}", Name = "UpdateAudio")]
        [ProducesResponseType(typeof(AudioDetailViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [SwaggerOperation(
            Summary = "Update audio's details.",
            Description = "Requires authentication.",
            OperationId = "UpdateAudio",
            Tags = new[] {"audios"})]
        public async Task<IActionResult> Update(Guid audioId,
            [FromBody] UpdateAudioRequest request,
            CancellationToken cancellationToken)
        {
            request.AudioId = audioId;
            var result = await _mediator.Send(request, cancellationToken);
            return result.IsSuccess ? Ok(result.Data) : result.ReturnErrorResponse();
        }

        [HttpDelete("{audioId:guid}", Name = "DeleteAudio")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ErrorApiResponse), StatusCodes.Status404NotFound)]
        [SwaggerOperation(
            Summary = "Remove audio.",
            Description = "Requires authentication.",
            OperationId = "DeleteAudio",
            Tags = new[] {"audios"})]
        public async Task<IActionResult> Destroy(Guid audioId, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new RemoveAudioRequest(audioId), cancellationToken);
            return result.IsSuccess ? NoContent() : result.ReturnErrorResponse();
        }

        [HttpPatch("{audioId:guid}/picture")]
        [SwaggerOperation(
            Summary = "Add Picture.",
            Description = "Requires authentication.",
            OperationId = "AddAudioPicture",
            Tags = new[] {"audios"})]
        public async Task<IActionResult> AddPicture(Guid audioId,
            [FromBody] UpdateAudioPictureRequest request,
            CancellationToken cancellationToken)
        {
            request.AudioId = audioId;
            var result = await _mediator.Send(request, cancellationToken);
            return result.IsSuccess
                ? Ok(result.Data)
                : result.ReturnErrorResponse();
        }
    }
}