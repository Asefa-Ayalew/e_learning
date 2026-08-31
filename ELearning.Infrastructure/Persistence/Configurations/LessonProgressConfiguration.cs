using ELearning.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ELearning.Infrastructure.Persistence.Configurations;

public sealed class LessonProgressConfiguration
: IEntityTypeConfiguration<LessonProgress>
{
    public void Configure(
     EntityTypeBuilder<LessonProgress> builder
    )
    {
        builder.ToTable("lesson_progresses");

        builder.HasKey(progress => progress.Id);

        builder.Property(progress => progress.Id)
        .HasColumnName("id");

        builder.Property(progress => progress.UserId)
        .HasColumnName("user_id")
        .IsRequired();

        builder.Property(progress => progress.LessonId)
        .HasColumnName("lesson_id")
        .IsRequired();

        builder.Property(progress => progress.IsCompleted)
        .HasColumnName("is_completed")
        .IsRequired();

        builder.Property(progress => progress.WatchedSeconds)
        .HasColumnName("watched_seconds")
        .IsRequired();

        builder.Property(progress => progress.CompletedAt)
        .HasColumnName("completed_at");

        builder.Property(progress => progress.LastAccessedAt)
        .HasColumnName("last_accessed_at")
        .IsRequired();

        builder.HasOne(progress => progress.User)
        .WithMany(user => user.LessonProgresses)
        .HasForeignKey(progress => progress.UserId)
        .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(progress => progress.Lesson)
        .WithMany(lesson => lesson.ProgressRecords)
        .HasForeignKey(progress => progress.LessonId)
        .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(progress => new
        {
            progress.UserId,
            progress.LessonId
        })
        .IsUnique();
    }
}