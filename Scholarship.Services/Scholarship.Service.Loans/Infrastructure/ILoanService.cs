using Scholarship.Service.Loans.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholarship.Service.Loans.Infrastructure
{
    public interface ILoanService
    {
        public Task<List<LoanModel>> GetLoansFromUuid(Guid clientUuid);
        public Task CreateLoan(CreateLoanModel loanInfo);
        public Task CloseLoan(CloseLoanModel loanInfo);
        public Task<bool> CheckLoanOwner(Guid loanUuid, Guid userUuid);
        
        public Task<List<LoanModel>> GetAllLoans();
        public Task RewriteAllLoans(List<RewriteLoanModel> loansList);
    }
}
