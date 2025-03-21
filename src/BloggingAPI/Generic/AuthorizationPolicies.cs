using Microsoft.AspNetCore.Authorization;

namespace BloggingAPI.Generic
{
    public static class AuthorizationPolicies
    {
        public const string RequireSuperAdmin = "RequireSuperAdmin";
        public const string RequireAdminOrSuperAdmin = "RequireAdminOrSuperAdmin";
        public const string RequireUserOrAdmin = "RequireUserOrAdmin";
        public const string RequireReportUser = "RequireReportUser";
        public const string RequireAccountUser = "RequireAccountUser";

        public static void AddCustomAuthorizationPolicies(AuthorizationOptions options)
        {
            options.AddPolicy(RequireSuperAdmin, policy =>
               policy.RequireAssertion(context =>
                   context.User.IsInRole("SuperAdmin")
               ));

            options.AddPolicy(RequireAdminOrSuperAdmin, policy =>
                policy.RequireAssertion(context =>
                    context.User.IsInRole("SuperAdmin") || context.User.IsInRole("Admin")
                ));

            options.AddPolicy(RequireUserOrAdmin, policy =>
                policy.RequireAssertion(context =>
                    context.User.IsInRole("SuperAdmin") || context.User.IsInRole("Admin") || context.User.IsInRole("User")
                ));

            options.AddPolicy(RequireReportUser, policy =>
                 policy.RequireAssertion(context =>
                      context.User.IsInRole("SuperAdmin") || context.User.IsInRole("Admin") || context.User.IsInRole("ReportUser")
                 ));

            options.AddPolicy(RequireAccountUser, policy =>
                 policy.RequireAssertion(context =>
                      context.User.IsInRole("SuperAdmin") || context.User.IsInRole("Admin") || context.User.IsInRole("AccountUser")
                 ));
        }
    }
}
