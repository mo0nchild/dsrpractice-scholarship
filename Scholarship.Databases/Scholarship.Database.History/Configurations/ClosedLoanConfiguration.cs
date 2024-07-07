using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Scholarship.Database.History.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholarship.Database.History.Configurations
{
    public class ClosedLoanConfiguration : IEntityTypeConfiguration<ClosedLoanInfo>
    {
        public ClosedLoanConfiguration() : base() { }
        public void Configure(EntityTypeBuilder<ClosedLoanInfo> builder)
        {
            builder.ToTable(nameof(ClosedLoanInfo), "public");
            builder.HasIndex(item => item.Uuid).IsUnique();

            builder.Property(item => item.CreditorSurname).HasMaxLength(100);
            builder.Property(item => item.CreditorName).HasMaxLength(100);
            builder.Property(item => item.CreditorPatronymic).HasMaxLength(100);
        }
    }
}
