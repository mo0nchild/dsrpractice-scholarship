using FluentValidation;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Scholarship.Database.Loans.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholarship.Service.Loans.Models
{
    public class CloseLoanModel : object
    {
        public Guid LoanUuid { get; set; } = Guid.Empty;
        public DateOnly CloseTime { get; set; } = default!;
    }
    public class CloseLoanModelValidator : AbstractValidator<CloseLoanModel>
    {
        public CloseLoanModelValidator(IDbContextFactory<LoansDbContext> contextFactory, 
            IScopedClientFactory clientFactory) : base()
        {
            this.RuleFor(item => item.LoanUuid)
                .NotEmpty().WithMessage("Loan Uuid value required")
                .Must(item =>
                {
                    using var dbContext = contextFactory.CreateDbContext();
                    return dbContext.Loans.Any(p => p.Uuid == item);
                })
                .WithMessage("Loan record not found")
                .Must((model, item) =>
                {
                    using var dbContext = contextFactory.CreateDbContext();
                    var record = dbContext.Loans.FirstOrDefault(p => p.Uuid == item);
                    return record == null ? true : record.CloseTime == null;
                })
                .WithMessage("The loan record is already closed");
            this.RuleFor(item => item.CloseTime)
                .NotEmpty().WithMessage("Required closing date value")
                .Must((model, item) =>
                {
                    using var dbContext = contextFactory.CreateDbContext();
                    var record = dbContext.Loans.FirstOrDefault(item => item.Uuid == model.LoanUuid);

                    return record == null ? true : record.OpenTime <= item;
                })
                .WithMessage("Closing date must be later than opening date");
        }
    }
}
