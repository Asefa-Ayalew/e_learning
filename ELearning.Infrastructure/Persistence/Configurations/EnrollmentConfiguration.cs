using ELearning.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ELearning.Infrastructure.Persistence.Configurations;

public class EnrollmentConfiguration : IEntityTypeConfiguration<Enrollment>
{
    public void Configure(EntityTypeBuilder<Enrollment> builder)
    {
        builder.ToTable("enrollments");
        builder.HasKey(enrollment => enrollment.Id);
        builder.Property(enrollment => enrollment.EnrolledAt)
            .IsRequired();

        builder.Property(enrollment => enrollment.CompletedAt);

        builder.Property(enrollment => enrollment.ProgressPercentage)
            .HasPrecision(5, 2)
            .IsRequired();

        builder.Property(enrollment => enrollment.Status)
            .IsRequired();

        builder.HasOne(enrollment => enrollment.User)
            .WithMany(user => user.Enrollments)
            .HasForeignKey(enrollment => enrollment.UserId)
            .IsRequired();

        builder.HasOne(enrollment => enrollment.Course)
            .WithMany(course => course.Enrollments)
            .HasForeignKey(enrollment => enrollment.CourseId)
            .IsRequired();

        builder.HasIndex(enrollment => new
        {
            enrollment.UserId,
            enrollment.CourseId
        })
        .IsUnique();
    }
}