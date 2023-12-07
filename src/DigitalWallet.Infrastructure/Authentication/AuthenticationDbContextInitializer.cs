using DigitalWallet.Domain.Authentication.Aggregates.UserAggregate;
using DigitalWallet.Domain.Authentication.Aggregates.UserAggregate.Entities;
using DigitalWallet.Domain.Common;
using DigitalWallet.Infrastructure.Common.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DigitalWallet.Infrastructure.Authentication;

public sealed class AuthenticationDbContextInitializer : IInitializer
{
    private readonly ILogger<AuthenticationDbContextInitializer> _logger;
    private readonly AuthenticationDbContext _context;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public AuthenticationDbContextInitializer(
        ILogger<AuthenticationDbContextInitializer> logger,
        AuthenticationDbContext context,
        RoleManager<ApplicationRole> roleManager,
        UserManager<ApplicationUser> userManager)
    {
        _logger = logger;
        _context = context;
        _roleManager = roleManager;
        _userManager = userManager;
    }

    public async Task InitializeAsync()
    {
        try
        {
            if (_context.Database.IsNpgsql())
                await _context.Database.MigrateAsync();

            await SeedDataAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initializing the database.");
            throw;
        }
    }

    private async Task SeedDataAsync()
    {
        await TryAddRole(Roles.SuperAdmin);
        await TryAddRole(Roles.Admin);
        await TryAddRole(Roles.Member);

        await TryAddSuperAdminAccount();
    }

    private async Task TryAddSuperAdminAccount()
    {
        string userName = "DigitalWalletSuperAdmin";
        string password = "AcK5c+7599C)";
        var user = ApplicationUser.CreateNew(
            userName,
            "RandomTestEmail@domain.com");

        var currentUser = await _userManager.FindByNameAsync(userName);

        if (currentUser is not null)
            return;

        var result = await _userManager.CreateAsync(user, password);

        if (!result.Succeeded)
            return;

        await _userManager.AddToRoleAsync(user, Roles.SuperAdmin);
    }

    private async Task TryAddRole(string roleName)
    {
        var role = ApplicationRole.CreateNew(roleName);
        var existingRole = await _roleManager.GetRoleNameAsync(role);

        if (existingRole is null)
            return;

        await _roleManager.CreateAsync(role);
    }
}
