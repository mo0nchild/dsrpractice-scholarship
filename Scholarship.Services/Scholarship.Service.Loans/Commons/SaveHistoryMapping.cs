using AutoMapper;
using Scholarship.Database.Loans.Entities;
using Scholarship.Shared.Messages.HistoryMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholarship.Service.Loans.Commons
{
    public class SaveHistoryMapping : Profile
    {
        public SaveHistoryMapping() : base() => this.CreateMap<LoanInfo, SaveHistoryRequest>();
    }
}
