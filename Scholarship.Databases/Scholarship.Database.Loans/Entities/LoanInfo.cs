using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Scholarship.Database.Loans.Configurations;

namespace Scholarship.Database.Loans.Entities
{
    [EntityTypeConfiguration(typeof(LoanConfiguration))]
    public class LoanInfo : object
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int Id { get; set; }

        [Required]
        public virtual Guid Uuid { get; set; } = Guid.NewGuid();

        [Required]
        public virtual Guid ClientUuid { get; set; } = Guid.Empty;

        public double MoneyAmount { get; set; } = default!;

        public DateTime OpenTime { get; set; } = default!;
        public DateTime BeforeTime { get; set; } = default!;

        public DateTime? CloseTime { get; set; } = default!;

        public string CreditorFIO { get; set; } = string.Empty;
    }
}
