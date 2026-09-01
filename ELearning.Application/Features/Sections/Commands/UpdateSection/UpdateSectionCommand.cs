using MediatR;

namespace ELearning.Application.Features.Sections.Commands.UpdateSection;

public sealed record UpdateSectionCommand(
    Guid Id,
    string Title,
    string? Description,
    int DisplayOrder
) : IRequest<bool>;