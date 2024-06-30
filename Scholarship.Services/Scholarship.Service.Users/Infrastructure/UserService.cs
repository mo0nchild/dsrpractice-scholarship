using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Scholarship.Database.Users.Context;
using Scholarship.Database.Users.Entities;
using Scholarship.Service.Users.Models;
using Scholarship.Services.Tokens.Infrastructure;
using Scholarship.Services.Tokens.Models;
using Scholarship.Shared.Commons.Exceptions;
using Scholarship.Shared.Commons.Security;
using Scholarship.Shared.Commons.Validator;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Scholarship.Service.Users.Infrastructure
{
    using static System.Runtime.InteropServices.JavaScript.JSType;
    using BCryptType = BCrypt.Net.BCrypt;
    public class UserService : IUserService
    {
        private readonly IDbContextFactory<UsersDbContext> contextFactory = default!;
        private readonly IMapper mapper = default!;
        private readonly ITokenService tokenService = default!;
        private readonly IModelValidator<RegistrationModel> registrationValidator = default!;

        public UserService(IDbContextFactory<UsersDbContext> contextFactory, 
            IMapper mapper, ITokenService tokenService, 
            IModelValidator<RegistrationModel> registrationValidator) : base() 
        {
            this.contextFactory = contextFactory;
            this.mapper = mapper;
            this.tokenService = tokenService;
            this.registrationValidator = registrationValidator;
        }
        protected virtual Claim[] GenerateClaims(UserModel model) => new Claim[]
        {
            new Claim(ClaimTypes.PrimarySid, model.Uuid.ToString()),
            new Claim(ClaimTypes.Role, model.Role),
            new Claim(ClaimTypes.Email, model.Email),
        };
        public async Task<UserModel?> GetUserByAccess(string accessToken)
        {
            var userClaims = await this.tokenService.VerifyAccessToken(accessToken);
            var userUuid = userClaims?.FirstOrDefault(item => item.Type == ClaimTypes.PrimarySid);

            ProcessException.ThrowIf(() => userClaims == null || userUuid == null, "Ошибка в валидации токена");
            using (var dbContext = await this.contextFactory.CreateDbContextAsync())
            {
                var userProfile = await dbContext.UserInfos.Include(item => item.Role)
                    .FirstOrDefaultAsync(item => item.Uuid == Guid.Parse(userUuid!.Value));
                if (userProfile == null)
                {
                    throw new ProcessException("Пользователь не найден");
                }
                return this.mapper.Map<UserModel>(userProfile);
            }
        }
        public async Task<IdentityModel?> GetTokensByRefresh(string refreshToken)
        {
            var userClaims = await this.tokenService.VerifyRefreshToken(refreshToken);
            var userUuid = userClaims?.FirstOrDefault(item => item.Type == ClaimTypes.PrimarySid);

            ProcessException.ThrowIf(() => userClaims == null || userUuid == null, "Ошибка в валидации токена");
            using (var dbContext = await this.contextFactory.CreateDbContextAsync())
            {
                var profile = await dbContext.RefreshTokens.Where(item => item.Token == refreshToken)
                    .Include(item => item.UserInfo)
                    .ThenInclude(item => item.Role)
                    .Where(item => item.UserInfo.Uuid == Guid.Parse(userUuid!.Value))
                    .Select(item => item.UserInfo).FirstOrDefaultAsync();

                if (profile == null) throw new ProcessException("Пользователь не найден");
                var profileClaims = this.GenerateClaims(this.mapper.Map<UserModel>(profile));
                var tokens = await this.tokenService.CreateJwtTokens(profileClaims);

                var identityInstance = this.mapper.Map<IdentityModel>(tokens);
                profile.RefreshToken.Token = identityInstance.RefreshToken;
                await dbContext.SaveChangesAsync();

                identityInstance.Role = profile.Role.Name;
                return identityInstance;
            }
        }
        public async Task<IdentityModel?> GetTokensByCredentials(CredentialsModel credentials)
        {
            var verifyPassword = (string hashPassword) =>
            {
                try { return BCryptType.Verify(credentials.Password, hashPassword, false, BCrypt.Net.HashType.SHA384); }
                catch (BCrypt.Net.SaltParseException) { return false; }
            };
            using (var dbContext = await this.contextFactory.CreateDbContextAsync())
            {
                var profilesList = await dbContext.UserInfos.Where(item => item.Email == credentials.Email)
                    .Include(item => item.RefreshToken).ToListAsync();

                var profile = profilesList.FirstOrDefault(item => 
                    item.Email == credentials.Email && verifyPassword(item.Password));
                if (profile == null) throw new ProcessException("Пользователь не найден");

                var profileClaims = this.GenerateClaims(this.mapper.Map<UserModel>(profile));
                var tokens = await this.tokenService.CreateJwtTokens(profileClaims);

                var identityInstance = this.mapper.Map<IdentityModel>(tokens);
                profile.RefreshToken.Token = identityInstance.RefreshToken;
                await dbContext.SaveChangesAsync();
                
                identityInstance.Role = profile.Role.Name;
                return identityInstance;
            }
        }
        public async Task<IdentityModel> Registration(RegistrationModel info)
        {
            var userRecord = this.mapper.Map<UserInfo>(info);
            await this.registrationValidator.CheckAsync(info);

            using (var dbContext = await this.contextFactory.CreateDbContextAsync())
            {
                var role = await dbContext.UserRoles.FirstOrDefaultAsync(item => item.Name == info.RoleName);
                if (role == null) throw new ProcessException("Роль не найдена");
                userRecord.Role = role;
                var profileClaims = this.GenerateClaims(this.mapper.Map<UserModel>(userRecord));

                var tokens = await this.tokenService.CreateJwtTokens(profileClaims);
                userRecord.RefreshToken.Token = tokens.RefreshToken;

                await dbContext.UserInfos.AddRangeAsync(userRecord);
                await dbContext.SaveChangesAsync();

                var identityInstance = this.mapper.Map<IdentityModel>(tokens);
                identityInstance.Role = userRecord.Role.Name;
                return identityInstance;
            }
        }
        public async Task<bool> IsEmpty()
        {
            using var dbContext = await this.contextFactory.CreateDbContextAsync();
            return !(await dbContext.UserInfos.AnyAsync());
        }
        public async Task<bool> IsUserExist(Guid uuid)
        {
            using var dbContext = await this.contextFactory.CreateDbContextAsync();
            return (await dbContext.UserInfos.AnyAsync(item => item.Uuid == uuid));
        }
    }
}
