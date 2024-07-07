using MassTransit;
using Scholarship.Service.Users.Infrastructure;
using Scholarship.Shared.Messages.LoansMessages;
using Scholarship.Shared.Messages.UsersMessages;

namespace Scholarship.Api.Users.Consumers
{
    public class GetAllUsersConsumer : IConsumer<GetAllUsersRequest>
    {
        protected ILogger<GetAllUsersConsumer> Logger { get; set; } = default!;
        private readonly IUserService userService = default!;

        public GetAllUsersConsumer(IUserService userService, ILogger<GetAllUsersConsumer> logger) : base() 
        {
            this.userService = userService;
            this.Logger = logger;
        }
        public async Task Consume(ConsumeContext<GetAllUsersRequest> context)
        {
            await context.RespondAsync<GetAllUsersResponse>(new GetAllUsersResponse()
            {
                Users = (await this.userService.GetAllUsers()).Select(item =>
                {
                    return new GetAllUsersResponse.UsersItemMessage()
                    {
                        Uuid = item.Uuid,
                        Email = item.Email,
                        Name = item.Name,
                        Password = item.Password,
                        RoleName = item.RoleName,
                    };
                }).ToList()
            });
        }
    }
}
