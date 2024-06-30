using MassTransit;
using Scholarship.Service.Users.Infrastructure;
using Scholarship.Shared.Commons.TransitModels;

namespace Scholarship.Api.Users.Consumers
{
    public class UserExistsConsumer : IConsumer<UserExistsRequest>
    {
        protected ILogger<UserExistsConsumer> Logger { get; set; } = default!;
        private readonly IUserService userService = default!;
        public UserExistsConsumer(ILogger<UserExistsConsumer> logger, IUserService userService) : base()
        {
            this.userService = userService;
            this.Logger = logger;
        } 
        public async Task Consume(ConsumeContext<UserExistsRequest> context)
        {
            await context.RespondAsync<UserExistsResponse>(new UserExistsResponse()
            {
                Exists = await this.userService.IsUserExist(context.Message.UserUuid)
            });
        }
    }
}
