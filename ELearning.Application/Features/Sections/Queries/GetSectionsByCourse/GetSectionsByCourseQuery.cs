using ELearning.Application.Features.Sections.Common;
using MediatR;

namespace ELearning.Application.Features.Sections.Queries.GetSEctionsByCourse;

public sealed record GetSectionsByCourseQuery(
    Guid CourseId
) : IRequest<IReadOnlyList<SectionDto>>;