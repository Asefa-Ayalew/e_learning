using ELearning.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ELearning.Infrastructure.Persistence.Configurations;

public sealed class SectionConfiguration
    : IEntityTypeConfiguration<Section>
{
    public void Configure(
        EntityTypeBuilder<Section> builder)
    {
        builder.ToTable("sections");

        builder.HasKey(section => section.Id);

        builder.Property(section => section.Id)
            .HasColumnName("id");

        builder.Property(section => section.CourseId)
            .HasColumnName("course_id")
            .IsRequired();

        builder.Property(section => section.Title)
            .HasColumnName("title")
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(section => section.Description)
            .HasColumnName("description");

        builder.Property(section => section.DisplayOrder)
            .HasColumnName("display_order")
            .IsRequired();

        builder.Property(section => section.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(section => section.UpdatedAt)
            .HasColumnName("updated_at");

        builder.HasOne(section => section.Course)
            .WithMany(course => course.Sections)
            .HasForeignKey(section => section.CourseId)
            .OnDelete(DeleteBehavior.Cascade);

        // ============================================================
        // INDEX
        // ============================================================
        //
        // Allows efficient ordering of sections within a course.
        //

        builder.HasIndex(section => new
        {
            section.CourseId,
            section.DisplayOrder
        });
    }
}