namespace ELearning.Domain.Entities;

public class Category
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public string? Description { get; set; }

    public string? ImageUrl { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }


    // ============================================================
    // ONE-TO-MANY RELATIONSHIP
    // ============================================================
    //
    // Relationship:
    //
    //      Category 1  ───────────  * Course
    //
    // Meaning:
    //      One Category can have MANY Courses.
    //
    // Example:
    //
    //      Category: "Programming"
    //          │
    //          ├── C# Fundamentals
    //          ├── ASP.NET Core
    //          ├── Entity Framework Core
    //          └── Clean Architecture
    //
    // The "1" side is Category.
    // The "*" side is Course.
    //
    // The foreign key is NOT stored in Category.
    // The foreign key is stored in Course:
    //
    //      Course.CategoryId
    //
    // Why?
    //
    // Because Course is the "many" side of the relationship.
    //
    // Database:
    //
    //      Categories
    //      ----------------
    //      Id
    //      Name
    //
    //      Courses
    //      ----------------
    //      Id
    //      Title
    //      CategoryId  <-- Foreign Key → Categories.Id
    //
    // This ICollection allows us to navigate from:
    //
    //      Category → Courses
    //
    // It does NOT create the foreign key itself.
    // The foreign key property is in Course.
    //
    public ICollection<Course> Courses { get; set; }
        = new List<Course>();
}