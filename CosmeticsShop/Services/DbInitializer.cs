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
            // Тональный крем — флаконы с тоналкой
            [3]  = "https://images.unsplash.com/photo-1512496015851-a90fb38ba796?w=600&fit=crop",
            // Мицеллярная вода — бутылочка средства для умывания
            [6]  = "https://images.unsplash.com/photo-1556228578-8c89e6adf883?w=600&fit=crop",
            // SPF-крем — флакон солнцезащитного крема
            [7]  = "https://images.unsplash.com/photo-1583241800698-e8ab01830a24?w=600&fit=crop",
            // Шампунь — флакон шампуня
            [8]  = "https://images.unsplash.com/photo-1535585209827-a15fcdbc4c2d?w=600&fit=crop",
            // Маска для волос — баночка маски
            [9]  = "https://images.unsplash.com/photo-1598440947619-2c35fc9aa908?w=600&fit=crop",
            // Парфюм Miss Dior — флакон духов
            [10] = "https://images.unsplash.com/photo-1619451334792-150fd785ee74?w=600&fit=crop",
            // Парфюм Chanel No.5 — флакон духов
            [11] = "https://images.unsplash.com/photo-1583241801015-2c4bf8e79d30?w=600&fit=crop",
            // Гель для душа — тюбик/флакон
            [13] = "https://images.unsplash.com/photo-1571019613454-1cb2f99b2d8b?w=600&fit=crop",
            // Хайлайтер — палетка макияжа
            [17] = "https://images.unsplash.com/photo-1596704017254-9b5e2a025acf?w=600&fit=crop",
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
