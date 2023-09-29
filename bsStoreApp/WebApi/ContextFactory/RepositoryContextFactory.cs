using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Repositories.EfCore;

namespace WebApi.ContextFactory
{
    public class RepositoryContextFactory :IDesignTimeDbContextFactory<RepositoryContext>
    {
        public RepositoryContext CreateDbContext(string[] args)
        {
            //configuration
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();

            //DbContextOptionsBuilder

            var builder = new DbContextOptionsBuilder<RepositoryContext>().UseSqlServer(
                    configuration.GetConnectionString("sqlConnection"),prj => prj.MigrationsAssembly("WebApi"));

            return new RepositoryContext(builder.Options);
        }
    }
}
