using AutoMapper;
using Scholarship.Database.History.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholarship.Service.History.Models
{
    public class ClosingLoanModel : object
    {
        public Guid ClientUuid { get; set; } = Guid.Empty;
        public double MoneyAmount { get; set; } = default!;

        public DateOnly OpenTime { get; set; } = default!;
        public DateOnly BeforeTime { get; set; } = default!;
        public DateOnly ClosedTime { get; set; } = default!;

        public string CreditorSurname { get; set; } = string.Empty;
        public string CreditorName { get; set; } = string.Empty;
        public string CreditorPatronymic { get; set; } = string.Empty;
    }
    public class ClosingLoanModelProfile : Profile
    {
        public ClosingLoanModelProfile() : base()
        {
            this.CreateMap<ClosingLoanModel, ClosedLoanInfo>();
        }
    }
}
