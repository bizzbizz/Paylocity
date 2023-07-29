using Api.DAL;
using Api.DAL.Mock;
using Api.Models;

namespace Api.Utils;

/// <summary>
/// We add all IoC registrations here.
/// Extension looks more consistent with the rest of the builder code in program.
/// RegisterAllDependencies will take care of everything including dev/prod registration.
/// </summary>
public static class ContainerExtensions
{
    public static void RegisterAllDependencies(this WebApplicationBuilder app)
    {
        RegisterCommonDependencies(app.Services);

        if (app.Environment.IsProduction())
        {
            RegisterProductionDependencies(app.Services);
        }
        else if (app.Environment.IsDevelopment())
        {
            RegisterDevelopmentDependencies(app.Services);
        }
    }

    private static void RegisterCommonDependencies(IServiceCollection services)
    {
        services.AddSingleton<IApiResponseUtils, ApiResponseUtils>();
    }

    private static void RegisterProductionDependencies(IServiceCollection services)
    {
        throw new NotImplementedException("We need real database here");
    }

    private static void RegisterDevelopmentDependencies(IServiceCollection services)
    {
        services.AddSingleton<ITable<Employee>, EmployeeMockTable>();
        services.AddSingleton<ITable<Dependent>, DependentMockTable>();
    }
}
