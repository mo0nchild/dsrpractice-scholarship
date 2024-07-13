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
    public class RewriteLoanModel : CreateLoanModel
    {
        public Guid Uuid { get; set; } = Guid.Empty;
        public DateOnly? CloseTime { get; set; } = default!;
    }
    public class RewriteLoanModelProfile : Profile
    {
        public RewriteLoanModelProfile() : base() => this.CreateMap<RewriteLoanModel, LoanInfo>()
            .ReverseMap();
    }
}
