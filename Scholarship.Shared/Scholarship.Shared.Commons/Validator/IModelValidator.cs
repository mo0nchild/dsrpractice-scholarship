namespace Scholarship.Shared.Commons.Validator;

public interface IModelValidator<TModel> where TModel : class
{
    public void Check(TModel model);
    public Task CheckAsync(TModel model);
}