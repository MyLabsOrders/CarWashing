using Serilog;
using CarWashing.Presentation.Middlewares;

namespace CarWashing.Presentation.WebAPI.Extensions;

internal static class WebApplicationExtensions {
    internal static WebApplication Configure(this WebApplication app) {
        app
            .UseSwagger()
            .UseSwaggerUI();

        app
            .UseRouting()
            .UseAuthentication()
            .UseAuthorization();

        app.UseCors(o => o.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
        app.MapControllers();
        app.UseMiddleware<ExceptionHandlingMiddleware>();
        app.UseRequestLogging();

        return app;
    }

    private static void UseRequestLogging(this WebApplication app) {
        app.UseSerilogRequestLogging(o => {
            o.IncludeQueryInRequestPath = true;
            o.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.File(
                    Path.Join("Serilogs", "RequestLog_.log"),
                    outputTemplate: "{Timestamp:o} {Message}{NewLine}",
                    rollingInterval: RollingInterval.Day,
                    retainedFileCountLimit: 30)
                .CreateLogger();
        });
    }
}
