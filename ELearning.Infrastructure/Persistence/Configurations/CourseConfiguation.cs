using ELearning.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ELearning.Infrastructure.Persistence.Configurations;

public class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.ToTable("courses");


        // ============================================================
        // PRIMARY KEY
        // ============================================================

        builder.HasKey(course => course.Id);


        // ============================================================
        // PROPERTIES
        // ============================================================

        builder.Property(course => course.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(course => course.Slug)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(course => course.Description)
            .HasMaxLength(2000);


        // ============================================================
        // UNIQUE SLUG
        // ============================================================
        //
        // Example:
        //
        // "aspnet-core-for-beginners"
        //
        // should identify only one course.
        //

        builder.HasIndex(course => course.Slug)
            .IsUnique();


        // ============================================================
        // COURSE → CATEGORY
        // MANY-TO-ONE
        // ============================================================
        //
        // Many Courses belong to ONE Category.
        //
        // Course (*) ─────────── (1) Category
        //
        // Foreign key:
        //
        //     Course.CategoryId
        //

        builder.HasOne(course => course.Category)

            // A Category can have many Courses.
            .WithMany(category => category.Courses)

            // Course.CategoryId is the FK.
            .HasForeignKey(course => course.CategoryId)

            // Every Course must have a Category.
            .IsRequired();


        // ============================================================
        // COURSE → INSTRUCTOR
        // MANY-TO-ONE
        // ============================================================
        //
        // Many Courses can belong to ONE Instructor.
        //
        // Course (*) ─────────── (1) User
        //
        // Foreign key:
        //
        //     Course.InstructorId
        //

        builder.HasOne(course => course.Instructor)

            // One User can teach many Courses.
            .WithMany(user => user.Courses)

            // Course.InstructorId is the FK.
            .HasForeignKey(course => course.InstructorId)

            // Every Course must have an Instructor.
            .IsRequired();
    }
}