using Microsoft.EntityFrameworkCore;
using Scholarship.Database.Users.Configurations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholarship.Database.Users.Entities
{
    [EntityTypeConfiguration(typeof(RefreshTokenConfiguration))]
    public class RefreshToken : object
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual Guid Uuid { get; set; } = Guid.NewGuid();

        public string Token { get; set; } = string.Empty;

        public Guid UserInfoUuid { get; set; } = default!;
        public virtual UserInfo UserInfo { get; set; } = default!;
    }
}
