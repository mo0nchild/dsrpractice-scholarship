using AutoMapper;
using Scholarship.Database.Loans.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholarship.Service.Loans.Models
{
    public class RewriteLoanModel : object
    {
        public Guid Uuid { get; set; } = Guid.Empty;

        public Guid ClientUuid { get; set; } = Guid.Empty;
        public double MoneyAmount { get; set; } = default!;

        public DateTime OpenTime { get; set; } = default!;
        public DateTime BeforeTime { get; set; } = default!;

        public DateTime? CloseTime { get; set; } = default!;

        public string CreditorSurname { get; set; } = string.Empty;
        public string CreditorName { get; set; } = string.Empty;
        public string CreditorPatronymic { get; set; } = string.Empty;
    }
    public class RewriteLoanModelProfile : Profile
    {
        public RewriteLoanModelProfile() : base() => this.CreateMap<RewriteLoanModel, LoanInfo>()
            .ReverseMap();
    }
}
