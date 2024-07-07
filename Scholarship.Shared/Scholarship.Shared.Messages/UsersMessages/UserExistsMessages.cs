using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholarship.Shared.Messages.UsersMessages
{
    public class UserExistsRequest : object
    {
        public Guid UserUuid { get; set; } = Guid.Empty;
    }
    public class UserExistsResponse : object
    {
        public bool Exists { get; set; } = default!;
    }
}
