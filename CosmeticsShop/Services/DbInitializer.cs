using CosmeticsShop.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CosmeticsShop.Services;

public static class DbInitializer
{
    public static async Task SeedAdminAsync(IServiceProvider services)
    {
        var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

        const string adminRole = "Admin";
        const string userRole  = "User";
        const string adminEmail    = "admin";
        const string adminPassword = "Admin@Beauty2026!";

        if (!await roleManager.RoleExistsAsync(adminRole))
            await roleManager.CreateAsync(new IdentityRole(adminRole));

        if (!await roleManager.RoleExistsAsync(userRole))
            await roleManager.CreateAsync(new IdentityRole(userRole));

        var admin = await userManager.FindByNameAsync(adminEmail);
        if (admin == null)
        {
            admin = new IdentityUser { UserName = adminEmail, Email = adminEmail + "@beauty.shop", EmailConfirmed = true };
            var result = await userManager.CreateAsync(admin, adminPassword);
            if (result.Succeeded)
                await userManager.AddToRoleAsync(admin, adminRole);
        }
        else
        {
            if (!await userManager.IsInRoleAsync(admin, adminRole))
                await userManager.AddToRoleAsync(admin, adminRole);

            var token = await userManager.GeneratePasswordResetTokenAsync(admin);
            await userManager.ResetPasswordAsync(admin, token, adminPassword);
        }
    }

    public static async Task FixProductImagesAsync(AppDbContext db)
    {
        var fixes = new Dictionary<int, string>
        {
            [3]  = "https://images.unsplash.com/photo-1512496015851-a90fb38ba796?w=600&fit=crop",
            [6]  = "https://images.unsplash.com/photo-1556228578-8c89e6adf883?w=600&fit=crop",
            [7]  = "https://images.unsplash.com/photo-1526758097130-bab247274f58?w=600&fit=crop",
            [10] = "https://images.unsplash.com/photo-1541643600914-78b084683702?w=600&fit=crop",
            [11] = "https://images.unsplash.com/photo-1557170334-a9086a2b4282?w=600&fit=crop",
            [13] = "https://images.unsplash.com/photo-1571019613454-1cb2f99b2d8b?w=600&fit=crop",
        };

        var ids = fixes.Keys.ToList();
        var products = await db.Products.Where(p => ids.Contains(p.Id)).ToListAsync();
        bool changed = false;
        foreach (var p in products)
        {
            if (fixes.TryGetValue(p.Id, out var newUrl) && p.ImageUrl != newUrl)
            {
                p.ImageUrl = newUrl;
                changed = true;
            }
        }
        if (changed) await db.SaveChangesAsync();
    }
}
