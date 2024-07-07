using AutoMapper;
using Scholarship.Database.History.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholarship.Service.History.Models
{
    public class ClosedLoanModel : object
    {
        public Guid Uuid { get; set; } = Guid.NewGuid();
        public Guid ClientUuid { get; set; } = Guid.Empty;
        public double MoneyAmount { get; set; } = default!;

        public DateOnly OpenTime { get; set; } = default!;
        public DateOnly BeforeTime { get; set; } = default!;
        public DateOnly ClosedTime { get; set; } = default!;

        public string CreditorSurname { get; set; } = string.Empty;
        public string CreditorName { get; set; } = string.Empty;
        public string CreditorPatronymic { get; set; } = string.Empty;
    }
    public class ClosedLoanModelProfile : Profile
    {
        public ClosedLoanModelProfile() : base()
        {
            this.CreateMap<ClosedLoanInfo, ClosedLoanModel>().ReverseMap();
        }
    }
}
