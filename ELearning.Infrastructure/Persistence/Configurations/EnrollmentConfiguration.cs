using ELearning.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ELearning.Infrastructure.Persistence.Configurations;

public sealed class EnrollmentConfiguration
: IEntityTypeConfiguration<Enrollment>
{
    public void Configure(EntityTypeBuilder<Enrollment> builder)
    {
        builder.ToTable("enrollments");

        builder.HasKey(enrollment => enrollment.Id);

        builder.Property(enrollment => enrollment.Id)
            .HasColumnName("id");

        builder.Property(enrollment => enrollment.UserId)
            .HasColumnName("user_id")
            .IsRequired();

        builder.Property(enrollment => enrollment.CourseId)
            .HasColumnName("course_id")
            .IsRequired();

        builder.Property(enrollment => enrollment.EnrolledAt)
            .HasColumnName("enrolled_at")
            .IsRequired();

        builder.Property(enrollment => enrollment.CompletedAt)
            .HasColumnName("completed_at");

        builder.Property(enrollment => enrollment.ProgressPercentage)
            .HasColumnName("progress_percentage")
            .HasPrecision(5, 2)
            .IsRequired();

        builder.Property(enrollment => enrollment.Status)
            .HasColumnName("status")
            .IsRequired();

        builder.HasOne(enrollment => enrollment.User)
            .WithMany(user => user.Enrollments)
            .HasForeignKey(enrollment => enrollment.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(enrollment => enrollment.Course)
            .WithMany(course => course.Enrollments)
            .HasForeignKey(enrollment => enrollment.CourseId)
            .OnDelete(DeleteBehavior.Cascade);


        builder.HasIndex(enrollment => new
        {
            enrollment.UserId,
            enrollment.CourseId
        })
        .IsUnique();
    }
}
