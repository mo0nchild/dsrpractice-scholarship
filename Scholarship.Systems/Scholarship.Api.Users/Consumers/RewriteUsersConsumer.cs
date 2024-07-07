using MassTransit;
using Scholarship.Service.Users.Infrastructure;
using Scholarship.Service.Users.Models;
using Scholarship.Shared.Messages.UsersMessages;

namespace Scholarship.Api.Users.Consumers
{
    public class RewriteUsersConsumer : IConsumer<RewriteUsersRequest>
    {
        protected ILogger<RewriteUsersConsumer> Logger { get; set; } = default!;
        private readonly IUserService userService = default!;
        public RewriteUsersConsumer(ILogger<RewriteUsersConsumer> logger, IUserService userService) : base()
        {
            this.userService = userService;
            this.Logger = logger;
        }
        public async Task Consume(ConsumeContext<RewriteUsersRequest> context)
        {
            await this.userService.RewriteAllUsers(context.Message.Users.Select(item =>
            {
                return new RewriteUserModel()
                {
                    Email = item.Email,
                    Name = item.Name,
                    Password = item.Password,
                    RoleName = item.RoleName,
                    Uuid = item.Uuid
                };
            }).ToList());
        }
    }
}
