using RepoDb;

namespace BookStorage.Extensions.RepositoryExtensions;

public static class RepoDbExtensions
{
    public static void InitializeRepoDb(this IApplicationBuilder app)
    {
        //SqlServerBootstrap.Initialize();
        GlobalConfiguration.Setup().UseSqlServer();
        //SqlServerGlobalConfiguration.UseSqlServer(new GlobalConfiguration());
    }
}