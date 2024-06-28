namespace Scholarship.Shared.Commons.Validator;

using FluentValidation;

public class ModelValidator<TModel> : IModelValidator<TModel> where TModel : class
{
    private readonly IValidator<TModel> validator;
    public ModelValidator(IValidator<TModel> validator) : base() => this.validator = validator;
    public void Check(TModel model)
    {
        var result = validator.Validate(model);
        if (!result.IsValid) throw new ValidationException(result.Errors);
    }
    public async Task CheckAsync(TModel model)
    {
        var result = await validator.ValidateAsync(model);
        if (!result.IsValid) throw new ValidationException(result.Errors);
    }
}