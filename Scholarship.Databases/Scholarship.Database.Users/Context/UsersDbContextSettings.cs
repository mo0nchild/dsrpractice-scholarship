using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholarship.Database.Users.Context
{
    public class UsersDbContextSettings : object
    {
        public string ConnectionString { get; set; } = string.Empty;
        public AdministratorInfo? AdminInfo { get; set; } = default!; 
    }
    public class AdministratorInfo : object
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
