using ELearning.Application.Common.Authorization;

namespace ELearning.Api.Configuration;

/// Configures ASP.NET Core authorization policies.
/// Policies define WHAT authenticated users are allowed to do.
public static class AuthorizationConfiguration
{
    public static IServiceCollection AddApplicationAuthorization(
        this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            // ADMIN

            options.AddPolicy(
                AuthorizationPolicies.RequireAdmin,
                policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireRole(AppRoles.Admin);
                });

            // INSTRUCTOR

            options.AddPolicy(
                AuthorizationPolicies.RequireInstructor,
                policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireRole(AppRoles.Instructor);
                });

            // STUDENT

            options.AddPolicy(
                AuthorizationPolicies.RequireStudent,
                policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireRole(AppRoles.Student);
                });

            // INSTRUCTOR OR ADMIN

            options.AddPolicy(
                AuthorizationPolicies.RequireInstructorOrAdmin,
                policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireRole(
                        AppRoles.Admin,
                        AppRoles.Instructor);
                });
        });

        return services;
    }
}