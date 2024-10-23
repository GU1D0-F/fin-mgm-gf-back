using FinManagerGf.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FinManagerGf.Data
{
    /// <summary>
    /// https://github.com/dotnet/efcore/blob/main/src/EFCore/Extensions/EntityFrameworkServiceCollectionExtensions.cs
    /// https://github.com/dotnet/efcore/blob/main/src/EFCore/Internal/DbContextFactory.cs
    /// https://github.com/dotnet/efcore/blob/main/src/EFCore/Internal/DbContextFactorySource.cs
    /// </summary>
    /// <param name="serviceProvider"></param>
    /// <param name="options"></param>
    /// <param name="factorySource"></param>
    public class PostgresDbContextFactory(
        IServiceProvider serviceProvider) : IDbContextFactory<CoreDbContext>
    {
        public CoreDbContext CreateDbContext() => 
            ActivatorUtilities.CreateInstance<PostgresDbContext>(serviceProvider, Type.EmptyTypes);

        public Task<CoreDbContext> CreateDbContextAsync() => Task.FromResult(CreateDbContext());
    }
}
