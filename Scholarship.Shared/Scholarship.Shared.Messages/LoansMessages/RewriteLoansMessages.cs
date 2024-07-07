using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholarship.Shared.Messages.LoansMessages
{
    public class RewriteLoansRequest : object
    {
        public List<LoansItemMessage> Loans { get; set; } = new();
        public class LoansItemMessage : object
        {
            public Guid Uuid { get; set; } = Guid.Empty;
            public Guid ClientUuid { get; set; } = Guid.Empty;
            public double MoneyAmount { get; set; } = default!;

            public DateOnly OpenTime { get; set; } = default!;
            public DateOnly BeforeTime { get; set; } = default!;

            public string CreditorSurname { get; set; } = string.Empty;
            public string CreditorName { get; set; } = string.Empty;
            public string CreditorPatronymic { get; set; } = string.Empty;
        }
    }
}
