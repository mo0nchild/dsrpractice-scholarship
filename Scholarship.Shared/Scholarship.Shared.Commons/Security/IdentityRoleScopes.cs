using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholarship.Shared.Commons.Security
{
    public static class IdentityRoleScopes : object
    {
        public static string User { get => "User"; }
        public static string Admin { get => "Admin"; }
    }
}
