using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholarship.Shared.Messages.UsersMessages
{
    public class RewriteUsersRequest : object
    {
        public List<UsersItemMessage> Users { get; set; } = new();
        public class UsersItemMessage : object
        {
            public Guid Uuid { get; set; } = Guid.Empty;
            public string Email { get; set; } = string.Empty;
            public string Name { get; set; } = string.Empty;

            public string Password { get; set; } = string.Empty;
            public string RoleName { get; set; } = string.Empty;
        }
    }
}
