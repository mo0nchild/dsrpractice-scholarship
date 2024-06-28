using Microsoft.EntityFrameworkCore;
using Scholarship.Database.Users.Configurations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholarship.Database.Users.Entities
{
    [EntityTypeConfiguration(typeof(UserRoleConfiguration))]
    public class UserRole : object
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public virtual List<UserInfo> Users { get; set; } = new();
    }
}
