using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholarship.Shared.Messages.HistoryMessages
{
    public class CheckLoanClosedRequest : object
    {
        public Guid LoanUuid { get; set; } = Guid.Empty;
    }
    public class CheckLoanClosedResponse : object
    {
        public bool Closed { get; set; } = default!;
    }
}
