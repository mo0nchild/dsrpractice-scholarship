using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholarship.Shared.Messages.LoansMessages
{
    public class GetAllLoansRequest : object { }
    public class GetAllLoansResponse : object
    {
        public List<LoansMessageItem> Loans { get; set; } = new();
        public class LoansMessageItem : object
        {
            public Guid Uuid { get; set; } = Guid.Empty;
            public Guid ClientUuid { get; set; } = Guid.Empty;
            public double MoneyAmount { get; set; } = default!;

            public DateOnly OpenTime { get; set; } = default!;
            public DateOnly BeforeTime { get; set; } = default!;
            public DateOnly? ClosedTime { get; set; } = default!;

            public string CreditorSurname { get; set; } = string.Empty;
            public string CreditorName { get; set; } = string.Empty;
            public string CreditorPatronymic { get; set; } = string.Empty;
        }
    }
}
