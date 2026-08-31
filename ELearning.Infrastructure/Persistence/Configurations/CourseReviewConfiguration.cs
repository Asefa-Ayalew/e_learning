using ELearning.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ELearning.Infrastructure.Persistence.Configurations;

public sealed class CourseReviewConfiguration
: IEntityTypeConfiguration<CourseReview>
{
    public void Configure(
        EntityTypeBuilder<CourseReview> builder
    )
    {
        builder.ToTable("course_reviews");
        builder.HasKey(review => review.Id);

        builder.Property(review => review.Id)
        .HasColumnName("id");

        builder.Property(review => review.CourseId)
        .HasColumnName("course_id")
        .IsRequired();

        builder.Property(review => review.UserId)
        .HasColumnName("user_id")
        .IsRequired();

        builder.Property(review => review.Rating)
        .HasColumnName("rating")
        .IsRequired();

        builder.Property(review => review.Comment)
        .HasColumnName("comment");

        builder.Property(review => review.IsPublished)
        .HasColumnName("is_published")
        .IsRequired();

        builder.Property(review => review.CreatedAt)
        .HasColumnName("created_at")
        .IsRequired();

        builder.Property(review => review.UpdatedAt)
        .HasColumnName("updated_at");

        builder.HasOne(review => review.Course)
        .WithMany(course => course.Reviews)
        .HasForeignKey(review => review.CourseId)
        .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(review => review.User)
        .WithMany(course => course.CourseReviews)
        .HasForeignKey(review => review.UserId)
        .OnDelete(DeleteBehavior.Cascade);

        // ============================================================
        // UNIQUE INDEX
        // ============================================================
        //
        // One User can leave only ONE review for a Course.
        //
        //      CourseId + UserId must be unique.
        //
        builder.HasIndex(review => new
        {
            review.CourseId,
            review.UserId
        })
        .IsUnique();
    }
}