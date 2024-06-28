using AutoMapper;
using Scholarship.Database.Users.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholarship.Service.Users.Models
{
    public class UserModel : object
    {
        public Guid Uuid { get; set; } = Guid.Empty;
        public string Role { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }

    public class UserModelProfile : Profile
    {
        public UserModelProfile() : base() => base.CreateMap<UserInfo, UserModel>()
            .ForMember(item => item.Role, options => options.MapFrom(p => p.Role.Name));
    }
}
