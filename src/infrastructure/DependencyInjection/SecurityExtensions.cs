using Doyep.Analyzer.Application.Security;
using Doyep.Analyzer.Infrastructure.Persistence;
using Doyep.Analyzer.Infrastructure.Security;

using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Doyep.Analyzer.Infrastructure;

public static class SecurityExtensions
{
    public static IServiceCollection AddSecurity(this IServiceCollection services)
    {
        services.AddDataProtection()
             .PersistKeysToDbContext<AnalyzerDbContext>()
             .SetApplicationName("DoyepAnalyzer");

        services.AddSingleton<IEncryptionService, DataProtectionEncryptionService>();

        services.AddOptions<StateOptions>()
            .BindConfiguration(StateOptions.SectionName)
            .ValidateOnStart();
        services.AddSingleton<IValidateOptions<StateOptions>, StateOptionsValidator>();
        services.AddScoped<IStateService, StateService>();

        return services;
    }
}
