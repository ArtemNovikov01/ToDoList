using ToDoList.Backend.Settings;
using ToDoList.DataBase;
using ToDoList.Application;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace ToDoList.Web;
public static class DependencyInjections
{
    public static WebApplicationBuilder BuildWebApplication(this WebApplicationBuilder builder)
    {
        var webAppSettings = builder.Configuration.Get<WebAppSettings>()
                             ?? throw new NullReferenceException("Не заданы настройки приложения");

        // Services
        builder.Services
            .AddApplication()
            .AddDatabase(webAppSettings.Database);

        builder.Services
            .AddMvc();

        // Swagger
        builder.Services
            .AddSwaggerGen();

        return builder;
    }

    public static WebApplication ConfigureWebApplication(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(opt => { opt.SwaggerEndpoint("v1/swagger.json", "ToDo Test"); });

        app.UseRouting();

        app.MapControllers();

        return app;
    }
}
