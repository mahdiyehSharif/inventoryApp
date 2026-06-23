using Microsoft.EntityFrameworkCore;
using Entities.Security;
using Entities.Data;
using Microsoft.AspNetCore.Identity;
using Entities;

namespace InventoryApp.Data.Seed
{
    public static class DbSeeder
{
    public static async Task SeedPermissionsAsync(ApplicationDbContext context)
    {
        var allPermissions = Permissions.GetAll();

        foreach (var permission in allPermissions)
        {
            var exists = await context.Permissions
                .AnyAsync(p => p.Name == permission);

            if (!exists)
            {
                await context.Permissions.AddAsync(new Permission
                {
                    Name = permission,
                    Group = permission.Split('.')[0]  
                });
            }
        }

        await context.SaveChangesAsync();
    }
    public static async Task SeedRolesAndPermissionsAsync(
    ApplicationDbContext context,
    RoleManager<AppRole> roleManager)
{
    var rolePermissions = new Dictionary<string, List<string>>
    {
        ["Admin"] = Permissions.GetAll().ToList(),

        ["WarehouseManager"] = new()
        {
            Permissions.Product.View,
            Permissions.Inventory.View,
            Permissions.Inventory.RegisterEntry,
            Permissions.Inventory.RegisterExit,
            Permissions.Order.ViewAll,
            Permissions.Order.Approve,
            Permissions.Order.Reject,
            Permissions.Report.Inventory,
            Permissions.Report.Transaction,
        },

        ["Personnel"] = new()
        {
            Permissions.Product.View,
            Permissions.Order.View,
            Permissions.Order.Create,
        },
    };

    foreach (var (roleName, permissions) in rolePermissions)
    {
        if (!await roleManager.RoleExistsAsync(roleName))
            await roleManager.CreateAsync(new AppRole(roleName));

        var role = await roleManager.FindByNameAsync(roleName);

        foreach (var permissionName in permissions)
        {
            var permission = await context.Permissions
                .FirstOrDefaultAsync(p => p.Name == permissionName);

            if (permission == null) continue;

            var exists = await context.RolePermissions
                .AnyAsync(rp => rp.RoleId == role.Id && rp.PermissionId == permission.ID);

            if (!exists)
            {
                context.RolePermissions.Add(new RolePermission
                {
                    RoleId = role.Id,
                    PermissionId = permission.ID
                });
            }
        }
    }

    await context.SaveChangesAsync();
}
}
}