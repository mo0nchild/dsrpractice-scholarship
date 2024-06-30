using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scholarship.Service.Backup.Infrastructure;
using Scholarship.Shared.Commons.Exceptions;
using Scholarship.Shared.Commons.Responses;
using Scholarship.Shared.Commons.Security;
using System.Net;

namespace Scholarship.Api.Backup.Controllers
{
    [Route("backup"), ApiController]
    public class BackupController : ControllerBase
    {
        private ILogger<BackupController> Logger { get; set; } = default!;
        protected Guid UserUuid { get => this.User.GetUserUuid() ?? throw new ProcessException("Не найден Uuid пользователя"); }

        private readonly IBackupService backupService = default!;
        private readonly IMapper mapper = default!;
        public BackupController(IBackupService backupService, IMapper mapper) : base()
        {
            this.Logger = LoggerFactory.Create(builder => builder.AddConsole())
                .CreateLogger<BackupController>();
            (this.backupService, this.mapper) = (backupService, mapper);
        }
        [Authorize("Admin", AuthenticationSchemes = UsersAuthenticateSchemeOptions.DefaultScheme)]
        [Route("get"), HttpGet]
        [ProducesResponseType(typeof(SucceedResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetBackupHandler()
        {
            return this.Ok(new SucceedResponse() { Message = "Тест" });
        }
    }
}
