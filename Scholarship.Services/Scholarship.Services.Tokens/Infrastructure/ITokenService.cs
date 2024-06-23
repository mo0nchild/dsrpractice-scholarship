using Scholarship.Services.Tokens.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Scholarship.Services.Tokens.Infrastructure
{
    public interface ITokenService
    {
        public Task<TokensModel> CreateJwtTokens(Claim[] claims);
        public Task<Claim[]?> VerifyAccessToken(string token);
        public Task<Claim[]?> VerifyRefreshToken(string token);
    }
}
