using Microsoft.Extensions.DependencyInjection;
using Scholarship.Service.Loans.Infrastructure;

namespace Scholarship.Service.Loans
{
    public static class Bootstrapper : object
    {
        public static Task<IServiceCollection> AddLoanService(this IServiceCollection collection)
        {
            collection.AddTransient<ILoanService, LoanService>();
            return Task.FromResult(collection);
        }
    }
}
