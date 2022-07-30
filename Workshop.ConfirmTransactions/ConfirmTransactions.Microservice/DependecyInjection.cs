using Microsoft.EntityFrameworkCore;

namespace ConfirmTransactions.Microservice
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddInMemoryDB(this IServiceCollection services)
        {
            services.AddDbContext<transactionconfirmationdbContext>(options =>
            options.UseInMemoryDatabase("TransactionConfirmationInMemosryDB"));

            return services;
        }
    }
}
