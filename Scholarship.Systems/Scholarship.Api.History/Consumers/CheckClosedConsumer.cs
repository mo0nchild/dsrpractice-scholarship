using MassTransit;
using Scholarship.Service.History.Infrastructure;
using Scholarship.Shared.Messages.HistoryMessages;

namespace Scholarship.Api.History.Consumers
{
    public class CheckClosedConsumer : IConsumer<CheckLoanClosedRequest>
    {
        protected ILogger<CheckClosedConsumer> Logger { get; set; } = default!;
        private readonly IHistoryService historyService = default!;
        public CheckClosedConsumer(IHistoryService historyService, ILogger<CheckClosedConsumer> logger) : base()
        {
            this.historyService = historyService;
            this.Logger = logger;
        }
        public async Task Consume(ConsumeContext<CheckLoanClosedRequest> context)
        {
            await context.RespondAsync<CheckLoanClosedResponse>(new CheckLoanClosedResponse()
            {
                Closed = await this.historyService.CheckLoanIsClosed(context.Message.LoanUuid)
            });
        }
    }
}
