using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Scholarship.Database.History.Context;
using Scholarship.Database.History.Entities;
using Scholarship.Service.History.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholarship.Service.History.Infrastructure
{
    internal class HistoryService : IHistoryService
    {
        private readonly IDbContextFactory<HistoryDbContext> contextFactory = default!;
        private readonly IMapper mapper = default!;
        public HistoryService(IDbContextFactory<HistoryDbContext> factory, IMapper mapper) : base()
        {
            this.mapper = mapper;
            this.contextFactory = factory;
        }
        public async Task AddLoanToHistory(ClosingLoanModel model)
        {
            var newRecord = this.mapper.Map<ClosedLoanInfo>(model);
            using (var dbContext = await this.contextFactory.CreateDbContextAsync())
            {
                await dbContext.ClosedLoans.AddAsync(newRecord);
                await dbContext.SaveChangesAsync();
            }
        }
        public async Task<bool> CheckLoanIsClosed(Guid loanUuid)
        {
            using var dbContext = await this.contextFactory.CreateDbContextAsync();
            return await dbContext.ClosedLoans.AnyAsync(item => item.Uuid == loanUuid);
        }
        public async Task<List<ClosedLoanModel>> GetAllClosedLoans()
        {
            using var dbContext = await this.contextFactory.CreateDbContextAsync();
            return this.mapper.Map<List<ClosedLoanModel>>(await dbContext.ClosedLoans.ToListAsync());
        }
        public async Task<List<ClosedLoanModel>> GetClosedLoansByUser(Guid uuid)
        {
            using (var dbContext = await this.contextFactory.CreateDbContextAsync())
            {
                var records = await dbContext.ClosedLoans.Where(item => item.ClientUuid == uuid).ToListAsync();
                return this.mapper.Map<List<ClosedLoanModel>>(records);
            }
        }
        public async Task RewriteAllClosedLoans(List<ClosedLoanModel> loansList)
        {
            using var dbContext = await this.contextFactory.CreateDbContextAsync();
            await dbContext.Database.ExecuteSqlRawAsync(@"TRUNCATE TABLE ""ClosedLoanInfo"" CASCADE;");

            await dbContext.ClosedLoans.AddRangeAsync(this.mapper.Map<List<ClosedLoanInfo>>(loansList));
            await dbContext.SaveChangesAsync();
        }
    }
}
