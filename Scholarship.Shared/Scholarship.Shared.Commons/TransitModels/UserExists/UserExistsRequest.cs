using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholarship.Shared.Commons.TransitModels.UserExists
{
    public class UserExistsRequest : object
    {
        public Guid UserUuid { get; set; } = Guid.Empty;
    }
}
