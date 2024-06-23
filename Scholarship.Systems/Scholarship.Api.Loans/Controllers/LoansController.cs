using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scholarship.Service.Loans.Infrastructure;
using Scholarship.Shared.Commons.Security;
using System.Net;

namespace Scholarship.Api.Loans.Controllers
{
    [Route("loans"), ApiController]
    public class LoansController : ControllerBase
    {
        private ILogger<LoansController> Logger { get; set; } = default!;

        private readonly IMapper mapper = default!;
        private readonly ILoanService loanService = default!;
        public LoansController(ILoanService loanService, IMapper mapper) : base()
        {
            this.Logger = LoggerFactory.Create(builder => builder.AddConsole())
                .CreateLogger<LoansController>();
            (this.loanService, this.mapper) = (loanService, mapper);
        }
        [Route("test"), HttpGet]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> TestHandler()
        {
            return this.Ok();
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
