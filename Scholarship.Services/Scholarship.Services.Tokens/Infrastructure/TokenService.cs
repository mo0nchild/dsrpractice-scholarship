using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Abstractions;
using Microsoft.IdentityModel.Tokens;
using Scholarship.Service.Tokens.Configurations;
using Scholarship.Services.Tokens.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Sockets;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Scholarship.Services.Tokens.Infrastructure
{
    public class TokenService : ITokenService
    {
        protected readonly ILogger<TokenService> logger = default!;
        public TokenOptions TokenOptions { get; set; }
        public TokenService(IOptions<TokenOptions> options, ILogger<TokenService> logger) : base() 
        {
            this.logger = logger;
            this.TokenOptions = options.Value;
        }
        protected virtual string GenerateToken(Claim[] claims, byte[] symmetricKey, int expires)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityKey = new SymmetricSecurityKey(symmetricKey);
            var securityToken = tokenHandler.CreateToken(new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(expires),
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            });
            return tokenHandler.WriteToken(securityToken);
        }
        public Task<TokensModel> CreateJwtTokens(Claim[] claims)
        {
            var accessSymmetricKey = Encoding.UTF8.GetBytes(this.TokenOptions.AccessSecret);
            var refreshSymmetricKey = Encoding.UTF8.GetBytes(this.TokenOptions.RefreshSecret);
            
            return Task.FromResult(new TokensModel()
            {
                AccessToken = this.GenerateToken(claims, accessSymmetricKey, this.TokenOptions.AccessExpires),
                RefreshToken = this.GenerateToken(claims, refreshSymmetricKey, this.TokenOptions.RefreshExpires)
            });
        }
        protected virtual Claim[]? ValidateToken(string token, byte[] symmetricKey)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(symmetricKey),
                ValidateIssuer = false, 
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            };
            try
            {
                var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
                var jwtToken = (JwtSecurityToken)validatedToken;

                if (jwtToken.ValidTo < DateTime.UtcNow) throw new Exception("Токен просрочен");
                return principal.Claims.ToArray();
            }
            catch (Exception error)
            {
                Console.WriteLine($"Ошибка в валидации токена: {error.Message}");
                return null;
            }
        } 
        public Task<Claim[]?> VerifyAccessToken(string token)
        {
            return Task.FromResult(this.ValidateToken(token, Encoding.UTF8.GetBytes(this.TokenOptions.AccessSecret)));   
        }
        public Task<Claim[]?> VerifyRefreshToken(string token)
        {
            return Task.FromResult(this.ValidateToken(token, Encoding.UTF8.GetBytes(this.TokenOptions.RefreshSecret)));
        }
    }
}
