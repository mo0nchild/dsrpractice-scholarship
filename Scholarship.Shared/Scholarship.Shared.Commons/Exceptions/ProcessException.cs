using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholarship.Shared.Commons.Exceptions
{
    public class ProcessException : Exception
    {
        public ProcessException(string message) : base(message) { }
        public static void ThrowIf(Func<bool> predicate, string message)
        {
            if (predicate.Invoke()) throw new ProcessException(message);
        }
    }
}
