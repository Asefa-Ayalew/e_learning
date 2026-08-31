using ELearning.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ELearning.Infrastructure.Persistence.Configurations;

public sealed class LessonConfiguration
    : IEntityTypeConfiguration<Lesson>
{
    public void Configure(
        EntityTypeBuilder<Lesson> builder)
    {
        builder.ToTable("lessons");

        builder.HasKey(lesson => lesson.Id);

        builder.Property(lesson => lesson.Id)
            .HasColumnName("id");

        builder.Property(lesson => lesson.SectionId)
            .HasColumnName("section_id")
            .IsRequired();

        builder.Property(lesson => lesson.Title)
            .HasColumnName("title")
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(lesson => lesson.Description)
            .HasColumnName("description");

        builder.Property(lesson => lesson.Type)
            .HasColumnName("type")
            .IsRequired();

        builder.Property(lesson => lesson.VideoUrl)
            .HasColumnName("video_url")
            .HasMaxLength(1000);

        builder.Property(lesson => lesson.Content)
            .HasColumnName("content");

        builder.Property(lesson => lesson.DurationInSeconds)
            .HasColumnName("duration_in_seconds")
            .IsRequired();

        builder.Property(lesson => lesson.DisplayOrder)
            .HasColumnName("display_order")
            .IsRequired();

        builder.Property(lesson => lesson.IsFreePreview)
            .HasColumnName("is_free_preview")
            .IsRequired();

        builder.Property(lesson => lesson.IsPublished)
            .HasColumnName("is_published")
            .IsRequired();

        builder.Property(lesson => lesson.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(lesson => lesson.UpdatedAt)
            .HasColumnName("updated_at");

        builder.HasOne(lesson => lesson.Section)
            .WithMany(section => section.Lessons)
            .HasForeignKey(lesson => lesson.SectionId)
            .OnDelete(DeleteBehavior.Cascade);


        builder.HasIndex(lesson => new
        {
            lesson.SectionId,
            lesson.DisplayOrder
        });
    }
}