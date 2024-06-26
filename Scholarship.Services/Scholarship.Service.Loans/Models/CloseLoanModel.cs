﻿using FluentValidation;
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
        public CloseLoanModelValidator(IDbContextFactory<LoansDbContext> contextFactory) : base()
        {
            this.RuleFor(item => item.LoanUuid)
                .NotEmpty().WithMessage("Необходимо значение Uuid займа")
                .Must(item =>
                {
                    using var dbContext = contextFactory.CreateDbContext();
                    return dbContext.Loans.Any(p => p.Uuid == item);
                })
                .WithMessage("Запись займа не найдена")
                .Must((model, item) =>
                {
                    using var dbContext = contextFactory.CreateDbContext();
                    var record = dbContext.Loans.FirstOrDefault(item => item.Uuid == model.LoanUuid);
                    return record == null ? true : record.CloseTime == null;
                })
                .WithMessage("Запись о займе уже закрыта");
            this.RuleFor(item => item.CloseTime)
                .NotEmpty().WithMessage("Необходимо значение даты закрытия")
                .Must((model, item) =>
                {
                    using var dbContext = contextFactory.CreateDbContext();
                    var record = dbContext.Loans.FirstOrDefault(item => item.Uuid == model.LoanUuid);

                    return record == null ? true : record.BeforeTime <= item && record.OpenTime <= item;
                })
                .WithMessage("Дата закрытия должна быть позже даты открытия");
        }
    }
}
