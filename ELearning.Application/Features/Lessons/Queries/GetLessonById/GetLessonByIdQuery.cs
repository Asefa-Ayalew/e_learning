using ELearning.Application.Features.Lessons.Common;
using MediatR;

namespace ELearning.Application.Features.Lessons.Queries.GetLessonById;

public sealed record GetLessonByIdQuery(Guid Id) : IRequest<LessonDto?>;