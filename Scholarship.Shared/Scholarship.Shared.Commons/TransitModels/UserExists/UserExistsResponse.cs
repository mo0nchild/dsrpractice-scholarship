using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholarship.Shared.Commons.TransitModels.UserExists
{
    public class UserExistsResponse : object
    {
        public bool Exists { get; set; } = default!;
    }
}
