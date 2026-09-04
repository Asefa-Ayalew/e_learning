using ELearning.Application.Features.LessonProgress.Common;
using MediatR;

namespace ELearning.Application.Features.LessonProgress.Queries.GetLessonProgressById;

public sealed record GetLessonProgressByIdQuery(
    Guid Id
) : IRequest<LessonProgressDto>;
