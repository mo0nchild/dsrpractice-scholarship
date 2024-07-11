using AutoMapper;
using FluentValidation;
using MassTransit;
using Scholarship.Database.Loans.Entities;
using Scholarship.Shared.Commons.Exceptions;
using Scholarship.Shared.Messages.UsersMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholarship.Service.Loans.Models
{
    public class CreateLoanModel : object
    {
        public Guid ClientUuid { get; set; } = Guid.Empty;
        public double MoneyAmount { get; set; } = default!;

        public DateOnly OpenTime { get; set; } = default!;
        public DateOnly BeforeTime { get; set; } = default!;

        public string CreditorSurname { get; set; } = string.Empty;
        public string CreditorName { get; set; } = string.Empty;
        public string CreditorPatronymic { get; set; } = string.Empty;
    }
    public class CreateLoanModelProfile : Profile
    {
        public CreateLoanModelProfile() : base() => base.CreateMap<CreateLoanModel, LoanInfo>()
            .ForMember(item => item.Uuid, options => options.MapFrom(p => Guid.NewGuid()));
    }

    public class CreateLoanModelValidator : AbstractValidator<CreateLoanModel>
    {
        public CreateLoanModelValidator(IRequestClient<UserExistsRequest> requestClient) : base()
        {
            this.RuleFor(item => item.ClientUuid)
                .NotEmpty().WithMessage("Client Uuid required")
                .Must(item =>
                {
                    var response = requestClient.GetResponse<UserExistsResponse>(new UserExistsRequest()
                    {
                        UserUuid = item
                    }).Result;
                    ProcessException.ThrowIf(() => response == null, "Failed to verify user");
                    return response.Message.Exists;
                })
                .WithMessage("User is not found");
            this.RuleFor(item => item.OpenTime)
                .NotEmpty().WithMessage("Loan date must be set")
                .Must((model, item) => item <= model.BeforeTime)
                .WithMessage("The start date must be earlier than the estimated date");
            this.RuleFor(item => item.CreditorSurname)
                .NotEmpty().WithMessage("The creditor's name is required")
                .Length(3, 100).WithMessage("The length of the creditor's last name is from 3 to 100 characters");
            this.RuleFor(item => item.CreditorName)
                .NotEmpty().WithMessage("Creditor's name is required")
                .Length(3, 100).WithMessage("Creditor's name length from 3 to 100 characters");
            this.RuleFor(item => item.CreditorPatronymic)
                .NotEmpty().WithMessage("The lender's middle name must be indicated")
                .Length(3, 100).WithMessage("The length of the lender's middle name is from 3 to 100 characters");
        }
    }
}
