namespace ELearning.Application.Common.Authorization;

public static class AuthorizationPolicies
{
    public const string RequireAdmin = "RequireAdmin";
    public const string RequireInstructor = "RequireInstructor";
    public const string RequireStudent = "RequireStudent";
    public const string RequireInstructorOrAdmin = "RequireInstructorOrAdmin";
}