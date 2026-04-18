using Doyep.Analyzer.Application.Security;
using Doyep.Analyzer.Infrastructure.Persistence;
using Doyep.Analyzer.Infrastructure.Security;

using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;

namespace Doyep.Analyzer.Infrastructure;

public static class SecurityExtensions
{
    public static IServiceCollection AddSecurity(this IServiceCollection services)
    {
        services.AddDataProtection()
             .PersistKeysToDbContext<AnalyzerDbContext>()
             .SetApplicationName("DoyepAnalyzer");

        services.AddSingleton<IEncryptionService, DataProtectionEncryptionService>();

        return services;
    }
}
