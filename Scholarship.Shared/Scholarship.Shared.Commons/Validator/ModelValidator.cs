namespace Scholarship.Shared.Commons.Validator;

using FluentValidation;
using FluentValidation.Results;

public class ModelValidator<TModel> : IModelValidator<TModel> where TModel : class
{
    private readonly IValidator<TModel> validator;
    public ModelValidator(IValidator<TModel> validator) : base() => this.validator = validator;

    protected virtual string GetErrorMessage(List<ValidationFailure> errors) => errors.First().ErrorMessage;
    public void Check(TModel model)
    {
        var result = validator.Validate(model);
        if (!result.IsValid) throw new ValidationException(this.GetErrorMessage(result.Errors));
    }
    public async Task CheckAsync(TModel model)
    {
        var result = await validator.ValidateAsync(model);
        if (!result.IsValid) throw new ValidationException(this.GetErrorMessage(result.Errors));
    }
}