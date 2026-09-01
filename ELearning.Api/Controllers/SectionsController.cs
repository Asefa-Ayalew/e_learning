using System.Globalization;
using ELearning.Application.Features.Sections.Commands.CreateSection;
using ELearning.Application.Features.Sections.Commands.DeleteSection;
using ELearning.Application.Features.Sections.Commands.UpdateSection;
using ELearning.Application.Features.Sections.Queries.GetSectionById;
using ELearning.Application.Features.Sections.Queries.GetSEctionsByCourse;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ELearning.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public sealed class SectionsController : ControllerBase
{
    private readonly ISender _sender;

    public SectionsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateSectionCommand command,
        CancellationToken cancellationToken)
    {
        var sectionId = await _sender.Send(
            command,
            cancellationToken);

        return CreatedAtAction(
            nameof(GetById),
            new { id = sectionId },
            new
            {
                id = sectionId
            });
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var query = new GetSectionByIdQuery(id);

        var section = await _sender.Send(
            query,
            cancellationToken);

        if (section is null)
        {
            return NotFound(new
            {
                message = "Section not found."
            });
        }

        return Ok(section);
    }
    [HttpGet("course/{courseId:guid}")]
    public async Task<IActionResult> GetByCourse(
        Guid courseId,
        CancellationToken cancellationToken)
    {
        var query = new GetSectionsByCourseQuery(courseId);
        var sections = await _sender.Send(
            query,
            cancellationToken);

        return Ok(sections);
    }
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(
        Guid id,
        [FromBody] UpdateSectionCommand command,
        CancellationToken cancellationToken)
    {
        command = command with { Id = id };

        var updated = await _sender.Send(
            command,
            cancellationToken);

        if (!updated)
        {
            return NotFound(new
            {
                message = "Section not found."
            });
        }

        return NoContent();
    }
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(
        Guid id,
        CancellationToken cancellationToken)
    {
        var command = new DeleteSectionCommand(id);

        var deleted = await _sender.Send(
            command,
            cancellationToken);

        if (!deleted)
        {
            return NotFound(new
            {
                message = "Section not found."
            });
        }

        return NoContent();
    }
}