using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Security;
using Domain.Entity.Admin;
using Domain.Entity.Announcements;
using Domain.Entity.Catalogs;
using Domain.Entity.Other;
using Domain.Entity.Realty;
using Domain.Entity.Shared;
using Reklama.Filters;
using Reklama.Models;
using WebMatrix.WebData;
using Domain.Entity.Articles;

namespace Reklama.Migrations
{

    internal sealed class Configuration : DbMigrationsConfiguration<Reklama.Models.ReklamaContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        [InitializeSimpleMembership]
        protected override void Seed(Reklama.Models.ReklamaContext context)
        {
            //SeedMembership(context);
            //SeedMenu(context);
            //SeedCity(context);
            //SeedCurrency(context);

            //SeedCategory(context);
//            SeedSectionsSubsections(context);

            //SeedRealtyCategory(context);
            //SeedRealtySection(context);

            //SeedArticleSection(context);
            //SeedArticleSubsection(context);
            //SeedPage(context);

            //SeedConfig(context);
            //SeedPremium(context);
            //SeedAnnouncementConfiguration(context);
            //SeedArticleConfig(context);

            //SeedRoles();
            //SeedCatalogCategory(context);
            //SeedUnit(context);
            //SeedCatalogParamSection(context);
            //SeedConfig(context);
            //SeedProductParam(context);


            // Seed on 1st production updates
            //SeedCatalogConfig(context);


            // Don't seed this seeds on production server
            //SeedPopularSectionInCatalog(context);
            //SeedNewSectionInCatalog(context);
            //SeedPopularProducts(context);
            //SeedPopularAnnouncement(context);
            //SeedPopularProducts(context);
            //SeedMainPageArticleConfig(context);
        }


        /// Создание администратора
        private void SeedMembership(Reklama.Models.ReklamaContext context)
        {
            if (!WebSecurity.Initialized)
            {
                WebSecurity.InitializeDatabaseConnection("UserDbConnection", "UserProfile", "UserId", "Email", autoCreateTables: true);
            }

            var roleProvider = (SimpleRoleProvider)Roles.Provider;
            var membershipProvider = (SimpleMembershipProvider)Membership.Provider;

            if (membershipProvider.GetUser("admin@reklama.tm", false) == null)
            {
                membershipProvider.CreateUserAndAccount("admin@reklama.tm", "qwerty2q1");
                var user = context.UserProfiles.First();
                user.Name = "Name";
                user.Surname = "Surname";
                user.Icq = "000000";
                user.Site = "reklama.tm";
                user.Skype = "reklama_skype";
                context.UserProfiles.AddOrUpdate(user);

            }

            if (!roleProvider.RoleExists(ProjectConfiguration.Get.AdministratorRole))
            {
                roleProvider.CreateRole(ProjectConfiguration.Get.AdministratorRole);
            }

            if (!roleProvider.IsUserInRole("admin@reklama.tm", ProjectConfiguration.Get.AdministratorRole))
            {
                roleProvider.AddUsersToRoles(new[] { "admin@reklama.tm" }, new[] { ProjectConfiguration.Get.AdministratorRole });
            }
        }


        private void SeedMenu(Reklama.Models.ReklamaContext context)
        {
            context.Menus.AddOrUpdate(i => i.Id,
                new[]
                    {
                        new Menu()
                            {
                                Id = 1, Name = "UnderLogoMenu", Image = "UnderLogoMenu.png",
                                Description = "Находится сразу под логотипом"
                            },
                        new Menu()
                            {
                                Id = 2, Name = "AllsDropDownMenu", Image = "AllsDropDownMenu.png",
                                Description = "Находится за строкой главного меню в выпадающем списке &quote;Все&quote;"
                            },
                        new Menu()
                            {
                                Id = 3, Name = "FooterMenu", Image = "FooterMenu.png",
                                Description = "Находится в самом низу (футере) сайта"
                            },
                        new Menu()
                        {
                            Id = 4, Name = "FooterBottomMenu", Image = "FooterBottomMenu.png",
                            Description = "Находится чуть ниже меню футера"
                        }
                    });
        }


        private void SeedCategory(ReklamaContext context)
        {
            context.Categories.AddOrUpdate(
                new Category() { Id = 1, Name = "Продажа" },
                new Category() { Id = 2, Name = "Покупка" },
                new Category() { Id = 3, Name = "Обмен" }
                );
        }


        private void SeedSectionsSubsections(ReklamaContext context)
        {
            context.Sections.AddOrUpdate(
                                        new Section() { Id = 1, Name = "Телефоны" },
                                        new Section() { Id = 2, Name = "Компьютеры" },
                                        new Section() { Id = 3, Name = "Авто" }
                                        );

            context.Subsections.AddOrUpdate(
                new Subsection() { Id = 1, Name = "Nokia", SectionId = 1 },
                new Subsection() { Id = 2, Name = "Motorola", SectionId = 1 },
                new Subsection() { Id = 3, Name = "Apple", SectionId = 2 },
                new Subsection() { Id = 4, Name = "МегоБульдозер", SectionId = 3 }
                );
        }


        private void SeedConfig(ReklamaContext context)
        {
            //Доделать
            //Обязательные/Необазятельные поля для заполнения при регистрации магазина(п.16 в конце)
            //context.Configs.AddOrUpdate(
            //    new Config() { Id = 1, Name = "TextInfomationBlock", Description = "Текстовый информационный блок" , IsEnable = true , IsEnableValue = true},
            //    new Config() { Id = 2, Name = "CatalogTextPromoBlock", Description = "Текстовый промо блок", IsEnable = null, IsEnableValue = true },
            //    new Config() { Id = 5, Name = "NumberActualArticles", Description = "Количество актуальных статей", IsEnable = null, IsEnableValue = true },
            //    new Config() { Id = 6, Name = "NumberPopularArticles", Description = "Количество популярных статей", IsEnable = null, IsEnableValue = true },
            //    new Config() { Id = 8, Name = "ExpiredAtAnnouncement", Description = "Время действия объявления", Value = "30", IsEnable = null, IsEnableValue = true },
            //new Config() { Id = 9, Name = "ExpiredAtRealty", Description = "Время действия объявления недвижимости", Value = "30", IsEnable = null, IsEnableValue = true },
            //    new Config() { Id = 10, Name = "UpTimeAnnouncement", Description = "Количество часов до поднятия объявления", Value = "20", IsEnable = null, IsEnableValue = true },
            //    new Config() { Id = 11, Name = "UpTimeRealty", Description = "Количество часов до поднятия объявления недвижимости", Value = "20", IsEnable = null, IsEnableValue = true },
            //    new Config() { Id = 12, Name = "LinkFacebook", Description = "Ссылка на группу в Facebook", Value = "http://facebook.com", IsEnable = null, IsEnableValue = true },
            //    new Config() { Id = 13, Name = "LinkVk", Description = "Ссылка на группу в ВКонтакте", Value = "http://vk.com", IsEnable = null, IsEnableValue = true },
            //    new Config() { Id = 14, Name = "LinkTwitter", Description = "Ссылка на аккаунт Twitter", Value = "http://twitter.com", IsEnable = null, IsEnableValue = true },
            //    new Config() { Id = 15, Name = "LinkGooglePlus", Description = "Ссылка на группу в Google plus", Value = "http://plus.google.com", IsEnable = null, IsEnableValue = true },
            //    new Config() { Id = 16, Name = "LinkOdnoklassniki", Description = "Ссылка на группу в 'Одноклассники'", Value = "http://odnoklassniki.ru", IsEnable = null, IsEnableValue = true },
            //    new Config() { Id = 17, Name = "LinkMail", Description = "Ссылка на группу в 'Мой мир'", Value = "http://mail.ru", IsEnable = null, IsEnableValue = true }
            //    );

            //context.Configs.AddOrUpdate(
            //        new Config() { Id = 18, Name = "UserAgreement", Description = "Страница пользовательского соглашения", IsEnable = true, IsEnableValue = true, Value = 1.ToString() },
            //        new Config() { Id = 19, Name = "HowToUpAnnouncement", Description = "Как поднять объявление?", IsEnable = true, IsEnableValue = true, Value = 1.ToString() }

            //new Config() { Id = 20, Name = "IsEnabledPaymentTerminal", Description = "Включить агрегатор платежей?", IsEnable = true },
            //new Config() { Id = 21, Name = "Yandex Direct", Description = "Включить контекстную рекламу Яндекса?", IsEnable = true, IsEnableValue = true },
            //new Config() { Id = 22, Name = "CountOfPremium1Items", Description = "Количество выводимых Премиум-1 объявлений", IsEnableValue = true, Value = "3" },
            //new Config() { Id = 23, Name = "CountOfPremium2Items", Description = "Количество выводимых Премиум-2 объявлений", IsEnableValue = true, Value = "3" },
            //new Config() { Id = 24, Name = "CountOfPremium3Items", Description = "Количество выводимых Премиум-3 объявлений", IsEnableValue = true, Value = "3" },
            //new Config() { Id = 25, Name = "ROBOPass1", Description = "Пароль #1 для робокассы", IsEnableValue = true, Value = "adsaderq534asasd" },
            //new Config() { Id = 26, Name = "ROBOPass2", Description = "Пароль #2 для робокассы", IsEnableValue = true, Value = "fhsdkf7#T*Rhjgfhj" },
            //new Config() { Id = 27, Name = "ROBOLogin", Description = "Логин для робокассы", IsEnableValue = true, Value = "maximahmedov" }
            //);
            context.Configs.AddOrUpdate(new Config() { Id = 28, Name = "ChangeRegistrationDataHelp", Description = "Сообщение выводимое при изменении регистрационных данных магазина", IsEnableValue = true, Value = "" });
        }


        private void SeedCity(ReklamaContext context)
        {
            //Cities
            context.Cities.AddOrUpdate(
                new City() { Id = 1, Name = "Ашхабад" },
                new City() { Id = 2, Name = "Абадан" },
                new City() { Id = 3, Name = "Аннау" },
                new City() { Id = 4, Name = "Атамурат" },
                new City() { Id = 5, Name = "Байрамали" },
                new City() { Id = 6, Name = "Балканабад" },
                new City() { Id = 7, Name = "Берекет" },
                new City() { Id = 8, Name = "Бехерден" },
                new City() { Id = 9, Name = "Газаджак" },
                new City() { Id = 10, Name = "Гёкделе" },
                new City() { Id = 11, Name = "Дашогуз" },
                new City() { Id = 12, Name = "Ёлётен" },
                new City() { Id = 13, Name = "Карабогаз" },
                new City() { Id = 14, Name = "Кёнеургенч" },
                new City() { Id = 15, Name = "Кумдаг" },
                new City() { Id = 16, Name = "Магданлы" },
                new City() { Id = 17, Name = "Мары" },
                new City() { Id = 18, Name = "Сейди" },
                new City() { Id = 19, Name = "Сердар" },
                new City() { Id = 20, Name = "Серхетабад" },
                new City() { Id = 21, Name = "Теджен" },
                new City() { Id = 22, Name = "Туркменабад" },
                new City() { Id = 23, Name = "Туркменбашы" },
                new City() { Id = 24, Name = "Хазар" },
                new City() { Id = 25, Name = "Шатлык" },
                new City() { Id = 26, Name = "--Другой" }
                );
        }





        private void SeedCurrency(ReklamaContext context)
        {
            //Currency
            context.Currencies.AddOrUpdate(
                new Currency() { Id = 1, Name = "USD", Rate = 1 },
                //new Currency() { Id = 2, Name = "EUR", Rate = 1.41f },
                new Currency() { Id = 3, Name = "TMT", Rate = 2.85f }
                );
        }


        private void SeedPage(ReklamaContext context)
        {
            context.Pages.AddOrUpdate(
                new Page() { Id = 1, Name = "Контакты", Description = "Наши контакты", CreatedAt = DateTime.Now, IsActive = true, PageTemplateId = 1, Slug = "our-contacts" }
            );
        }


        private void SeedRealtyCategory(ReklamaContext context)
        {
            //RealtyCategory
            context.RealtyCategories.AddOrUpdate(
                new RealtyCategory() { Id = 1, Name = "Дом" },
                new RealtyCategory() { Id = 2, Name = "Квартира" },
                new RealtyCategory() { Id = 3, Name = "Коттедж" }
                );
        }


        private void SeedRealtySection(ReklamaContext context)
        {
            //RealtySection
            context.RealtySections.AddOrUpdate(
                new RealtySection() { Id = 1, Name = "Продам" },
                new RealtySection() { Id = 2, Name = "Куплю" },
                new RealtySection() { Id = 3, Name = "Обменяю" },
                new RealtySection() { Id = 4, Name = "Сдам" },
                new RealtySection() { Id = 5, Name = "Сниму" },
                new RealtySection() { Id = 6, Name = "Услуга" }
                );
        }


        private void SeedArticleSection(ReklamaContext context)
        {
            context.ArticleSections.AddOrUpdate(
                new ArticleSection() { Id = 1, Name = "Телефоны" },
                new ArticleSection() { Id = 2, Name = "Компьютеры" },
                new ArticleSection() { Id = 3, Name = "Велосипеды" }
            );
        }

        private void SeedArticleSubsection(ReklamaContext context)
        {
            context.ArticleSubsections.AddOrUpdate(
                new ArticleSubsection() { Id = 1, Name = "Mototola", SectionId = 1 },
                new ArticleSubsection() { Id = 2, Name = "Apple", SectionId = 2 },
                new ArticleSubsection() { Id = 3, Name = "Аист", SectionId = 3 },
                new ArticleSubsection() { Id = 4, Name = "Author", SectionId = 3 }
            );
        }


        private void SeedPremium(ReklamaContext context)
        {
            context.Premiums.AddOrUpdate(
                new Premium() { Id = 1, Name = "Премим1 Доски", Description = "Премиум первого уровня для доски объявлений", Cost = 15, Lifetime = 72 },
                new Premium() { Id = 2, Name = "Премим2 Доски", Description = "Премиум второго уровня для доски объявлений", Cost = 10, Lifetime = 72 },
                new Premium() { Id = 3, Name = "Премим3 Доски", Description = "Премиум третьего уровня для доски объявлений", Cost = 5, Lifetime = 72 },

                new Premium() { Id = 4, Name = "Премим1 Недвижимости", Description = "Премиум первого уровня для недвижимости", Cost = 15, Lifetime = 72 },
                new Premium() { Id = 5, Name = "Премим2 Недвижимости", Description = "Премиум второго уровня для недвижимости", Cost = 10, Lifetime = 72 },
                new Premium() { Id = 6, Name = "Премим3 Недвижимости", Description = "Премиум третьего уровня для недвижимости", Cost = 5, Lifetime = 72 }
                );
        }


        private void SeedAnnouncementConfiguration(ReklamaContext context)
        {
            context.AnnouncementConfigs.AddOrUpdate(
                new AnnouncementConfig() { Id = 1, Slogan = "Объявления. Это стабильная версия нашего сайта", Premium1Id = 1, Premium2Id = 2, Premium3Id = 3 }
                );
        }


        private void SeedArticleConfig(ReklamaContext context)
        {
            context.ArticleConfigs.AddOrUpdate(
                new ArticleConfig() { Id = 1, Slogan = "Статьи. Это стабильная версия нашего сайта" }
                );
        }


        private void SeedRoles()
        {
            if (!WebSecurity.Initialized)
            {
                WebSecurity.InitializeDatabaseConnection("UserDbConnection", "UserProfile", "UserId", "Email", autoCreateTables: true);
            }

            var roleProvider = (SimpleRoleProvider)Roles.Provider;
            //roleProvider.CreateRole("Moderator");
            roleProvider.CreateRole("Shop");
        }


        private void SeedCatalogCategory(ReklamaContext context)
        {
            context.CatalogCategories.AddOrUpdate(
                new CatalogCategory() { Id = 1, Name = "Телефоны" },
                new CatalogCategory() { Id = 2, Name = "Компьютеры" }
            );

            context.CatalogSecondCategories.AddOrUpdate(
                new CatalogSecondCategory() { Id = 1, CategoryId = 1, Name = "Проводные телефоны" },
                new CatalogSecondCategory() { Id = 2, CategoryId = 1, Name = "Мобильные телефоны" },
                new CatalogSecondCategory() { Id = 3, CategoryId = 2, Name = "Стационарные компьютеры" },
                new CatalogSecondCategory() { Id = 4, CategoryId = 2, Name = "Ноутбуки" }
            );

            context.CatalogThirdCategories.AddOrUpdate(
                new CatalogThirdCategory() { Id = 1, SecondCategoryId = 1, Name = "Redtel" },
                new CatalogThirdCategory() { Id = 2, SecondCategoryId = 1, Name = "GreenTel" },
                new CatalogThirdCategory() { Id = 3, SecondCategoryId = 2, Name = "Motorola" },
                new CatalogThirdCategory() { Id = 4, SecondCategoryId = 2, Name = "Nokia" },
                new CatalogThirdCategory() { Id = 5, SecondCategoryId = 3, Name = "PhiniX" },
                new CatalogThirdCategory() { Id = 6, SecondCategoryId = 3, Name = "iMac" },
                new CatalogThirdCategory() { Id = 7, SecondCategoryId = 4, Name = "Apple" },
                new CatalogThirdCategory() { Id = 8, SecondCategoryId = 4, Name = "Lenovo" }
            );
        }


        private void SeedUnit(ReklamaContext context)
        {
            context.Units.AddOrUpdate(
                new Unit() { Id = 1, Name = "" },
                new Unit() { Id = 2, Name = "кг" },
                new Unit() { Id = 3, Name = "вольт" },
                new Unit() { Id = 4, Name = "мм" }
            );
        }


        private void SeedCatalogParamSection(ReklamaContext context)
        {
            context.CatalogParamSubsections.AddOrUpdate(
                new CatalogParamSubsection() { Id = 1, SecondCategoryId = 1, Name = "Длина провода", Slogan = "Длина провода проводного телефона" },
                new CatalogParamSubsection() { Id = 2, SecondCategoryId = 1, Name = "Дополнительные функции", Slogan = "Дополнительные функции проводного телефона" },
                new CatalogParamSubsection() { Id = 3, SecondCategoryId = 2, Name = "Характеристики дизайна", Slogan = "Характеристики дизайна мобильного телефона" },
                new CatalogParamSubsection() { Id = 4, SecondCategoryId = 2, Name = "Характеристики процессора", Slogan = "Характеристики процессора мобильного телефона" },
                new CatalogParamSubsection() { Id = 5, SecondCategoryId = 3, Name = "Характеристики процессора", Slogan = "Характеристики процессора ПК" },
                new CatalogParamSubsection() { Id = 6, SecondCategoryId = 4, Name = "Характеристики процессора", Slogan = "Характеристики процессора ноутбука" },
                new CatalogParamSubsection() { Id = 7, SecondCategoryId = 4, Name = "Покрытие", Slogan = "Покрытие ноутбука" }
            );
        }


        private void SeedProductParam(ReklamaContext context)
        {
            context.ProductParams.AddOrUpdate(
                new ProductParam() { Id = 1, Name = "Год выпуска" },
                new ProductParam() { Id = 2, Name = "Вес" },
                new ProductParam() { Id = 3, Name = "Габариты" },
                new ProductParam() { Id = 4, Name = "Объем памяти" },
                new ProductParam() { Id = 5, Name = "Мощность" }
            );
        }

        private void SeedCatalogConfig(ReklamaContext context)
        {
            context.CatalogConfigs.AddOrUpdate(
                new CatalogConfig()
                {
                    Id = 1,
                    Slogan = "Каталог. Это часть сайта находится на этапе тестирования",
                    PromoText = "<strong class=\"orangeStrong\">Увеличьте продажи</strong><br/>МТС-СМС",
                    RegShopPromoText = "<p>Несколько обзацев про-текста про выгодные условия торговли через ресурс.</p>\n<span>Наши преимущества</span>\n<ul>\n<li><img src=\"/Images/System/regN1.png\" />Краткий текст описания того или иного преимущества проекта перед конкурентами</li>\n<li><img src=\"/Images/System/regN2.png\" />Краткий текст описания того или иного преимущества проекта перед конкурентами</li>\n<li><img src=\"/Images/System/regN3.png\" />Краткий текст описания того или иного преимущества проекта перед конкурентами</li>\n<li><img src=\"/Images/System/regN4.png\" />Краткий текст описания того или иного преимущества проекта перед конкурентами</li>\n</ul>",
                    WarningText = "Приведенные предложения продавцов являются рекламной информацией и их приглашением делать оферы. При покупке всегда запоминайте полное наименование юридического лица или ИП продавца. Обязательно уточняйте комплект поставки, цвет товара и иную информацию в процессе заказа."
                }
                );
        }


        private void SeedPopularSectionInCatalog(ReklamaContext context)
        {
            context.PopularSectionsInCatalog.AddOrUpdate(
                new PopularSectionInCatalog() { Id = 1, SectionId = 1 },
                new PopularSectionInCatalog() { Id = 2, SectionId = 2 },
                new PopularSectionInCatalog() { Id = 3, SectionId = 3 },
                new PopularSectionInCatalog() { Id = 4, SectionId = 4 },
                new PopularSectionInCatalog() { Id = 5, SectionId = 5 }
                );
        }

        private void SeedNewSectionInCatalog(ReklamaContext context)
        {
            context.NewSectionsInCatalog.AddOrUpdate(
                new NewSectionInCatalog() { Id = 1, SectionId = 1 },
                new NewSectionInCatalog() { Id = 2, SectionId = 2 },
                new NewSectionInCatalog() { Id = 3, SectionId = 3 },
                new NewSectionInCatalog() { Id = 4, SectionId = 4 }
                );
        }

        private void SeedPopularProducts(ReklamaContext context)
        {
            context.PopularProducts.AddOrUpdate(
                new PopularProduct() { Id = 1, ProductId = 1 },
                new PopularProduct() { Id = 2, ProductId = 19 },
                new PopularProduct() { Id = 3, ProductId = 20 },
                new PopularProduct() { Id = 4, ProductId = 21 },
                new PopularProduct() { Id = 5, ProductId = 24 }
                );
        }

        private void SeedPopularAnnouncement(ReklamaContext context)
        {
            context.PopularAnnouncement.AddOrUpdate(
                new PopularAnnouncement() { Id = 1, AnnouncementId = 19 },
                new PopularAnnouncement() { Id = 2, AnnouncementId = 20 },
                new PopularAnnouncement() { Id = 3, AnnouncementId = 21 },
                new PopularAnnouncement() { Id = 4, AnnouncementId = 11 },
                new PopularAnnouncement() { Id = 5, AnnouncementId = 12 },
                new PopularAnnouncement() { Id = 6, AnnouncementId = 5 });


        }
        private void SeedMainPageArticleConfig(ReklamaContext context)
        {
            context.MainPageArticleConfigs.AddOrUpdate(
                new MainPageArticleConfig()
                    {
                        Article1Id = 26,
                        Article2Id = 25,
                        Article3Id = 24,
                        Article4Id = 22
                    }
                );
        }
    }
}
