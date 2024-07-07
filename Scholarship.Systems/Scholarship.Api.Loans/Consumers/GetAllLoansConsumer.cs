using MassTransit;
using Scholarship.Service.Loans.Infrastructure;
using Scholarship.Shared.Messages.LoansMessages;

namespace Scholarship.Api.Loans.Consumers
{
    public class GetAllLoansConsumer : IConsumer<GetAllLoansRequest>
    {
        protected ILogger<GetAllLoansConsumer> Logger { get; set; } = default!;
        private readonly ILoanService loanService = default!;
        public GetAllLoansConsumer(ILoanService loanService, ILogger<GetAllLoansConsumer> logger) : base() 
        {
            this.loanService = loanService;
            this.Logger = logger;
        }
        public async Task Consume(ConsumeContext<GetAllLoansRequest> context)
        {
            var loansList = await this.loanService.GetAllLoans();
            await context.RespondAsync<GetAllLoansResponse>(new GetAllLoansResponse()
            {
                Loans = loansList.Select(item => new GetAllLoansResponse.LoansMessageItem()
                {
                    Uuid = item.Uuid,
                    ClientUuid = item.ClientUuid,
                    OpenTime = item.OpenTime,
                    CreditorName = item.Creditor.Name,
                    CreditorPatronymic = item.Creditor.Patronymic,
                    CreditorSurname = item.Creditor.Surname,
                    MoneyAmount = item.MoneyAmount,
                    BeforeTime = item.BeforeTime,
                }).ToList()
            });
        }
    }
}
