using System.Security.Claims;
using CarWashing.Application.Abstractions.Identity;
using CarWashing.Presentation.WebAPI.Configuration;
using CarWashing.Presentation.WebAPI.Extensions;
using CarWashing.Presentation.WebAPI.Helpers;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilogForAppLogs(builder.Configuration);

WebApiConfiguration webApiConfiguration = new(builder.Configuration);
IConfigurationSection identityConfigurationSection = builder.Configuration.GetSection("Identity:IdentityConfiguration");

builder.Services.ConfigureServices(
    webApiConfiguration,
    identityConfigurationSection,
    builder.Configuration);

WebApplication app = builder.Build().Configure();

using (IServiceScope scope = app.Services.CreateScope()) {
    ClaimsPrincipal principal = new(new ClaimsIdentity(new[] {
        new Claim(ClaimTypes.Role, CarWashingIdentityRoleNames.AdminRoleName),
        new Claim(ClaimTypes.NameIdentifier, Guid.Empty.ToString()),
    }));

    ICurrentUserManager currentUserManager = scope.ServiceProvider.GetRequiredService<ICurrentUserManager>();
    currentUserManager.Authenticate(principal);

    await SeedingHelper.SeedRoles(scope.ServiceProvider);
    await SeedingHelper.SeedAdmins(scope.ServiceProvider, app.Configuration);
}

app.Logger.LogInformation("Initializing IronPDF renderer...");
Installation.Initialize();
app.Run();
