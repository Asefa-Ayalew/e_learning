using ELearning.Application.Features.Sections.Common;
using MediatR;

namespace ELearning.Application.Features.Sections.Queries.GetSectionById;

public sealed record GetSectionByIdQuery(
    Guid Id
) : IRequest<SectionDto?>;