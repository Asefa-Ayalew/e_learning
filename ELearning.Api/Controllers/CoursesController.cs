using ELearning.Application.Common.Models;
using ELearning.Application.Features.Courses.Commands.CreateCourse;
using ELearning.Application.Features.Courses.Commands.DeleteCourse;
using ELearning.Application.Features.Courses.Commands.PublishCourse;
using ELearning.Application.Features.Courses.Commands.UnpublishCourse;
using ELearning.Application.Features.Courses.Commands.UpdateCourse;
using ELearning.Application.Features.Courses.Queries.GetCourseById;
using ELearning.Application.Features.Courses.Queries.GetCourses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ELearning.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public sealed class CoursesController : ControllerBase
{
    private readonly ISender _sender;

    public CoursesController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetCourses(
     [FromQuery] CollectionQuery query,
     CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new GetCoursesQuery(query),
            cancellationToken);

        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetCourseById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new GetCourseByIdQuery(id),
            cancellationToken);

        if (result is null)
        {
            return NotFound(new
            {
                message = "Course not found."
            });
        }

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCourse(
        [FromBody] CreateCourseCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            command,
            cancellationToken);

        return CreatedAtAction(
            nameof(GetCourseById),
            new { id = result.Id },
            result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateCourse(
        Guid id,
        [FromBody] UpdateCourseCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.Id)
        {
            return BadRequest(new
            {
                message = "Route ID does not match request ID."
            });
        }

        var result = await _sender.Send(
            command,
            cancellationToken);

        if (result is null)
        {
            return NotFound(new
            {
                message = "Course not found."
            });
        }

        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteCourse(
        Guid id,
        CancellationToken cancellationToken)
    {
        var deleted = await _sender.Send(
            new DeleteCourseCommand(id),
            cancellationToken);

        if (!deleted)
        {
            return NotFound(new
            {
                message = "Course not found."
            });
        }

        return NoContent();
    }
    [HttpPost("{id:guid}/publish")]
    public async Task<IActionResult> PublishCourse(
     Guid id,
     CancellationToken cancellationToken)
    {
        var published = await _sender.Send(
            new PublishCourseCommand(id),
            cancellationToken);

        if (!published)
        {
            return NotFound(new
            {
                message = "Course not found."
            });
        }

        return Ok(new
        {
            message = "Course published successfully."
        });
    }

    [HttpPost("{id:guid}/unpublish")]
    public async Task<IActionResult> UnpublishCourse(
        Guid id,
        CancellationToken cancellationToken)
    {
        var unpublished = await _sender.Send(
            new UnpublishCourseCommand(id),
            cancellationToken);

        if (!unpublished)
        {
            return NotFound(new
            {
                message = "Course not found."
            });
        }

        return Ok(new
        {
            message = "Course unpublished successfully."
        });
    }
}