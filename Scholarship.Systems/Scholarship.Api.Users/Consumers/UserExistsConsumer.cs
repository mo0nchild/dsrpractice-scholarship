using MassTransit;
using Scholarship.Shared.Commons.TransitModels.UserExists;

namespace Scholarship.Api.Users.Consumers
{
    public class UserExistsConsumer : IConsumer<UserExistsRequest>
    {
        public UserExistsConsumer() : base()
        {

        }
        public async Task Consume(ConsumeContext<UserExistsRequest> context)
        {
            Console.WriteLine("\n\n\n\nCONSUMER");
            await context.RespondAsync<UserExistsResponse>(new UserExistsResponse()
            {
                Exists = true
            });
        }
    }
}
