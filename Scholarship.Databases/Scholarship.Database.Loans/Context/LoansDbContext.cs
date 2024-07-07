using Microsoft.EntityFrameworkCore;
using Scholarship.Database.Loans.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholarship.Database.Loans.Context
{
    public class LoansDbContext : DbContext
    {
        public virtual DbSet<LoanInfo> Loans { get; set; } = default!;
        public LoansDbContext(DbContextOptions<LoansDbContext> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder.UseLazyLoadingProxies());
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
