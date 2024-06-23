using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Scholarship.Database.Loans.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholarship.Database.Authorizations.Context
{
    internal class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<LoansDbContext>
    {
        public DesignTimeDbContextFactory() : base() { }
        public LoansDbContext CreateDbContext(string[] args)
        {
            var contextOptions = new DbContextOptionsBuilder<LoansDbContext>();
            contextOptions.UseNpgsql("Server=localhost;Port=5433;Username=postgres;Password=1234567890;Database=scholarship.loans");

            return new LoansDbContext(contextOptions.Options);
        }
    }
}
