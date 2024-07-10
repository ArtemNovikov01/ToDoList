using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ToDoList.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services
          .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()))
          .AddMemoryCache();
    }
}
