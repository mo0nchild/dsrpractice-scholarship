using AutoMapper;
using FluentValidation;
using Scholarship.Database.Loans.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholarship.Service.Loans.Models
{
    public class CreateLoanModel : object
    {
        public Guid ClientUuid { get; set; } = Guid.Empty;
        public double MoneyAmount { get; set; } = default!;

        public DateTime OpenTime { get; set; } = default!;
        public DateTime BeforeTime { get; set; } = default!;

        public string CreditorFIO { get; set; } = string.Empty;
    }
    public class CreateLoanModelProfile : Profile
    {
        public CreateLoanModelProfile() : base() => base.CreateMap<CreateLoanModel, LoanInfo>();
    }

    public class CreateLoanModelValidator : AbstractValidator<CreateLoanModel>
    {
        public CreateLoanModelValidator() : base()
        {

        }
    }
}
