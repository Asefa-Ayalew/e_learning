using ELearning.Application.Features.Categories.Commands.DeleteCategory;
using ELearning.Application.Features.Categories.Common;
using ELearning.Application.Features.Categories.Commands.CreateCategory;
using ELearning.Application.Features.Categories.Queries.GetCategories;
using ELearning.Application.Features.Categories.Queries.GetCategoryById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ELearning.Application.Features.Categories.Commands.UpdateCategory;
using ELearning.Application.Common.Models;

namespace ELearning.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class CategoriesController : ControllerBase
{
    private readonly ISender _sender;

    public CategoriesController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(
     typeof(PagedResult<CategoryResponse>),
     StatusCodes.Status200OK)]
    public async Task<ActionResult<PagedResult<CategoryResponse>>> GetCategories(
     [FromQuery] CollectionQuery query,
     CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new GetCategoriesQuery(query),
            cancellationToken);

        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    [ProducesResponseType(
        typeof(CategoryResponse),
        StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CategoryResponse>> GetById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new GetCategoryByIdQuery(id),
            cancellationToken);

        if (result is null)
        {
            return NotFound(new
            {
                message = "Category not found."
            });
        }

        return Ok(result);
    }

    [Authorize]
    [HttpPost]
    [ProducesResponseType(
        typeof(CategoryResponse),
        StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<CategoryResponse>> Create(
        CreateCategoryCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            command,
            cancellationToken);

        return Created(
            $"/api/categories/{result.Id}",
            result);
    }

    [Authorize]
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(
        Guid id,
        CancellationToken cancellationToken)
    {
        var deleted = await _sender.Send(
            new DeleteCategoryCommand(id),
            cancellationToken);

        if (!deleted)
        {
            return NotFound(new
            {
                message = "Category not found."
            });
        }

        return NoContent();
    }
    [Authorize]
    [HttpPut("{id:guid}")]
    [ProducesResponseType(
    typeof(CategoryResponse),
    StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CategoryResponse>> Update(
    Guid id,
    UpdateCategoryCommand command,
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
                message = "Category not found."
            });
        }

        return Ok(result);
    }
}