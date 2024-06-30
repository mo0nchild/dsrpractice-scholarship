using AutoMapper;
using FluentValidation;
using MassTransit;
using Scholarship.Database.Loans.Entities;
using Scholarship.Shared.Commons.Exceptions;
using Scholarship.Shared.Commons.TransitModels.UserExists;
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
                .NotEmpty().WithMessage("Необходимо значение Uuid клиента")
                .Must(item =>
                {
                    var response = requestClient.GetResponse<UserExistsResponse>(new UserExistsRequest()
                    {
                        UserUuid = Guid.NewGuid()
                    }).Result;
                    ProcessException.ThrowIf(() => response == null, "Не удалось проверить пользователя");
                    return response.Message.Exists;
                })
                .WithMessage("Пользователь не найден");
            this.RuleFor(item => item.OpenTime)
                .NotEmpty().WithMessage("Необходимо установить дату займа")
                .Must((model, item) => item <= model.BeforeTime)
                .WithMessage("Дата начала должна быть раньше ориентировочной даты");
            this.RuleFor(item => item.CreditorSurname)
                .NotEmpty().WithMessage("Необходимо указать фамилию кредитора")
                .Length(3, 100).WithMessage("Длина фамилии кредитора от 3 до 100 символов");
            this.RuleFor(item => item.CreditorName)
                .NotEmpty().WithMessage("Необходимо указать имя кредитора")
                .Length(3, 100).WithMessage("Длина имени кредитора от 3 до 100 символов");
            this.RuleFor(item => item.CreditorPatronymic)
                .NotEmpty().WithMessage("Необходимо указать отчество кредитора")
                .Length(3, 100).WithMessage("Длина отчества кредитора от 3 до 100 символов");
        }
    }
}
