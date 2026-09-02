using ELearning.Application.Common.Models;
using ELearning.Application.Features.Lessons.Common;
using MediatR;

namespace ELearning.Application.Features.Lessons.Queries.GetLessons;

public sealed record GetLessonsQuery(
    Guid? SectionId,
    string? Search,
    int Page = 1,
    int PageSize = 10
) : IRequest<PagedResult<LessonDto>>;