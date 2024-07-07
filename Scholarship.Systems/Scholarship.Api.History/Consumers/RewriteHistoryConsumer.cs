using MassTransit;
using Scholarship.Service.History.Infrastructure;
using Scholarship.Service.History.Models;
using Scholarship.Shared.Messages.HistoryMessages;

namespace Scholarship.Api.History.Consumers
{
    public class RewriteHistoryConsumer : IConsumer<RewriteHistoryRequest>
    {
        protected ILogger<RewriteHistoryConsumer> Logger { get; set; } = default!;
        private readonly IHistoryService historyService = default!;
        public RewriteHistoryConsumer(IHistoryService historyService, ILogger<RewriteHistoryConsumer> logger) : base()
        {
            this.historyService = historyService;
            this.Logger = logger;
        }
        public async Task Consume(ConsumeContext<RewriteHistoryRequest> context)
        {
            await this.historyService.RewriteAllClosedLoans(context.Message.ClosedLoans.Select(item =>
            {
                return new ClosedLoanModel()
                {
                    BeforeTime = item.BeforeTime,
                    ClientUuid = item.ClientUuid,
                    ClosedTime = item.ClosedTime,
                    CreditorName = item.CreditorName,
                    CreditorPatronymic = item.CreditorPatronymic,
                    CreditorSurname = item.CreditorSurname,
                    MoneyAmount = item.MoneyAmount,
                    OpenTime = item.OpenTime,
                    Uuid = item.Uuid,
                };
            }).ToList());
        }
    }
}
