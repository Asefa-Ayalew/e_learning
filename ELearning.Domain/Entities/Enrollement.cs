using ELearning.Domain.Enums;

namespace ELearning.Domain.Entities;

public class Enrollment
{
    public Guid Id { get; set; }


    // ============================================================
    // FOREIGN KEY: User
    // ============================================================
    //
    // Relationship:
    //
    //      User 1 ─────────── * Enrollment
    //
    // One User can have MANY enrollments.
    //
    // UserId → User.Id
    //
    public Guid UserId { get; set; }

    // Navigation property:
    //
    //      Enrollment → User
    //
    public User User { get; set; } = null!;


    // ============================================================
    // FOREIGN KEY: Course
    // ============================================================
    //
    // Relationship:
    //
    //      Course 1 ─────────── * Enrollment
    //
    // One Course can have MANY enrollments.
    //
    // CourseId → Course.Id
    //
    public Guid CourseId { get; set; }

    // Navigation property:
    //
    //      Enrollment → Course
    //
    public Course Course { get; set; } = null!;


    // ============================================================
    // WHY ENROLLMENT CREATES A MANY-TO-MANY RELATIONSHIP
    // ============================================================
    //
    // From the business perspective:
    //
    //      User * ─────────── * Course
    //
    // One User can enroll in MANY Courses.
    //
    // One Course can have MANY Users.
    //
    // Therefore:
    //
    //      User ↔ Course = MANY-TO-MANY
    //
    // But Enrollment contains additional information:
    //
    //      EnrolledAt
    //      CompletedAt
    //      Status
    //      ProgressPercentage
    //
    // Therefore Enrollment is a JOIN/ASSOCIATIVE ENTITY.
    //
    public DateTime EnrolledAt { get; set; } = DateTime.UtcNow;

    public DateTime? CompletedAt { get; set; }

    public EnrollmentStatus Status { get; set; } = EnrollmentStatus.Active;

    public decimal ProgressPercentage { get; set; }
}