using ELearning.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ELearning.Application.Interfaces;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; }

    DbSet<Role> Roles { get; }

    DbSet<UserRole> UserRoles { get; }
    DbSet<Section> Sections { get; }

    DbSet<RefreshToken> RefreshTokens { get; }
    DbSet<Course> Courses { get; }
    DbSet<Category> Categories { get; }
    DbSet<Enrollment> Enrollments { get; }
    DbSet<Lesson> Lessons { get; }
    DbSet<LessonProgress> LessonProgresses { get; }

    Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default);
}