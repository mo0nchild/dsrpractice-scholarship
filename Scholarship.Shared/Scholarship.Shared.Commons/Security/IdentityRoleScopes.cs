using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholarship.Shared.Commons.Security
{
    public static class IdentityRoleScopes : object
    {
        private static readonly Dictionary<string, string> IdentityRoles = new()
        {
            ["User"] = "User", ["Admin"] = "Admin",
        };
        public static string User { get => IdentityRoleScopes.IdentityRoles["User"]; }
        public static string Admin { get => IdentityRoleScopes.IdentityRoles["Admin"]; }
    }
}
