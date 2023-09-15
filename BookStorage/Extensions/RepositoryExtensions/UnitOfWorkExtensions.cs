using BookStorage.Repositories.Base;

namespace BookStorage.Extensions.RepositoryExtensions
{
    public static class UnitOfWorkExtensions
    {
        public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            return services.AddTransient<IUnitOfWork, RepoDbUnitOfWork>();
        }
    }
}