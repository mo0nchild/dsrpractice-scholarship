﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scholarship.Api.Users.Models;
using Scholarship.Database.Users.Entities;
using Scholarship.Service.Users.Infrastructure;
using Scholarship.Service.Users.Models;
using Scholarship.Shared.Commons.Responses;
using System.Net;
using System.Security.Claims;

namespace Scholarship.Api.Users.Controllers
{
    [Route("users"), ApiController]
    public class UsersController : ControllerBase
    {
        private ILogger<UsersController> Logger { get; set; } = default!;

        private readonly IMapper mapper = default!;
        private readonly IUserService userService = default!;
        public UsersController(IUserService userService, IMapper mapper) : base()
        {
            this.Logger = LoggerFactory.Create(builder => builder.AddConsole())
                .CreateLogger<UsersController>();
            (this.userService, this.mapper) = (userService, mapper);
        }
        [Route("add"), HttpPost]
        [ProducesResponseType(typeof(IdentityModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> RegistrationHandler([FromBody] RegistrationRequest request)
        {
            var model = this.mapper.Map<RegistrationModel>(request);
            return this.Ok(await this.userService.Registration(model));
        }
        [Route("login"), HttpGet]
        [ProducesResponseType(typeof(IdentityModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> LoginHandler([FromQuery] CredentialsModel request)
        {
            var data = await this.userService.GetTokensByCredentials(request);
            if (data == null) return this.BadRequest(new ErrorResponse() { Cause = "Невозможно получить ответ" });
            return this.Ok(data);
        }
        [Route("info"), HttpGet]
        [ProducesResponseType(typeof(UserModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetInfoHandler([FromQuery] string token)
        {
            var data = await this.userService.GetUserByAccess(token);
            if (data == null) return this.BadRequest(new ErrorResponse() { Cause = "Невозможно получить ответ" });
            return this.Ok(data);
        }
        [Route("refresh"), HttpGet]
        [ProducesResponseType(typeof(IdentityModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRefreshHandler([FromQuery] string token)
        {
            var data = await this.userService.GetTokensByRefresh(token);
            if (data == null) return this.BadRequest(new ErrorResponse() { Cause = "Невозможно получить ответ" });
            return this.Ok(data);
        }
    }
}
