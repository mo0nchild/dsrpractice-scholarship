using AutoMapper;
using Scholarship.Service.Users.Models;
using Scholarship.Shared.Commons.Security;

namespace Scholarship.Api.Users.Models
{
    public class RegistrationRequest : object
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }
    public class RegistrationRequestProfile : Profile
    {
        public RegistrationRequestProfile() : base() => this.CreateMap<RegistrationRequest, RegistrationModel>()
            .ForMember(item => item.RoleName, options => options.MapFrom(p => IdentityRoleScopes.User));
    }
}
