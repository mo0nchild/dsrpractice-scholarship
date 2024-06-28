using AutoMapper;
using Scholarship.Service.Loans.Models;

namespace Scholarship.Api.Loans.Models
{
    public class AddLoanRequest : object
    {
        public double MoneyAmount { get; set; } = default!;

        public DateTime OpenTime { get; set; } = default!;
        public DateTime BeforeTime { get; set; } = default!;

        public string CreditorSurname { get; set; } = string.Empty;
        public string CreditorName { get; set; } = string.Empty;
        public string CreditorPatronymic { get; set; } = string.Empty;
    }
    public class AddLoanRequestProfile : Profile
    {
        public AddLoanRequestProfile() : base() => this.CreateMap<AddLoanRequest, CreateLoanModel>();
    }
}
