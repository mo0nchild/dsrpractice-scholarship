using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Scholarship.Database.Loans.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholarship.Database.Loans.Configurations
{
    internal class LoanConfiguration : IEntityTypeConfiguration<LoanInfo>
    {
        public LoanConfiguration() : base() { }
        public void Configure(EntityTypeBuilder<LoanInfo> builder)
        {
            builder.ToTable(nameof(LoanInfo), "public");

            builder.HasIndex(item => item.Id).IsUnique();
            builder.HasIndex(item => item.Uuid).IsUnique();

            builder.Property(item => item.CloseTime).IsRequired(false);
            builder.Property(item => item.CreditorFIO).HasMaxLength(255);
        }
    }
}
