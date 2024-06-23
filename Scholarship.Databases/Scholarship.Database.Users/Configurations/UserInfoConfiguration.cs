using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Scholarship.Database.Users.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholarship.Database.Users.Configurations
{
    internal class UserInfoConfiguration : IEntityTypeConfiguration<UserInfo>
    {
        public UserInfoConfiguration() : base() { }
        public void Configure(EntityTypeBuilder<UserInfo> builder)
        {
            builder.ToTable(nameof(UserInfo), "public");

            builder.HasIndex(item => item.Uuid).IsUnique();
            builder.HasIndex(item => item.Id).IsUnique();
            builder.HasIndex(item => item.Email).IsUnique();

            builder.Property(item => item.Name).HasMaxLength(50);
            builder.Property(item => item.Role).HasMaxLength(50);

            builder.Property(item => item.Email).HasMaxLength(100);
            builder.Property(item => item.Password).HasMaxLength(255);
        }
    }
}
