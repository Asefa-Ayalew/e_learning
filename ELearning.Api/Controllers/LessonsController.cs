using ELearning.Application.Features.Lessons.Commands.CreateLesson;
using ELearning.Application.Features.Lessons.Commands.DeleteLesson;
using ELearning.Application.Features.Lessons.Commands.UpdateLesson;
using ELearning.Application.Features.Lessons.Queries.GetLessonById;
using ELearning.Application.Features.Lessons.Queries.GetLessons;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ELearning.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public sealed class LessonsController : ControllerBase
{
    private readonly ISender _sender;

    public LessonsController(ISender sender)
    {
        _sender = sender;
    }
    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] GetLessonsQuery query,
        CancellationToken cancellationToken)
    {
        var lessons = await _sender.Send(
            query,
            cancellationToken);

        return Ok(lessons);
    }
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var lesson = await _sender.Send(
            new GetLessonByIdQuery(id),
            cancellationToken);

        if (lesson is null)
        {
            return NotFound();
        }

        return Ok();
    }
    [HttpPost]
    public async Task<IActionResult> Create(
       [FromBody] CreateLessonCommand command,
       CancellationToken cancellationToken)
    {
        var lessonId = await _sender.Send(
            command,
            cancellationToken);

        return CreatedAtAction(
            nameof(GetById),
            new { id = lessonId },
            new
            {
                id = lessonId
            });
    }
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(
        [FromRoute] Guid id,
        [FromBody] UpdateLessonCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.Id)
        {
            return BadRequest("Id in route does not match Id in request body.");
        }

        var updatedLesson = await _sender.Send(
            command,
            cancellationToken);

        return Ok(updatedLesson);
    }
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(
    Guid id,
    CancellationToken cancellationToken)
    {
        await _sender.Send(
            new DeleteLessonCommand(id),
            cancellationToken);

        return NoContent();
    }
}