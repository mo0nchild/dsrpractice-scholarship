using AutoMapper;
using Scholarship.Database.Loans.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholarship.Service.Loans.Models
{
    public class LoanModel : object
    {
        public Guid Uuid { get; set; } = Guid.Empty;
        public Guid ClientUuid { get; set; } = Guid.Empty;
        public double MoneyAmount { get; set; } = default!;

        public DateOnly OpenTime { get; set; } = default!;
        public DateOnly BeforeTime { get; set; } = default!;

        public CreditorInfo Creditor { get; set; } = default!;
    }
    public class CreditorInfo : object 
    {
        public string Surname { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Patronymic { get; set; } = string.Empty;
    }
    public class LoanModelProfile : Profile
    {
        public LoanModelProfile() : base() => base.CreateMap<LoanInfo, LoanModel>()
            .ForMember(item => item.Creditor, options => options.MapFrom(p => new CreditorInfo()
            {
                Surname = p.CreditorSurname,
                Name = p.CreditorName,
                Patronymic = p.CreditorPatronymic,
            }));
    }
}
