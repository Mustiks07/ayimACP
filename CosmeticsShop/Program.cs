using CosmeticsShop.Domain;
using CosmeticsShop.Domain.Repositories.Abstract;
using CosmeticsShop.Domain.Repositories.EntityFramework;
using CosmeticsShop.Infrastructure;
using CosmeticsShop.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>(opts =>
    {
        opts.Password.RequireDigit           = true;
        opts.Password.RequireLowercase       = true;
        opts.Password.RequireUppercase       = true;
        opts.Password.RequireNonAlphanumeric = true;
        opts.Password.RequiredLength         = 6;
    })
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath        = "/account/login";
    options.AccessDeniedPath = "/account/accessdenied";
    options.Events.OnRedirectToLogin = ctx =>
    {
        ctx.Response.Redirect(
            ctx.Request.Path.StartsWithSegments("/admin")
                ? "/admin/account/login"
                : $"/account/login?ReturnUrl={Uri.EscapeDataString(ctx.Request.Path)}");
        return Task.CompletedTask;
    };
    options.Events.OnRedirectToAccessDenied = ctx =>
    {
        ctx.Response.Redirect(
            ctx.Request.Path.StartsWithSegments("/admin")
                ? "/admin/account/login"
                : "/account/accessdenied");
        return Task.CompletedTask;
    };
});

builder.Services.AddScoped<IProductsRepository,    EFProductsRepository>();
builder.Services.AddScoped<IBrandsRepository,      EFBrandsRepository>();
builder.Services.AddScoped<ICategoriesRepository,  EFCategoriesRepository>();
builder.Services.AddScoped<IReviewsRepository,     EFReviewsRepository>();
builder.Services.AddScoped<ICartRepository,        EFCartRepository>();
builder.Services.AddScoped<IOrdersRepository,      EFOrdersRepository>();
builder.Services.AddScoped<DataManager>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<LangService>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// Seed admin
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
    await DbInitializer.SeedAdminAsync(scope.ServiceProvider);
}

// Admin routes (order matters - more specific first)
app.MapControllerRoute(
    name: "admin_login",
    pattern: "admin/account/{action=Login}",
    defaults: new { controller = "AdminAccount" });

app.MapControllerRoute(
    name: "admin_products",
    pattern: "admin/products/{action=Index}/{id?}",
    defaults: new { controller = "AdminProducts" });

app.MapControllerRoute(
    name: "admin_orders",
    pattern: "admin/orders/{action=Index}/{id?}",
    defaults: new { controller = "AdminOrders" });

app.MapControllerRoute(
    name: "admin_reviews",
    pattern: "admin/reviews/{action=Index}/{id?}",
    defaults: new { controller = "AdminReviews" });

app.MapControllerRoute(
    name: "admin_brands",
    pattern: "admin/brands/{action=Index}/{id?}",
    defaults: new { controller = "AdminBrands" });

app.MapControllerRoute(
    name: "admin_categories",
    pattern: "admin/categories/{action=Index}/{id?}",
    defaults: new { controller = "AdminCategories" });

app.MapControllerRoute(
    name: "admin",
    pattern: "admin/{action=Index}/{id?}",
    defaults: new { controller = "AdminCore" });

// Public routes
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
