using ELearning.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ELearning.Infrastructure.Persistence.Configurations;

public sealed class CategoryConfiguration
    : IEntityTypeConfiguration<Category>
{
    public void Configure(
        EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("categories");

        builder.HasKey(category => category.Id);

        builder.Property(category => category.Id)
            .HasColumnName("id");

        builder.Property(category => category.Name)
            .HasColumnName("name")
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(category => category.Slug)
            .HasColumnName("slug")
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(category => category.Slug)
            .IsUnique();

        builder.Property(category => category.Description)
            .HasColumnName("description")
            .HasMaxLength(500);

        builder.Property(category => category.ImageUrl)
            .HasColumnName("image_url")
            .HasMaxLength(1000);

        builder.Property(category => category.IsActive)
            .HasColumnName("is_active")
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(category => category.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(category => category.UpdatedAt)
            .HasColumnName("updated_at");
    }
}