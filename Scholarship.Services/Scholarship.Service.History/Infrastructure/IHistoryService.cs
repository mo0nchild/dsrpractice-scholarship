using Scholarship.Service.History.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholarship.Service.History.Infrastructure
{
    public interface IHistoryService
    {
        public Task AddLoanToHistory(ClosingLoanModel model);
        public Task<List<ClosedLoanModel>> GetClosedLoansByUser(Guid uuid);
        public Task<List<ClosedLoanModel>> GetAllClosedLoans();

        public Task<bool> CheckLoanIsClosed(Guid loanUuid);
        public Task RewriteAllClosedLoans(List<ClosedLoanModel> loansList);
    }
}
