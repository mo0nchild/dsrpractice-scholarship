using MassTransit;
using Scholarship.Service.History.Infrastructure;
using Scholarship.Service.History.Models;
using Scholarship.Shared.Messages.HistoryMessages;
using Scholarship.Shared.Messages.LoansMessages;

namespace Scholarship.Api.History.Consumers
{
    public class SaveHistoryConsumer : IConsumer<SaveHistoryRequest>
    {
        protected ILogger<SaveHistoryConsumer> Logger { get; set; } = default!;
        private readonly IHistoryService historyService = default!;
        public SaveHistoryConsumer(IHistoryService historyService, ILogger<SaveHistoryConsumer> logger) : base()
        {
            this.historyService = historyService;
            this.Logger = logger;
        }
        public async Task Consume(ConsumeContext<SaveHistoryRequest> context)
        {
            await this.historyService.AddLoanToHistory(new ClosingLoanModel()
            {
                BeforeTime = context.Message.BeforeTime,
                ClientUuid = context.Message.ClientUuid,
                ClosedTime = context.Message.ClosedTime,
                CreditorName = context.Message.CreditorName,
                CreditorPatronymic = context.Message.CreditorPatronymic,
                CreditorSurname = context.Message.CreditorSurname,
                MoneyAmount = context.Message.MoneyAmount,
                OpenTime = context.Message.OpenTime
            });
        }
    }
}
