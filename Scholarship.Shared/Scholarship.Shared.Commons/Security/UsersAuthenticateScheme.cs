using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using System.Web;

namespace Scholarship.Shared.Commons.Security
{
    public class UsersAuthenticateScheme : AuthenticationHandler<UsersAuthenticateSchemeOptions>
    {
        private readonly IHttpClientFactory httpFactory = default!;
        public UsersAuthenticateScheme(IOptionsMonitor<UsersAuthenticateSchemeOptions> options, 
            ILoggerFactory logger,
            UrlEncoder encoder, IHttpClientFactory httpFactory) 
            : base(options, logger, encoder)
        {
            this.httpFactory = httpFactory;
        }
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var identityResponse = default(IdentityResponse);
            var authorizationHeader = this.Request.Headers["Authorization"].FirstOrDefault();
            if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer "))
            {
                return AuthenticateResult.Fail("Ошибка валидации заголовка аутентификации");
            }
            var tokenValue = authorizationHeader.Split(' ')[1];
            using (var httpClient = this.httpFactory.CreateClient())
            {
                httpClient.BaseAddress = this.Options.BaseUrl;
                var response = await httpClient.GetAsync($"/users/info?token={tokenValue}");
                if (response == null || !response.IsSuccessStatusCode) return AuthenticateResult.NoResult();

                var content = await response.Content.ReadAsStringAsync();
                identityResponse = JsonConvert.DeserializeObject<IdentityResponse>(content);
            }
            if (identityResponse == null) return AuthenticateResult.NoResult();
            var claims = new Claim[] {
                new Claim(ClaimTypes.PrimarySid, identityResponse.Uuid.ToString()),
                new Claim(ClaimTypes.Name, identityResponse.Name),
                new Claim(ClaimTypes.Email, identityResponse.Email),
                new Claim(ClaimTypes.Role, identityResponse.RoleName),
            };
            var identity = new ClaimsIdentity(claims, this.Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            return AuthenticateResult.Success(new AuthenticationTicket(principal, this.Scheme.Name));
        }
        public class IdentityResponse : object
        {
            public Guid Uuid { get; set; } = Guid.Empty;
            public string RoleName { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string Name { get; set; } = string.Empty;
        }
    }
    public class UsersAuthenticateSchemeOptions : AuthenticationSchemeOptions
    {
        public const string DefaultScheme = "MyAuthenticationScheme";
        public Uri BaseUrl { get; set; } = new Uri("http://localhost:8080");
    }
    public static class UserAuthenticationExtensions : object
    {
        public static AuthenticationBuilder AddUsersAuthentication(this AuthenticationBuilder builder, 
            Action<UsersAuthenticateSchemeOptions> configuration)
        {
            return builder.AddScheme<UsersAuthenticateSchemeOptions, UsersAuthenticateScheme>(
                UsersAuthenticateSchemeOptions.DefaultScheme, configuration);
        }
    }
}
