using MediatR;
using CarWashing.Application.Abstractions.Identity;
using CarWashing.Application.Contracts.Identity.Commands;
using CarWashing.Presentation.Contracts.Configuration;

namespace CarWashing.Presentation.WebAPI.Helpers;

internal static class SeedingHelper {
    internal static async Task SeedAdmins(IServiceProvider provider, IConfiguration configuration) {
        IMediator mediator = provider.GetRequiredService<IMediator>();
        ILogger<Program> logger = provider.GetRequiredService<ILogger<Program>>();
        IConfigurationSection adminsSection = configuration.GetSection("Identity:DefaultAdmins");
        AdminModel[] admins = adminsSection.Get<AdminModel[]>() ?? Array.Empty<AdminModel>();

        foreach (AdminModel admin in admins) {
            try {
                CreateAdmin.Command registerCommand = new(admin.Username, admin.Password);
                await mediator.Send(registerCommand);

                ChangeUserRole.Command changeRole = new(admin.Username, CarWashingIdentityRoleNames.AdminRoleName);
                await mediator.Send(changeRole);
            }
            catch (Exception ex) {
                logger.LogWarning(ex.Message,"Failed to register admin {Username}", admin.Username);
            }
        }
    }

    internal static async Task SeedRoles(IServiceProvider provider) {
        IAuthorizationService service = provider.GetRequiredService<IAuthorizationService>();
        ILogger<Program> logger = provider.GetRequiredService<ILogger<Program>>();
        try {
            await service.CreateRoleIfNotExistsAsync(CarWashingIdentityRoleNames.AdminRoleName);
            await service.CreateRoleIfNotExistsAsync(CarWashingIdentityRoleNames.DefaultUserRoleName);
        }
        catch (Exception ex) {
            logger.LogWarning(ex.Message, "Failed to seed roles");
        }
    }
}