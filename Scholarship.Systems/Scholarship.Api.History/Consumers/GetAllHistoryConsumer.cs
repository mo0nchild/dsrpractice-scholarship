using MassTransit;
using Scholarship.Service.History.Infrastructure;
using Scholarship.Shared.Messages.HistoryMessages;

namespace Scholarship.Api.History.Consumers
{
    public class GetAllHistoryConsumer : IConsumer<GetAllHistoryRequest>
    {
        protected ILogger<GetAllHistoryConsumer> Logger { get; set; } = default!;
        private readonly IHistoryService historyService = default!;
        public GetAllHistoryConsumer(IHistoryService historyService, ILogger<GetAllHistoryConsumer> logger) : base()
        {
            this.historyService = historyService;
            this.Logger = logger;
        }
        public async Task Consume(ConsumeContext<GetAllHistoryRequest> context)
        {
            var result = await this.historyService.GetAllClosedLoans();
            await context.RespondAsync<GetAllHistoryResponse>(new GetAllHistoryResponse()
            {
                Loans = result.Select(item =>
                {
                    return new GetAllHistoryResponse.LoansMessageItem()
                    {
                        BeforeTime = item.BeforeTime,
                        ClientUuid = item.ClientUuid,
                        ClosedTime = item.ClosedTime,
                        CreditorName = item.CreditorName,
                        CreditorPatronymic = item.CreditorPatronymic,
                        CreditorSurname = item.CreditorSurname,
                        MoneyAmount = item.MoneyAmount,
                        OpenTime = item.OpenTime,
                        Uuid = item.Uuid
                    };
                }).ToList()
            });
        }
    }
}
