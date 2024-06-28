using AutoMapper;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scholarship.Service.Loans.Infrastructure;
using Scholarship.Shared.Commons.Security;
using Scholarship.Shared.Commons.TransitModels.UserExists;
using System.Net;

namespace Scholarship.Api.Loans.Controllers
{
    [Route("loans"), ApiController]
    public class LoansController : ControllerBase
    {
        private ILogger<LoansController> Logger { get; set; } = default!;

        private readonly IMapper mapper = default!;
        private readonly IRequestClient<UserExistsRequest> requestClient = default!;
        private readonly ILoanService loanService = default!;
        public LoansController(ILoanService loanService, IMapper mapper,
            IRequestClient<UserExistsRequest> requestClient) : base()
        {
            this.Logger = LoggerFactory.Create(builder => builder.AddConsole())
                .CreateLogger<LoansController>();
            (this.loanService, this.mapper, this.requestClient) = (loanService, mapper, requestClient);
        }
        [Route("test"), HttpGet]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> TestHandler()
        {
            var response = await this.requestClient.GetResponse<UserExistsResponse>(new UserExistsRequest()
            {
                UserUuid = Guid.NewGuid()
            });
            if (response == null) throw new Exception("Pizdec");
            return this.Ok($"reponse: {response.Message.Exists}");
        }
        [Authorize("User", AuthenticationSchemes = UsersAuthenticateSchemeOptions.DefaultScheme)]
        [Route("test1"), HttpGet]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> TestAuthHandler()
        {
            foreach (var item in this.HttpContext.User.Claims)
            {
                await Console.Out.WriteLineAsync($"{item.Type}: {item.Value}");
            }
            return this.Ok();
        }
    }
}
