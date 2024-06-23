using AutoMapper;
using Scholarship.Database.Users.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholarship.Service.Users.Models
{
    public class CredentialsModel : object
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class CredentialsModelProfile : Profile
    {
        public CredentialsModelProfile() : base() => base.CreateMap<CredentialsModel, UserInfo>();
    }
}
