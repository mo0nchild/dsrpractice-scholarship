using AutoMapper;
using Scholarship.Database.Users.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholarship.Service.Users.Models
{
    public class RewriteUserModel : object
    {
        public Guid Uuid { get; set; } = Guid.Empty;
        public string Email { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
        public string RoleName { get; set; } = string.Empty;
    }
    public class RewriteUserModelProfile : Profile
    {
        public RewriteUserModelProfile() : base() => this.CreateMap<RewriteUserModel, UserInfo>()
            .ReverseMap();
    }
}
