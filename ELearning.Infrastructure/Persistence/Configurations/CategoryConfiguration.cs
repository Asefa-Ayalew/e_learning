using ELearning.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ELearning.Infrastructure.Persistence.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        // ============================================================
        // TABLE
        // ============================================================
        //
        // This tells EF Core:
        //
        //     Category entity → Categories table
        //
        builder.ToTable("categories");


        // ============================================================
        // PRIMARY KEY
        // ============================================================
        //
        // Category.Id becomes the primary key.
        //
        builder.HasKey(category => category.Id);


        // ============================================================
        // BASIC PROPERTIES
        // ============================================================

        builder.Property(category => category.Name)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(category => category.Slug)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(category => category.Description)
            .HasMaxLength(1000);

        builder.Property(category => category.ImageUrl)
            .HasMaxLength(500);

        builder.Property(category => category.IsActive)
            .IsRequired();

        builder.Property(category => category.CreatedAt)
            .IsRequired();

        builder.Property(category => category.UpdatedAt);


        // ============================================================
        // UNIQUE INDEX
        // ============================================================
        //
        // A category name should not be duplicated.
        //
        // Example:
        //
        //     Programming
        //     Programming   <-- NOT allowed
        //
        builder.HasIndex(category => category.Name)
            .IsUnique();


        // ============================================================
        // ONE-TO-MANY RELATIONSHIP
        // ============================================================
        //
        // Our relationship is:
        //
        //     Category 1 ─────────── * Course
        //
        // One Category can have MANY Courses.
        //
        // Category:
        //
        //     public ICollection<Course> Courses
        //
        // Course:
        //
        //     public Guid CategoryId
        //     public Category Category
        //
        builder.HasMany(category => category.Courses)

            // Each Course belongs to ONE Category.
            //
            // Course.Category
            //
            .WithOne(course => course.Category)

            // Course.CategoryId is the foreign key.
            //
            // Course is the MANY/dependent side.
            //
            .HasForeignKey(course => course.CategoryId)

            // A Course MUST have a Category.
            //
            // Therefore CategoryId is required.
            //
            .IsRequired();
    }
}