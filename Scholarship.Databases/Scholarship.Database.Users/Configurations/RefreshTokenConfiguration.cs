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
    internal class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public RefreshTokenConfiguration() : base() { }
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.ToTable(nameof(RefreshToken), "public");
            builder.HasIndex(item => item.Uuid).IsUnique();

            builder.HasOne(item => item.UserInfo).WithOne(item => item.RefreshToken)
                .HasForeignKey((RefreshToken item) => item.UserInfoUuid)
                .HasPrincipalKey((UserInfo item) => item.Uuid)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
