using ELearning.Application.Features.LessonProgress.Common;
using MediatR;

namespace ELearning.Application.Features.LessonProgress.Queries.GetLessonProgressByUserId;

public sealed record GetLessonProgressByUserIdQuery(
    Guid UserId
) : IRequest<IReadOnlyList<LessonProgressDto>>;