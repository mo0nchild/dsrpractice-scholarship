using AutoMapper;
using Scholarship.Services.Tokens.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholarship.Service.Users.Models
{
    public class IdentityModel : object
    {
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;
    }
    public class IdentityModelProfile : Profile
    {
        public IdentityModelProfile() : base() => base.CreateMap<TokensModel, IdentityModel>();
    }
}
