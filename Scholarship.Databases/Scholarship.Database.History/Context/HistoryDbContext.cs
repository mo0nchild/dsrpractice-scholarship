using Microsoft.EntityFrameworkCore;
using Scholarship.Database.History.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholarship.Database.History.Context
{
    public class HistoryDbContext : DbContext
    {
        public virtual DbSet<ClosedLoanInfo> ClosedLoans { get; set; } = default!;
        public HistoryDbContext(DbContextOptions<HistoryDbContext> options) : base(options) { }
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
