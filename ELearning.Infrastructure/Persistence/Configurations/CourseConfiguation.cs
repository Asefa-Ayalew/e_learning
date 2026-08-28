using ELearning.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ELearning.Infrastructure.Persistence.Configurations;

public sealed class CourseConfiguration
    : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.ToTable("courses");

        builder.HasKey(course => course.Id);

        builder.Property(course => course.Id)
            .HasColumnName("id");

        builder.Property(course => course.Title)
            .HasColumnName("title")
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(course => course.Slug)
            .HasColumnName("slug")
            .IsRequired()
            .HasMaxLength(200);

        builder.HasIndex(course => course.Slug)
            .IsUnique();

        builder.Property(course => course.ShortDescription)
            .HasColumnName("short_description")
            .HasMaxLength(500);

        builder.Property(course => course.Description)
            .HasColumnName("description")
            .HasMaxLength(5000);

        builder.Property(course => course.ThumbnailUrl)
            .HasColumnName("thumbnail_url")
            .HasMaxLength(1000);

        builder.Property(course => course.Price)
            .HasColumnName("price")
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(course => course.IsFree)
            .HasColumnName("is_free")
            .IsRequired();

        builder.Property(course => course.IsPublished)
            .HasColumnName("is_published")
            .IsRequired();

        builder.Property(course => course.PublishedAt)
            .HasColumnName("published_at");

        builder.Property(course => course.InstructorId)
            .HasColumnName("instructor_id")
            .IsRequired();

        builder.Property(course => course.CategoryId)
            .HasColumnName("category_id")
            .IsRequired();

        builder.Property(course => course.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(course => course.UpdatedAt)
            .HasColumnName("updated_at");

        builder.HasOne(course => course.Instructor)
            .WithMany(user => user.Courses)
            .HasForeignKey(course => course.InstructorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(course => course.Category)
            .WithMany(category => category.Courses)
            .HasForeignKey(course => course.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(course => course.Sections)
            .WithOne(section => section.Course)
            .HasForeignKey(section => section.CourseId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(course => course.Enrollments)
            .WithOne(enrollment => enrollment.Course)
            .HasForeignKey(enrollment => enrollment.CourseId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(course => course.Reviews)
            .WithOne(review => review.Course)
            .HasForeignKey(review => review.CourseId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(course => course.Certificates)
            .WithOne(certificate => certificate.Course)
            .HasForeignKey(certificate => certificate.CourseId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}