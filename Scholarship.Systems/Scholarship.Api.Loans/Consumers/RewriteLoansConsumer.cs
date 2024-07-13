using MassTransit;
using Scholarship.Service.Loans.Infrastructure;
using Scholarship.Service.Loans.Models;
using Scholarship.Shared.Messages.LoansMessages;

namespace Scholarship.Api.Loans.Consumers
{
    public class RewriteLoansConsumer : IConsumer<RewriteLoansRequest>
    {
        protected ILogger<RewriteLoansConsumer> Logger { get; set; } = default!;
        private readonly ILoanService loanService = default!;
        public RewriteLoansConsumer(ILoanService loanService, ILogger<RewriteLoansConsumer> logger) : base()
        {
            this.loanService = loanService;
            this.Logger = logger;
        }
        public async Task Consume(ConsumeContext<RewriteLoansRequest> context)
        {
            await this.loanService.RewriteAllLoans(context.Message.Loans.Select(item =>
            {
                return new RewriteLoanModel()
                {
                    BeforeTime = item.BeforeTime,
                    ClientUuid = item.ClientUuid,
                    CreditorName = item.CreditorName,
                    CloseTime = item.ClosedTime,
                    CreditorPatronymic = item.CreditorPatronymic,
                    CreditorSurname = item.CreditorSurname,
                    MoneyAmount = item.MoneyAmount,
                    OpenTime = item.OpenTime,
                    Uuid = item.Uuid
                };
            }).ToList());
        }
    }
}
