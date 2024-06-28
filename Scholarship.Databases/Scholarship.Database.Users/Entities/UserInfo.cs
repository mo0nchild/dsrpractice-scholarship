using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Scholarship.Database.Users.Configurations;

namespace Scholarship.Database.Users.Entities
{
    [EntityTypeConfiguration(typeof(UserInfoConfiguration))]
    public class UserInfo : object
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int Id { get; set; }

        [Required]
        public virtual Guid Uuid { get; set; } = Guid.NewGuid();

        [Required]
        public string Email { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public int RoleId { get; set; } = default!;
        public virtual UserRole Role { get; set; } = default!;

        public virtual RefreshToken RefreshToken { get; set; } = new();
    }
}
