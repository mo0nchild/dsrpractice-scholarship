using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Scholarship.Database.Users.Context;
using Scholarship.Database.Users.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholarship.Service.Users.Models
{
    using BCryptType = BCrypt.Net.BCrypt;
    public class RegistrationModel : object
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string RoleName { get; set; } = string.Empty;
    }
    public class RegistrationModelProfile : Profile
    {
        public RegistrationModelProfile() : base()
        {
            base.CreateMap<RegistrationModel, UserInfo>()
                .ForMember(item => item.Email, options => options.MapFrom(p => p.Email))
                .ForMember(item => item.Name, options => options.MapFrom(p => p.Name))
                .ForMember(item => item.Password, options =>
                {
                    options.MapFrom(p => BCryptType.HashPassword(p.Password));
                })
                .ForMember(item => item.Uuid, options => options.MapFrom(p => Guid.NewGuid()))
                .ForMember(item => item.RefreshToken, options => options.MapFrom(p => new RefreshToken()
                {
                    Uuid = Guid.NewGuid()
                }));
            base.CreateMap<RegistrationModel, UserModel>()
                .ForMember(item => item.RoleName, options => options.MapFrom(p => p.RoleName));
        }
    }
    public class RegistrationModelValidator : AbstractValidator<RegistrationModel>
    {
        public RegistrationModelValidator(IDbContextFactory<UsersDbContext> contextFactory) : base()
        {
            base.RuleFor(item => item.Email)
                .NotEmpty().WithMessage("The mail value cannot be empty")
                .EmailAddress().WithMessage("Invalid mail format")
                .Must(item =>
                {
                    using var context = contextFactory.CreateDbContext();
                    var profile = context.UserInfos.FirstOrDefault(op => op.Email == item);
                    return profile == null;
                }).WithMessage("User is already registered");
            base.RuleFor(item => item.Name)
                .NotEmpty().WithMessage("The name value cannot be empty")
                .Length(3, 50).WithMessage("Name length between 3 and 50 characters");
            base.RuleFor(item => item.RoleName)
                .NotEmpty().WithMessage("Role value cannot be empty")
                .Must(item =>
                {
                    using var context = contextFactory.CreateDbContext();
                    var roleFound = context.UserRoles.FirstOrDefault(op => op.Name == item);
                    return roleFound != null;
                }).WithMessage("User role not found");
        }
    }
}
