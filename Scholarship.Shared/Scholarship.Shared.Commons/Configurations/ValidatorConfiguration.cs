namespace Scholarship.Shared.Commons.Configurations;

using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Scholarship.Shared.Commons.Helpers;
using Scholarship.Shared.Commons.Validator;

public static class ValidatorConfiguration : object
{
    public static Task<IServiceCollection> AddModelsValidators(this IServiceCollection collection)
    {
        //collection.AddFluentValidationAutoValidation(options =>
        //{
        //    options.DisableDataAnnotationsValidation = false,
        //});
        ValidatorsRegisterHelper.Register(collection);
        collection.AddScoped(typeof(IModelValidator<>), typeof(ModelValidator<>));

        return Task.FromResult(collection);
    }
}