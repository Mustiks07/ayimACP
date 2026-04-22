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
            // 1  Помада — макияж флэтлей
            [1]  = "https://images.unsplash.com/photo-1596462502278-27bfdc403348?w=600&fit=crop",
            // 2  Тушь — вотерлайн/ресницы
            [2]  = "https://images.unsplash.com/photo-1612817288484-6f916006741a?w=600&fit=crop",
            // 3  Тональный крем — флаконы с мейкап
            [3]  = "https://images.unsplash.com/photo-1596462502278-27bfdc403348?w=600&fit=crop",
            // 4  Сыворотка — пипетка с сывороткой
            [4]  = "https://images.unsplash.com/photo-1620916566398-39f1143ab7be?w=600&fit=crop",
            // 5  CeraVe крем — белый крем-джар
            [5]  = "https://images.unsplash.com/photo-1631730486572-226d1f595b68?w=600&fit=crop",
            // 6  Мицеллярная — тюбик на песке
            [6]  = "https://images.unsplash.com/photo-1556228578-8c89e6adf883?w=600&fit=crop",
            // 7  SPF крем — баночка крема
            [7]  = "https://images.unsplash.com/photo-1631730486572-226d1f595b68?w=600&fit=crop",
            // 8  Шампунь — флакон для волос
            [8]  = "https://images.unsplash.com/photo-1585747860715-2ba37e788b70?w=600&fit=crop",
            // 9  Маска для волос — баночка маски
            [9]  = "https://images.unsplash.com/photo-1535585209827-a15fcdbc4c2d?w=600&fit=crop",
            // 10 Miss Dior — флакон духов
            [10] = "https://images.unsplash.com/photo-1585386959984-a4155224a1ad?w=600&fit=crop",
            // 11 Chanel No.5 — флакон духов
            [11] = "https://images.unsplash.com/photo-1547887537-6158d64c35b3?w=600&fit=crop",
            // 12 Лак для ногтей — лак
            [12] = "https://images.unsplash.com/photo-1604654894610-df63bc536371?w=600&fit=crop",
            // 13 Гель для душа — флакон
            [13] = "https://images.unsplash.com/photo-1556228578-8c89e6adf883?w=600&fit=crop",
            // 14 Скраб для тела — баночка скраба
            [14] = "https://images.unsplash.com/photo-1620916566398-39f1143ab7be?w=600&fit=crop",
            // 15 Набор кистей — кисти для макияжа
            [15] = "https://images.unsplash.com/photo-1522338242992-e1a54906a8da?w=600&fit=crop",
            // 16 Консилер — макияж продукты
            [16] = "https://images.unsplash.com/photo-1596462502278-27bfdc403348?w=600&fit=crop",
            // 17 Хайлайтер — сияющий макияж
            [17] = "https://images.unsplash.com/photo-1512496015851-a90fb38ba796?w=600&fit=crop",
            // 18 Тоник — флакон тоника
            [18] = "https://images.unsplash.com/photo-1620916566398-39f1143ab7be?w=600&fit=crop",
            // 19 Крем для рук — баночка крема
            [19] = "https://images.unsplash.com/photo-1631730486572-226d1f595b68?w=600&fit=crop",
            // 20 Блеск для губ — лип продукт
            [20] = "https://images.unsplash.com/photo-1596462502278-27bfdc403348?w=600&fit=crop",
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
