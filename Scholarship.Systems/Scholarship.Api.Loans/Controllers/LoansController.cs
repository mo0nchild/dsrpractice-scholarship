using AutoMapper;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scholarship.Api.Loans.Models;
using Scholarship.Service.Loans.Infrastructure;
using Scholarship.Service.Loans.Models;
using Scholarship.Shared.Commons.Responses;
using Scholarship.Shared.Commons.Security;
using Scholarship.Shared.Commons.TransitModels.UserExists;
using System.Net;

namespace Scholarship.Api.Loans.Controllers
{
    [Route("loans"), ApiController]
    public class LoansController : ControllerBase
    {
        private ILogger<LoansController> Logger { get; set; } = default!;
        protected Guid UserUuid { get => this.User.GetUserUuid() ?? throw new Exception("Не найден Uuid пользователя"); }

        private readonly ILoanService loanService = default!;
        private readonly IMapper mapper = default!;
        public LoansController(ILoanService loanService, IMapper mapper) : base()
        {
            this.Logger = LoggerFactory.Create(builder => builder.AddConsole())
                .CreateLogger<LoansController>();
            (this.loanService, this.mapper) = (loanService, mapper);
        }
        [Authorize("User", AuthenticationSchemes = UsersAuthenticateSchemeOptions.DefaultScheme)]
        [Route("add"), HttpPost]
        [ProducesResponseType(typeof(SucceedResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AddLoanHandler([FromBody] AddLoanRequest request)
        {
            var model = this.mapper.Map<CreateLoanModel>(request);
            model.ClientUuid = this.UserUuid;

            await this.loanService.CreateLoan(model);
            return this.Ok(new SucceedResponse() { Message = "Запись о займе добавлена" });
        }
        [Authorize("User", AuthenticationSchemes = UsersAuthenticateSchemeOptions.DefaultScheme)]
        [Route("close"), HttpPut]
        [ProducesResponseType(typeof(SucceedResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CloseLoanHandler(CloseLoanModel request)
        {
            var model = this.mapper.Map<CloseLoanModel>(request);
            await this.loanService.CheckLoanOwner(model.LoanUuid, this.UserUuid);
            await this.loanService.CloseLoan(model);
            return this.Ok(new SucceedResponse() { Message = "Запись о займе добавлена" });
        }
        [Authorize("User", AuthenticationSchemes = UsersAuthenticateSchemeOptions.DefaultScheme)]
        [Route("list"), HttpGet]
        [ProducesResponseType(typeof(List<LoanModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetLoansListHandler()
        {
            return this.Ok(await this.loanService.GetLoansFromUuid(this.UserUuid));
        }
    }
}
