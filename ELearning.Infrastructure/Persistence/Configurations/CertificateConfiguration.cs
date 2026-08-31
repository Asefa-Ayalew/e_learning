using ELearning.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ELearning.Infrastructure.Persistence.Configurations;

public sealed class CertificateConfiguration
    : IEntityTypeConfiguration<Certificate>
{
    public void Configure(EntityTypeBuilder<Certificate> builder)
    {
        builder.ToTable("certificates");

        builder.HasKey(certificate => certificate.Id);

        builder.Property(certificate => certificate.Id)
            .HasColumnName("id");

        builder.Property(certificate => certificate.UserId)
            .HasColumnName("user_id")
            .IsRequired();

        builder.HasOne(certificate => certificate.User)
            .WithMany(user => user.Certificates)
            .HasForeignKey(certificate => certificate.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(certificate => certificate.CourseId)
            .HasColumnName("course_id")
            .IsRequired();

        builder.HasOne(certificate => certificate.Course)
            .WithMany(course => course.Certificates)
            .HasForeignKey(certificate => certificate.CourseId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(certificate => certificate.CertificateNumber)
            .HasColumnName("certificate_number")
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(certificate => certificate.CertificateNumber)
            .IsUnique();

        builder.Property(certificate => certificate.IssuedAt)
            .HasColumnName("issued_at")
            .IsRequired();

        builder.Property(certificate => certificate.CertificateUrl)
            .HasColumnName("certificate_url")
            .HasMaxLength(1000);
    }
}
