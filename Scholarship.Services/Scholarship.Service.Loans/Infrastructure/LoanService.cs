﻿using AutoMapper;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Scholarship.Database.Loans.Context;
using Scholarship.Database.Loans.Entities;
using Scholarship.Service.Loans.Models;
using Scholarship.Shared.Commons.Exceptions;
using Scholarship.Shared.Commons.Validator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholarship.Service.Loans.Infrastructure
{
    internal class LoanService : ILoanService
    {
        private readonly IDbContextFactory<LoansDbContext> contextFactory = default!;
        private readonly IMapper mapper = default!;
        private readonly IPublishEndpoint publisher = default!;
        private readonly IModelValidator<CreateLoanModel> createLoanValidator = default!;
        private readonly IModelValidator<CloseLoanModel> closeLoanValidator = default!;
        public LoanService(IDbContextFactory<LoansDbContext> factory, IMapper mapper,
            IPublishEndpoint publisher,
            IModelValidator<CreateLoanModel> createLoanValidator,
            IModelValidator<CloseLoanModel> closeLoanValidator) : base()
        {
            this.contextFactory = factory;
            this.mapper = mapper;
            this.publisher = publisher;
            this.createLoanValidator = createLoanValidator;
            this.closeLoanValidator = closeLoanValidator;
        }
        public async Task<bool> CheckLoanOwner(Guid loanUuid, Guid userUuid)
        {
            using var dbContext = await this.contextFactory.CreateDbContextAsync();

            var loan = await dbContext.Loans.FirstOrDefaultAsync(item => item.Uuid == loanUuid);
            return loan == null 
                ? throw new ProcessException("Loan record not found")
                : loan.ClientUuid == userUuid;
        }
        public async Task CloseLoan(CloseLoanModel loanInfo)
        {
            await this.closeLoanValidator.CheckAsync(loanInfo);
            using (var dbContext = await this.contextFactory.CreateDbContextAsync())
            {
                var record = await dbContext.Loans.FirstOrDefaultAsync(item => item.Uuid 
                    == loanInfo.LoanUuid);
                if (record == null) throw new ProcessException("Loan record not found");

                record.CloseTime = loanInfo.CloseTime;
                await dbContext.SaveChangesAsync();
            }
        }
        public async Task CreateLoan(CreateLoanModel loanInfo)
        {
            await this.createLoanValidator.CheckAsync(loanInfo);
            using (var dbContext = await this.contextFactory.CreateDbContextAsync())
            {
                await dbContext.Loans.AddRangeAsync(this.mapper.Map<LoanInfo>(loanInfo));
                await dbContext.SaveChangesAsync();
            }
        }
        public async Task<List<LoanModel>> GetAllLoans()
        {
            using var dbContext = await this.contextFactory.CreateDbContextAsync();
            return this.mapper.Map<List<LoanModel>>(await dbContext.Loans.ToListAsync());
        }
        public async Task<List<LoanModel>> GetLoansByUser(Guid clientUuid)
        {
            using var dbContext = await this.contextFactory.CreateDbContextAsync();
            var loanRecords = await dbContext.Loans.Where(item => item.ClientUuid == clientUuid)
                .ToListAsync();
            return this.mapper.Map<List<LoanModel>>(loanRecords);
        }
        public async Task RewriteAllLoans(List<RewriteLoanModel> loansList)
        {
            using var dbContext = await this.contextFactory.CreateDbContextAsync();
            await dbContext.Database.ExecuteSqlRawAsync(@"TRUNCATE TABLE ""LoanInfo"" CASCADE;");
            await dbContext.Loans.AddRangeAsync(this.mapper.Map<List<LoanInfo>>(loansList));
            await dbContext.SaveChangesAsync();
        }
    }
}
