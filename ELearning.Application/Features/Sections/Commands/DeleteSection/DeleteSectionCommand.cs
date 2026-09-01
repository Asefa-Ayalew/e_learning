using MediatR;

namespace ELearning.Application.Features.Sections.Commands.DeleteSection;

public sealed record DeleteSectionCommand(
    Guid Id
) : IRequest<bool>;