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
    public class RewriteUserModel : UserModel
    {
        public string Password { get; set; } = string.Empty;
    }
    public class RewriteUserModelProfile : Profile
    {
        public RewriteUserModelProfile() : base() => this.CreateMap<RewriteUserModel, UserInfo>()
            .ReverseMap();
    }
}
