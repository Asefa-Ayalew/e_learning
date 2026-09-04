using ELearning.Application.Features.LessonProgress.Common;
using MediatR;

namespace ELearning.Application.Features.LessonProgress.Queries.GetLessonProgressByLessonId;

public sealed record GetLessonProgressByLessonIdQuery(
    Guid UserId,
    Guid LessonId
) : IRequest<LessonProgressDto?>;