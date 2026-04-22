using CosmeticsShop.Domain.Entities;
using CosmeticsShop.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CosmeticsShop.Domain;

public class AppDbContext : IdentityDbContext<IdentityUser>
{
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<ReviewVote> ReviewVotes { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Product>()
            .Property(p => p.Price)
            .HasColumnType("decimal(18,2)");

        builder.Entity<Order>()
            .Property(o => o.Total)
            .HasColumnType("decimal(18,2)");

        builder.Entity<OrderItem>()
            .Property(oi => oi.PriceAtPurchase)
            .HasColumnType("decimal(18,2)");

        // Ignore computed properties
        builder.Entity<Product>().Ignore(p => p.AverageRating).Ignore(p => p.ReviewCount);
        builder.Entity<Cart>().Ignore(c => c.Total);

        // Seed Categories
        builder.Entity<Category>().HasData(
            new Category { Id = 1, Title = "Уход за лицом", IconEmoji = "💧" },
            new Category { Id = 2, Title = "Декоративная косметика", IconEmoji = "💄" },
            new Category { Id = 3, Title = "Уход за волосами", IconEmoji = "💇" },
            new Category { Id = 4, Title = "Парфюмерия", IconEmoji = "🌸" },
            new Category { Id = 5, Title = "Уход за телом", IconEmoji = "🧴" },
            new Category { Id = 6, Title = "Средства для ногтей", IconEmoji = "💅" },
            new Category { Id = 7, Title = "Солнцезащитные", IconEmoji = "☀️" },
            new Category { Id = 8, Title = "Инструменты и кисти", IconEmoji = "🖌️" }
        );

        // Seed Brands
        builder.Entity<Brand>().HasData(
            new Brand { Id = 1, Title = "L'Oréal Paris", Country = "Франция", IsVerified = true, Description = "Ведущий мировой бренд красоты" },
            new Brand { Id = 2, Title = "Maybelline", Country = "США", IsVerified = true, Description = "Инновационная косметика из Нью-Йорка" },
            new Brand { Id = 3, Title = "MAC Cosmetics", Country = "Канада", IsVerified = true, Description = "Профессиональная косметика для всех" },
            new Brand { Id = 4, Title = "The Ordinary", Country = "Канада", IsVerified = true, Description = "Клинически доказанные формулы" },
            new Brand { Id = 5, Title = "CeraVe", Country = "США", IsVerified = true, Description = "Разработано с дерматологами" },
            new Brand { Id = 6, Title = "Dior Beauty", Country = "Франция", IsVerified = true, Description = "Роскошь и элегантность Парижа" },
            new Brand { Id = 7, Title = "Chanel Beauty", Country = "Франция", IsVerified = true, Description = "Икона французской моды и красоты" },
            new Brand { Id = 8, Title = "Nivea", Country = "Германия", IsVerified = true, Description = "Уход за кожей с 1911 года" },
            new Brand { Id = 9, Title = "Garnier", Country = "Франция", IsVerified = true, Description = "Натуральная красота" },
            new Brand { Id = 10, Title = "NYX Professional", Country = "США", IsVerified = true, Description = "Профессиональный макияж для всех" }
        );

        // Seed Products
        var now = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        builder.Entity<Product>().HasData(
            new Product
            {
                Id = 1, Title = "Помада матовая Rouge", Description = "Стойкая матовая помада с насыщенным цветом. Формула обогащена гиалуроновой кислотой для комфортного нанесения.",
                Price = 4500, ImageUrl = "https://images.unsplash.com/photo-1596462502278-27bfdc403348?w=600&fit=crop",
                Volume = "3.6g", SkinType = "Все типы", InStock = true, IsVerified = true, IsBestseller = true,
                PriceRange = PriceRangeEnum.Medium, BrandId = 1, CategoryId = 2, DateCreated = now
            },
            new Product
            {
                Id = 2, Title = "Тушь для ресниц Sky High", Description = "Тушь для экстремального объёма и удлинения. Придаёт ресницам невероятную длину без склеивания.",
                Price = 3200, ImageUrl = "https://images.unsplash.com/photo-1612817288484-6f916006741a?w=600&fit=crop",
                Volume = "7.2ml", SkinType = "Все типы", InStock = true, IsVerified = true, IsBestseller = true,
                PriceRange = PriceRangeEnum.Medium, BrandId = 2, CategoryId = 2, DateCreated = now
            },
            new Product
            {
                Id = 3, Title = "Тональный крем Studio Fix", Description = "Профессиональный тональный крем с матовым финишем. Обеспечивает полное покрытие и стойкость до 24 часов.",
                Price = 18000, ImageUrl = "https://images.unsplash.com/photo-1512496015851-a90fb38ba796?w=600&fit=crop",
                Volume = "30ml", SkinType = "Жирная", InStock = true, IsVerified = true, IsBestseller = false,
                PriceRange = PriceRangeEnum.Premium, BrandId = 3, CategoryId = 2, DateCreated = now
            },
            new Product
            {
                Id = 4, Title = "Сыворотка с витамином C", Description = "Осветляющая сыворотка с 23% аскорбиновой кислотой. Выравнивает тон и устраняет пигментацию.",
                Price = 6500, ImageUrl = "https://images.unsplash.com/photo-1620916566398-39f1143ab7be?w=600&fit=crop",
                Volume = "30ml", SkinType = "Все типы", InStock = true, IsVerified = true, IsBestseller = true,
                PriceRange = PriceRangeEnum.Medium, BrandId = 4, CategoryId = 1, DateCreated = now
            },
            new Product
            {
                Id = 5, Title = "Увлажняющий крем CeraVe", Description = "Интенсивно увлажняющий крем с керамидами и гиалуроновой кислотой. Восстанавливает кожный барьер.",
                Price = 5800, ImageUrl = "https://images.unsplash.com/photo-1631730486572-226d1f595b68?w=600&fit=crop",
                Volume = "50ml", SkinType = "Сухая", InStock = true, IsVerified = true, IsBestseller = true,
                PriceRange = PriceRangeEnum.Medium, BrandId = 5, CategoryId = 1, DateCreated = now
            },
            new Product
            {
                Id = 6, Title = "Мицеллярная вода Garnier", Description = "Нежно очищает кожу, снимает макияж и успокаивает. Подходит даже для чувствительной кожи.",
                Price = 2100, ImageUrl = "https://images.unsplash.com/photo-1556228578-8c89e6adf883?w=600&fit=crop",
                Volume = "400ml", SkinType = "Чувствительная", InStock = true, IsVerified = true, IsBestseller = false,
                PriceRange = PriceRangeEnum.Budget, BrandId = 9, CategoryId = 1, DateCreated = now
            },
            new Product
            {
                Id = 7, Title = "Солнцезащитный крем SPF50", Description = "Лёгкий солнцезащитный крем с высоким SPF50. Защищает от UVA и UVB лучей, не оставляет белых следов.",
                Price = 3900, ImageUrl = "https://images.unsplash.com/photo-1526758097130-bab247274f58?w=600&fit=crop",
                Volume = "50ml", SkinType = "Все типы", InStock = true, IsVerified = true, IsBestseller = false,
                PriceRange = PriceRangeEnum.Medium, BrandId = 8, CategoryId = 7, DateCreated = now
            },
            new Product
            {
                Id = 8, Title = "Шампунь Elseve", Description = "Питательный шампунь для сухих и повреждённых волос. Восстанавливает структуру и придаёт блеск.",
                Price = 2800, ImageUrl = "https://images.unsplash.com/photo-1522338242992-e1a54906a8da?w=600&fit=crop",
                Volume = "400ml", SkinType = "Все типы", InStock = true, IsVerified = true, IsBestseller = false,
                PriceRange = PriceRangeEnum.Budget, BrandId = 1, CategoryId = 3, DateCreated = now
            },
            new Product
            {
                Id = 9, Title = "Маска для волос Garnier", Description = "Интенсивная маска для восстановления волос. Содержит масло авокадо и пчелиный воск.",
                Price = 1900, ImageUrl = "https://images.unsplash.com/photo-1522338242992-e1a54906a8da?w=600&fit=crop",
                Volume = "300ml", SkinType = "Все типы", InStock = true, IsVerified = false, IsBestseller = false,
                PriceRange = PriceRangeEnum.Budget, BrandId = 9, CategoryId = 3, DateCreated = now
            },
            new Product
            {
                Id = 10, Title = "Парфюм Miss Dior", Description = "Изысканный аромат свежих пионов и мандарина. Символ романтики и женственности.",
                Price = 65000, ImageUrl = "https://images.unsplash.com/photo-1541643600914-78b084683702?w=600&fit=crop",
                Volume = "50ml", SkinType = "Все типы", InStock = true, IsVerified = true, IsBestseller = true,
                PriceRange = PriceRangeEnum.Luxury, BrandId = 6, CategoryId = 4, DateCreated = now
            },
            new Product
            {
                Id = 11, Title = "Парфюм Chanel No.5", Description = "Легендарный аромат — альдегидный флoral с нотами жасмина и розы. Икона парфюмерии.",
                Price = 89000, ImageUrl = "https://images.unsplash.com/photo-1557170334-a9086a2b4282?w=600&fit=crop",
                Volume = "100ml", SkinType = "Все типы", InStock = true, IsVerified = true, IsBestseller = true,
                PriceRange = PriceRangeEnum.Luxury, BrandId = 7, CategoryId = 4, DateCreated = now
            },
            new Product
            {
                Id = 12, Title = "Лак для ногтей NYX", Description = "Стойкий лак для ногтей с гелевым эффектом. Палитра более 100 оттенков, держится до 10 дней.",
                Price = 1800, ImageUrl = "https://images.unsplash.com/photo-1571781926291-c477ebfd024b?w=600&fit=crop",
                Volume = "13.3ml", SkinType = "Все типы", InStock = true, IsVerified = false, IsBestseller = false,
                PriceRange = PriceRangeEnum.Budget, BrandId = 10, CategoryId = 6, DateCreated = now
            },
            new Product
            {
                Id = 13, Title = "Гель для душа Nivea", Description = "Нежный гель для душа с питательной формулой. Оставляет кожу мягкой и увлажнённой весь день.",
                Price = 1500, ImageUrl = "https://images.unsplash.com/photo-1571019613454-1cb2f99b2d8b?w=600&fit=crop",
                Volume = "500ml", SkinType = "Все типы", InStock = true, IsVerified = true, IsBestseller = false,
                PriceRange = PriceRangeEnum.Budget, BrandId = 8, CategoryId = 5, DateCreated = now
            },
            new Product
            {
                Id = 14, Title = "Скраб для тела L'Oréal", Description = "Отшелушивающий скраб с частицами сахара и маслом ши. Разглаживает и питает кожу.",
                Price = 4200, ImageUrl = "https://images.unsplash.com/photo-1631730486572-226d1f595b68?w=600&fit=crop",
                Volume = "200ml", SkinType = "Все типы", InStock = true, IsVerified = false, IsBestseller = false,
                PriceRange = PriceRangeEnum.Medium, BrandId = 1, CategoryId = 5, DateCreated = now
            },
            new Product
            {
                Id = 15, Title = "Набор кистей MAC", Description = "Профессиональный набор из 12 кистей для нанесения макияжа. Волосяной ворс, удобные ручки.",
                Price = 22000, ImageUrl = "https://images.unsplash.com/photo-1522338242992-e1a54906a8da?w=600&fit=crop",
                Volume = "12 шт", SkinType = "Все типы", InStock = true, IsVerified = true, IsBestseller = false,
                PriceRange = PriceRangeEnum.Premium, BrandId = 3, CategoryId = 8, DateCreated = now
            },
            new Product
            {
                Id = 16, Title = "Консилер NYX", Description = "Консилер с полным покрытием для скрытия несовершенств. Водостойкая формула на весь день.",
                Price = 3100, ImageUrl = "https://images.unsplash.com/photo-1596462502278-27bfdc403348?w=600&fit=crop",
                Volume = "3ml", SkinType = "Все типы", InStock = true, IsVerified = false, IsBestseller = false,
                PriceRange = PriceRangeEnum.Medium, BrandId = 10, CategoryId = 2, DateCreated = now
            },
            new Product
            {
                Id = 17, Title = "Хайлайтер Maybelline", Description = "Сияющий хайлайтер для скульптурирования лица. Создаёт эффект здорового сияния кожи.",
                Price = 2900, ImageUrl = "https://images.unsplash.com/photo-1571781926291-c477ebfd024b?w=600&fit=crop",
                Volume = "5g", SkinType = "Все типы", InStock = true, IsVerified = true, IsBestseller = false,
                PriceRange = PriceRangeEnum.Budget, BrandId = 2, CategoryId = 2, DateCreated = now
            },
            new Product
            {
                Id = 18, Title = "Тоник для лица The Ordinary", Description = "Балансирующий тоник с гликолевой кислотой 7%. Отшелушивает, выравнивает тон, сужает поры.",
                Price = 4800, ImageUrl = "https://images.unsplash.com/photo-1620916566398-39f1143ab7be?w=600&fit=crop",
                Volume = "240ml", SkinType = "Комбинированная", InStock = true, IsVerified = true, IsBestseller = false,
                PriceRange = PriceRangeEnum.Medium, BrandId = 4, CategoryId = 1, DateCreated = now
            },
            new Product
            {
                Id = 19, Title = "Крем для рук Nivea", Description = "Интенсивно питающий крем для рук с глицерином. Быстро впитывается, не оставляет жирного блеска.",
                Price = 900, ImageUrl = "https://images.unsplash.com/photo-1631730486572-226d1f595b68?w=600&fit=crop",
                Volume = "100ml", SkinType = "Все типы", InStock = true, IsVerified = true, IsBestseller = false,
                PriceRange = PriceRangeEnum.Budget, BrandId = 8, CategoryId = 5, DateCreated = now
            },
            new Product
            {
                Id = 20, Title = "Блеск для губ NYX", Description = "Глянцевый блеск для губ с эффектом объёма. Увлажняет и придаёт соблазнительный блеск.",
                Price = 2400, ImageUrl = "https://images.unsplash.com/photo-1596462502278-27bfdc403348?w=600&fit=crop",
                Volume = "8ml", SkinType = "Все типы", InStock = true, IsVerified = false, IsBestseller = false,
                PriceRange = PriceRangeEnum.Budget, BrandId = 10, CategoryId = 2, DateCreated = now
            }
        );
    }
}
