namespace CosmeticsShop.Services;

public class LangService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly Dictionary<string, Dictionary<string, string>> _dict;

    public LangService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
        _dict = new Dictionary<string, Dictionary<string, string>>
        {
            ["ru"] = new()
            {
                // Nav
                ["nav.products"]    = "Каталог",
                ["nav.brands"]      = "Бренды",
                ["nav.cart"]        = "Корзина",
                ["nav.orders"]      = "Заказы",
                ["nav.profile"]     = "Профиль",
                ["nav.login"]       = "Войти",
                ["nav.register"]    = "Регистрация",
                ["nav.logout"]      = "Выйти",
                ["nav.admin"]       = "Админ панель",

                // Home
                ["home.title"]      = "Beauty Shop",
                ["home.subtitle"]   = "Откройте мир красоты — сотни брендов, тысячи товаров",
                ["home.find"]       = "Найдите свой идеальный продукт",
                ["home.bestsellers"]= "Хиты продаж",
                ["home.by_category"]= "Категории",
                ["home.all_brands"] = "Все бренды",
                ["home.stat_products"] = "Товаров",
                ["home.stat_brands"]   = "Брендов",
                ["home.stat_rating"]   = "Средний рейтинг",
                ["home.cta_title"]  = "Присоединяйтесь к Beauty Shop",
                ["home.cta_sub"]    = "Регистрируйтесь и получайте персональные рекомендации",
                ["home.cta_btn"]    = "Зарегистрироваться",

                // Products
                ["prod.title"]           = "Каталог товаров",
                ["prod.add_to_cart"]     = "В корзину",
                ["prod.buy_now"]         = "Купить сейчас",
                ["prod.out_of_stock"]    = "Нет в наличии",
                ["prod.bestseller"]      = "Хит",
                ["prod.verified"]        = "Верифицирован",
                ["prod.reviews_sfx"]     = "отзывов",
                ["prod.in_stock"]        = "В наличии",
                ["prod.search"]          = "Поиск товаров...",
                ["prod.all_brands"]      = "Все бренды",
                ["prod.all_cats"]        = "Все категории",
                ["prod.any_price"]       = "Любая цена",
                ["prod.any_skin"]        = "Любой тип кожи",
                ["prod.sort_new"]        = "Новые",
                ["prod.sort_rating"]     = "По рейтингу",
                ["prod.sort_price_asc"]  = "Цена ↑",
                ["prod.sort_price_desc"] = "Цена ↓",
                ["prod.in_stock_only"]   = "Только в наличии",
                ["prod.filter"]          = "Фильтры",
                ["prod.sort"]            = "Сортировка",
                ["prod.apply"]           = "Применить",
                ["prod.reset"]           = "Сбросить",
                ["prod.found"]           = "Найдено",
                ["prod.items"]           = "товаров",
                ["prod.details"]         = "Подробнее",
                ["prod.composition"]     = "Состав / Описание",
                ["prod.volume"]          = "Объём",
                ["prod.skin_type"]       = "Тип кожи",
                ["prod.brand"]           = "Бренд",
                ["prod.category"]        = "Категория",
                ["prod.rating_overall"]  = "Общий рейтинг",
                ["prod.write_review"]    = "Написать отзыв",
                ["prod.reviews_title"]   = "Отзывы покупателей",
                ["prod.qty"]             = "Количество",

                // Cart
                ["cart.title"]    = "Корзина",
                ["cart.empty"]    = "Ваша корзина пуста",
                ["cart.total"]    = "Итого",
                ["cart.checkout"] = "Оформить заказ",
                ["cart.remove"]   = "Удалить",
                ["cart.quantity"] = "Кол-во",
                ["cart.continue"] = "Продолжить покупки",

                // Order
                ["order.title"]             = "Оформление заказа",
                ["order.address"]           = "Адрес доставки",
                ["order.phone"]             = "Телефон",
                ["order.comment"]           = "Комментарий к заказу",
                ["order.confirm"]           = "Подтвердить заказ",
                ["order.status_pending"]    = "Ожидает подтверждения",
                ["order.status_processing"] = "Обрабатывается",
                ["order.status_shipped"]    = "Отправлен",
                ["order.status_delivered"]  = "Доставлен",
                ["order.status_cancelled"]  = "Отменён",
                ["order.my_orders"]         = "Мои заказы",
                ["order.number"]            = "Заказ",
                ["order.date"]              = "Дата",
                ["order.total"]             = "Сумма",
                ["order.details"]           = "Подробнее",
                ["order.no_orders"]         = "У вас пока нет заказов",
                ["order.summary"]           = "Ваш заказ",
                ["order.success"]           = "Заказ оформлен!",
                ["order.success_msg"]       = "Спасибо за покупку. Номер вашего заказа:",

                // Reviews
                ["rev.page_title"]        = "Написать отзыв",
                ["rev.title_label"]       = "Заголовок",
                ["rev.text_label"]        = "Текст отзыва",
                ["rev.rating"]            = "Общая оценка",
                ["rev.quality"]           = "Качество",
                ["rev.packaging"]         = "Упаковка",
                ["rev.value_price"]       = "Цена/Качество",
                ["rev.submit"]            = "Опубликовать",
                ["rev.verified_purchase"] = "Подтверждённая покупка",
                ["rev.useful"]            = "Полезно",
                ["rev.not_useful"]        = "Не полезно",
                ["rev.delete"]            = "Удалить",
                ["rev.no_reviews"]        = "Отзывов пока нет",
                ["rev.be_first"]          = "Будьте первым, кто оставит отзыв!",
                ["rev.login_required"]    = "Войдите, чтобы оставить отзыв",

                // Price ranges
                ["price.budget"]  = "до 3000 ₸",
                ["price.medium"]  = "3000–10000 ₸",
                ["price.premium"] = "10000–30000 ₸",
                ["price.luxury"]  = "от 30000 ₸",

                // Profile
                ["profile.title"]       = "Мой профиль",
                ["profile.my_reviews"]  = "Мои отзывы",
                ["profile.email"]       = "Email",
                ["profile.member_since"]= "Участник с",

                // Admin
                ["admin.dashboard"]     = "Панель управления",
                ["admin.products"]      = "Товары",
                ["admin.orders"]        = "Заказы",
                ["admin.reviews"]       = "Отзывы",
                ["admin.brands"]        = "Бренды",
                ["admin.categories"]    = "Категории",
                ["admin.add"]           = "Добавить",
                ["admin.edit"]          = "Редактировать",
                ["admin.delete"]        = "Удалить",
                ["admin.save"]          = "Сохранить",
                ["admin.cancel"]        = "Отмена",
                ["admin.total_products"]= "Всего товаров",
                ["admin.total_orders"]  = "Всего заказов",
                ["admin.today_orders"]  = "Заказов сегодня",
                ["admin.month_revenue"] = "Выручка за месяц",
                ["admin.recent_orders"] = "Последние заказы",
                ["admin.new_reviews"]   = "Новые отзывы",
                ["admin.change_status"] = "Изменить статус",
                ["admin.login"]         = "Вход в админ панель",

                // Common
                ["common.yes"]    = "Да",
                ["common.no"]     = "Нет",
                ["common.save"]   = "Сохранить",
                ["common.cancel"] = "Отмена",
                ["common.back"]   = "Назад",
                ["common.loading"]= "Загрузка...",
            },
            ["kz"] = new()
            {
                // Nav
                ["nav.products"]    = "Каталог",
                ["nav.brands"]      = "Брендтер",
                ["nav.cart"]        = "Себет",
                ["nav.orders"]      = "Тапсырыстар",
                ["nav.profile"]     = "Профиль",
                ["nav.login"]       = "Кіру",
                ["nav.register"]    = "Тіркелу",
                ["nav.logout"]      = "Шығу",
                ["nav.admin"]       = "Әкімші панелі",

                // Home
                ["home.title"]      = "Beauty Shop",
                ["home.subtitle"]   = "Сұлулық әлемін ашыңыз — жүздеген брендтер, мыңдаған тауарлар",
                ["home.find"]       = "Өз тамаша өніміңізді табыңыз",
                ["home.bestsellers"]= "Хит тауарлар",
                ["home.by_category"]= "Санаттар",
                ["home.all_brands"] = "Барлық брендтер",
                ["home.stat_products"] = "Тауар",
                ["home.stat_brands"]   = "Бренд",
                ["home.stat_rating"]   = "Орташа рейтинг",
                ["home.cta_title"]  = "Beauty Shop-қа қосылыңыз",
                ["home.cta_sub"]    = "Тіркеліп, жеке ұсыныстар алыңыз",
                ["home.cta_btn"]    = "Тіркелу",

                // Products
                ["prod.title"]           = "Тауарлар каталогы",
                ["prod.add_to_cart"]     = "Себетке",
                ["prod.buy_now"]         = "Қазір сатып алу",
                ["prod.out_of_stock"]    = "Қоймада жоқ",
                ["prod.bestseller"]      = "Хит",
                ["prod.verified"]        = "Расталған",
                ["prod.reviews_sfx"]     = "пікір",
                ["prod.in_stock"]        = "Қоймада бар",
                ["prod.search"]          = "Тауар іздеу...",
                ["prod.all_brands"]      = "Барлық брендтер",
                ["prod.all_cats"]        = "Барлық санаттар",
                ["prod.any_price"]       = "Кез келген баға",
                ["prod.any_skin"]        = "Кез келген тері түрі",
                ["prod.sort_new"]        = "Жаңалар",
                ["prod.sort_rating"]     = "Рейтинг бойынша",
                ["prod.sort_price_asc"]  = "Баға ↑",
                ["prod.sort_price_desc"] = "Баға ↓",
                ["prod.in_stock_only"]   = "Тек қоймада бар",
                ["prod.filter"]          = "Сүзгілер",
                ["prod.sort"]            = "Сұрыптау",
                ["prod.apply"]           = "Қолдану",
                ["prod.reset"]           = "Тазалау",
                ["prod.found"]           = "Табылды",
                ["prod.items"]           = "тауар",
                ["prod.details"]         = "Толығырақ",
                ["prod.composition"]     = "Құрамы / Сипаттамасы",
                ["prod.volume"]          = "Көлем",
                ["prod.skin_type"]       = "Тері түрі",
                ["prod.brand"]           = "Бренд",
                ["prod.category"]        = "Санат",
                ["prod.rating_overall"]  = "Жалпы рейтинг",
                ["prod.write_review"]    = "Пікір жазу",
                ["prod.reviews_title"]   = "Сатып алушылар пікірлері",
                ["prod.qty"]             = "Саны",

                // Cart
                ["cart.title"]    = "Себет",
                ["cart.empty"]    = "Себетіңіз бос",
                ["cart.total"]    = "Жиыны",
                ["cart.checkout"] = "Тапсырыс беру",
                ["cart.remove"]   = "Жою",
                ["cart.quantity"] = "Саны",
                ["cart.continue"] = "Сатып алуды жалғастыру",

                // Order
                ["order.title"]             = "Тапсырыс беру",
                ["order.address"]           = "Жеткізу мекенжайы",
                ["order.phone"]             = "Телефон",
                ["order.comment"]           = "Тапсырысқа түсініктеме",
                ["order.confirm"]           = "Тапсырысты растау",
                ["order.status_pending"]    = "Растауды күтуде",
                ["order.status_processing"] = "Өңделуде",
                ["order.status_shipped"]    = "Жіберілді",
                ["order.status_delivered"]  = "Жеткізілді",
                ["order.status_cancelled"]  = "Бас тартылды",
                ["order.my_orders"]         = "Менің тапсырыстарым",
                ["order.number"]            = "Тапсырыс",
                ["order.date"]              = "Күні",
                ["order.total"]             = "Сома",
                ["order.details"]           = "Толығырақ",
                ["order.no_orders"]         = "Тапсырыстарыңыз жоқ",
                ["order.summary"]           = "Сіздің тапсырысыңыз",
                ["order.success"]           = "Тапсырыс берілді!",
                ["order.success_msg"]       = "Сатып алғаныңызға рахмет. Тапсырыс нөміріңіз:",

                // Reviews
                ["rev.page_title"]        = "Пікір жазу",
                ["rev.title_label"]       = "Тақырып",
                ["rev.text_label"]        = "Пікір мәтіні",
                ["rev.rating"]            = "Жалпы баға",
                ["rev.quality"]           = "Сапасы",
                ["rev.packaging"]         = "Қаптамасы",
                ["rev.value_price"]       = "Баға/Сапа",
                ["rev.submit"]            = "Жариялау",
                ["rev.verified_purchase"] = "Расталған сатып алу",
                ["rev.useful"]            = "Пайдалы",
                ["rev.not_useful"]        = "Пайдасыз",
                ["rev.delete"]            = "Жою",
                ["rev.no_reviews"]        = "Пікірлер жоқ",
                ["rev.be_first"]          = "Бірінші пікір қалдырыңыз!",
                ["rev.login_required"]    = "Пікір қалдыру үшін кіріңіз",

                // Price ranges
                ["price.budget"]  = "3000 ₸ дейін",
                ["price.medium"]  = "3000–10000 ₸",
                ["price.premium"] = "10000–30000 ₸",
                ["price.luxury"]  = "30000 ₸ жоғары",

                // Profile
                ["profile.title"]       = "Менің профилім",
                ["profile.my_reviews"]  = "Менің пікірлерім",
                ["profile.email"]       = "Email",
                ["profile.member_since"]= "Мүше болған күні",

                // Admin
                ["admin.dashboard"]     = "Басқару панелі",
                ["admin.products"]      = "Тауарлар",
                ["admin.orders"]        = "Тапсырыстар",
                ["admin.reviews"]       = "Пікірлер",
                ["admin.brands"]        = "Брендтер",
                ["admin.categories"]    = "Санаттар",
                ["admin.add"]           = "Қосу",
                ["admin.edit"]          = "Өзгерту",
                ["admin.delete"]        = "Жою",
                ["admin.save"]          = "Сақтау",
                ["admin.cancel"]        = "Болдырмау",
                ["admin.total_products"]= "Барлық тауарлар",
                ["admin.total_orders"]  = "Барлық тапсырыстар",
                ["admin.today_orders"]  = "Бүгінгі тапсырыстар",
                ["admin.month_revenue"] = "Айлық түсім",
                ["admin.recent_orders"] = "Соңғы тапсырыстар",
                ["admin.new_reviews"]   = "Жаңа пікірлер",
                ["admin.change_status"] = "Күйді өзгерту",
                ["admin.login"]         = "Әкімші панеліне кіру",

                // Common
                ["common.yes"]    = "Иә",
                ["common.no"]     = "Жоқ",
                ["common.save"]   = "Сақтау",
                ["common.cancel"] = "Болдырмау",
                ["common.back"]   = "Артқа",
                ["common.loading"]= "Жүктелуде...",
            }
        };
    }

    public string Lang
    {
        get
        {
            var ctx = _httpContextAccessor.HttpContext;
            if (ctx?.Request.Cookies.TryGetValue("lang", out var lang) == true
                && _dict.ContainsKey(lang))
                return lang;
            return "ru";
        }
    }

    public string T(string key)
    {
        var lang = Lang;
        if (_dict.TryGetValue(lang, out var d) && d.TryGetValue(key, out var val))
            return val;
        if (_dict.TryGetValue("ru", out var ru) && ru.TryGetValue(key, out var ruVal))
            return ruVal;
        return key;
    }
}
