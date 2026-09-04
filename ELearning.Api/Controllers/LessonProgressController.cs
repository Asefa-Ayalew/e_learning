using ELearning.Application.Features.LessonProgress.Commands.CreateLessonProgress;
using ELearning.Application.Features.LessonProgress.Commands.UpdateLessonProgress;
using ELearning.Application.Features.LessonProgress.Queries.GetLessonProgressById;
using ELearning.Application.Features.LessonProgress.Queries.GetLessonProgressByLessonId;
using ELearning.Application.Features.LessonProgress.Queries.GetLessonProgressByUserId;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ELearning.Api.Controllers;

[ApiController]
[Route("api/lesson-progresses")]
public sealed class LessonProgressesController : ControllerBase
{
    private readonly ISender _sender;

    public LessonProgressesController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateLessonProgressCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            command,
            cancellationToken);

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.Id },
            result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(
        Guid id, CancellationToken cancellationToken
        )
    {
        var query = new GetLessonProgressByIdQuery(id);
        var result = await _sender.Send(
            query,
            cancellationToken
        );

        return Ok(result);
    }
    [HttpGet("lesson/{lessonId:guid}/user/{userId:guid}")]
    public async Task<IActionResult> GetByLessonId(
      Guid lessonId,
      Guid userId,
      CancellationToken cancellationToken)
    {
        var query = new GetLessonProgressByLessonIdQuery(
            userId,
            lessonId);

        var result = await _sender.Send(
            query,
            cancellationToken);

        return Ok(result);
    }
    [HttpGet("user/{userId:guid}")]
    public async Task<IActionResult> GetByUserId(
        Guid userId,
        CancellationToken cancellationToken
    )
    {
        var query = new GetLessonProgressByUserIdQuery(userId);

        var result = await _sender.Send(query, cancellationToken);

        return Ok(result);
    }
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(
    Guid id,
    [FromBody] UpdateLessonProgressCommand command,
    CancellationToken cancellationToken)
    {
        if (id != command.Id)
        {
            return BadRequest(
                "The route ID does not match the progress ID.");
        }

        var result = await _sender.Send(
            command,
            cancellationToken);

        return Ok(result);
    }
}