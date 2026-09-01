using MediatR;

namespace ELearning.Application.Features.Sections.Commands.CreateSection;

public sealed record CreateSectionCommand(
    Guid CourseId,
    string Title,
    string? Description,
    int DisplayOrder
) : IRequest<Guid>;