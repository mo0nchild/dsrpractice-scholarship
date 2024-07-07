using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scholarship.Service.History.Infrastructure;
using Scholarship.Service.History.Models;
using Scholarship.Shared.Commons.Exceptions;
using Scholarship.Shared.Commons.Security;
using System.Net;

namespace Scholarship.Api.History.Controllers
{
    [Route("history"), ApiController]
    public class HistoryController : ControllerBase
    {
        private ILogger<HistoryController> Logger { get; set; } = default!;
        protected Guid UserUuid { get => this.User.GetUserUuid() ?? throw new ProcessException("Не найден Uuid пользователя"); }
        
        private readonly IHistoryService historyService = default!;
        private readonly IMapper mapper = default!;
        public HistoryController(IHistoryService historyService, IMapper mapper) : base()
        {
            this.Logger = LoggerFactory.Create(builder => builder.AddConsole())
                .CreateLogger<HistoryController>();
            (this.historyService, this.mapper) = (historyService, mapper);
        }
        [Authorize("Admin", AuthenticationSchemes = UsersAuthenticateSchemeOptions.DefaultScheme)]
        [Route("getAll"), HttpGet]
        [ProducesResponseType(typeof(List<ClosedLoanModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAllHistoryHandler()
        {
            return this.Ok(await this.historyService.GetAllClosedLoans());
        }
        [Authorize("User", AuthenticationSchemes = UsersAuthenticateSchemeOptions.DefaultScheme)]
        [Route("get"), HttpGet]
        [ProducesResponseType(typeof(List<ClosedLoanModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetHisotryByUserHandler()
        {
            return this.Ok(await this.historyService.GetClosedLoansByUser(this.UserUuid));
        }
    }
}
