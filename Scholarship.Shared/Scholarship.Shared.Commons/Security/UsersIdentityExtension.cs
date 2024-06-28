using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Scholarship.Shared.Commons.Security
{
    public static class UsersIdentityExtension : object
    {
        public static Guid? GetUserUuid(this ClaimsPrincipal principal)
        {
            var uuid = principal.FindFirstValue(ClaimTypes.PrimarySid);
            return Guid.TryParse(uuid, out var result) ? result : null;
        }
        public static string? GetUserEmail(this ClaimsPrincipal principal)
        {
            return principal.FindFirstValue(ClaimTypes.Email);
        }
    }
}
