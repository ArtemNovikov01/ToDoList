using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ToDoList.Application.Services;
using ToDoList.DataBase.Setting;

namespace ToDoList.DataBase;
public static class DependencyInjections
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, DatabaseSettings? databaseSettings)
    {
        if (databaseSettings?.ConnectionString is null)
        {
            throw new ArgumentNullException(nameof(databaseSettings), "Не заданы настройки БД");
        }

        services.AddDbContext<ToDoDbContext>(opt => opt.UseNpgsql(databaseSettings.ConnectionString));
        services.AddScoped<IToDoDbContext>(isp => isp.GetRequiredService<ToDoDbContext>());
        services.AddTransient<IToDoDbContext, ToDoDbContext>();

        return services;
    }
}
