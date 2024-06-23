using Scholarship.Service.Users.Models;

namespace Scholarship.Service.Users.Infrastructure
{
    public interface IUserService
    {
        public Task<IdentityModel?> GetTokensByRefresh(string refreshToken);
        public Task<IdentityModel?> GetTokensByCredentials(CredentialsModel credentials);
        public Task<UserModel?> GetUserByAccess(string accessToken);

        public Task<IdentityModel> Registration(RegistrationModel info);
    }
}
