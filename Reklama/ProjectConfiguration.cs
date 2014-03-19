using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Enums;
using Domain.Entity.Admin;
using Reklama.Models;
using Domain.Repository.Admin;
using System.Web.Mvc;

namespace Reklama
{
    public class ProjectConfiguration: IDisposable
    {
        private static ProjectConfiguration _projectConfiguration;

        public static ProjectConfiguration Get {
            get
            {
                if (_projectConfiguration == null)
                {
                    return _projectConfiguration = new ProjectConfiguration();
                }
                return _projectConfiguration;
            }
        }

        public static bool IsAnonymousUserAllowed
        {
            get
            {
                var isAnonymousUserAllowed = Get.Configs.FirstOrDefault(c => c.Name.Equals("AnonimousAnnouncementAdd"));
                return isAnonymousUserAllowed != null && isAnonymousUserAllowed.IsEnable.HasValue &&
                       isAnonymousUserAllowed.IsEnable.Value;
            }
        }

        /// <summary>
        /// Количество записей на странице.<br />
        /// Содержит массив значений количества записей на страницу
        /// для возможности выбора нескольких значений<br />
        /// @type byte[]
        /// </summary>
        public readonly byte[] ItemsOnPage = new byte[] { 15, 25, 35 };

        /// <summary>
        /// Сообщение при отстуствии результата поиска
        /// Выдается когда ничего не нашло
        /// @type string
        /// </summary>
        public static string NotFoundResult = "По вашему запросу не было ничего найдено";

        /// <summary>
        /// Максимальное количество выводимых ссылок для подразделов.<br />
        /// Выводится в блоке под "хлебными крошками" в разделе Details и
        /// List для объявлений<br />
        /// @type byte
        /// </summary>
        public readonly byte MaxCountSubsectionsLinkInBlock = 15;


        /// <summary>
        /// Количество комментариев, отображаемых на одной странице<br />
        /// @type byte
        /// </summary>
        public readonly byte CommentsOnPage = 25;

        /// <summary>
        /// Количество продуктов, отображаемых в базе продуктов на одной странице
        /// @type byte
        /// </summary>
        public readonly byte ProductsOnPageInBasePage = 20;

        /// <summary>
        /// Минимальное количество символов в строке для поля глобального поиска<br />
        /// @type byte
        /// </summary>
        public readonly byte MinSearchStringLength = 3;


        /// <summary>
        /// Минимальная длина слова в строке поиска, начиная с которой производится поиск<br />
        /// @type byte
        /// </summary>
        public readonly byte MinSearchWordLength = 3;


        /// <summary>
        /// Id стандартной валюта на сайте.<br />
        /// Цена на все товары, размещаемые на сайте, указывается в этой валюте
        /// @type string
        /// </summary>
        public readonly int DefaultCurrencyId = 1;


        /// <summary>
        /// Название роли администратора<br />
        /// @type string
        /// </summary>
        public readonly string AdministratorRole = "Administrator";


        /// <summary>
        /// Текст ошибки, которая может возникнуть при невозможности
        /// сохранения данных в базу<br />
        /// @type string
        /// </summary>
        public readonly string DataErrorMessage =
            "Возникла исключительная проблема. Попробуйте еще раз или обратитесь к администратору";

        /// <summary>
        /// Данные почты для рассылки
        /// </summary>
        public readonly string Email = "reklama.tm.activation@gmail.com";
        public readonly string PasswordEmail = "tLD8VNte";
        public readonly string LoginEmail = "reklama.tm.activation";
        public readonly string HostEmail = "smtp.gmail.com";
        public readonly int PortEmail = 25;


        /**
         * Пути к файлам и изображениям
         */
        public readonly Dictionary<string, string> FilePaths = new Dictionary<string, string>()
                                                                 {
                                                                     {"base", AppDomain.CurrentDomain.BaseDirectory},
                                                                     {"articles", "Images/Articles/"},
                                                                     {"users", "Images/Users/"},
                                                                     {"avatars", "Images/Users/Avatars/"},
                                                                     {"realty", "Images/Realty/"},
                                                                     {"products", "Images/Catalog/Product/" },
                                                                     {"shopLogo", "Images/Catalog/ShopLogos/"},
                                                                     {"announcement_thumb", "Images/Thumbnails/Announcement"},
                                                                     {"realty_thumb", "Images/Thumbnails/Realty"},
                                                                     {"product_thumb", "Images/Thumbnails/Product"},
                                                                     {"product_article", "Images/Thumbnails/Article"},
                                                                     {"product_shop", "Images/Thumbnails/Shop"}
                                                                 };


        /// <summary>
        /// Максимальная ширина изображения пользовательской аватарки
        /// </summary>
        public readonly int UserAvatarWidth = 120;


        /// <summary>
        /// Максимальная высота изображения пользовательской аватарки
        /// </summary>
        public readonly int UserAvatarHeight = 80;


        public readonly int AnnouncementImageWidth = 150;
        public readonly int AnnouncementImageHeight = 100;


        /// <summary>
        /// Максимальная ширина и высота изображения для статьи
        /// </summary>
        public readonly int ArticleImageWidth = 480;
        public readonly int ArticleImageHeight = 275;

        /// <summary>
        /// Максимальная ширина и высота изображения для логотипа магазина
        /// </summary>
        public readonly int ShopLogoWidth = 120;
        public readonly int ShopLogoHeight = 45;

        public readonly int ProductImageWidth = 300;
        public readonly int ProductImageHeight = 160;

        public readonly int ProductThumbImageWidth = 150;
        public readonly int ProductThumbImageHeight = 100;

        public readonly int RealtyImageWidth = 150;
        public readonly int RealtyImageHeight = 100;

        /// <summary>
        /// Количество выводимых личных сообщений на странице
        /// </summary>
        public readonly int PrivateMessagesOnPage = 50;


        public IConvertible GetConfigValue(string name)
        {
            return Configs.First(c => c.Name.Equals(name)).Value;
        }


        /**
         * This fields must be read from database
         */
        #region DbSettings

        /// <summary>
        /// Конфигурация для доски объявлений
        /// </summary>
        public AnnouncementConfig AnnouncementConfig { get; private set; }

        /// <summary>
        /// Конфигурация для статей
        /// </summary>
        public ArticleConfig ArticleConfig { get; private set; }

        /// <summary>
        /// Конфигурация статей, которые выводятся на главную
        /// </summary>
        public MainPageArticleConfig MainPageArticleConfig { get; private set; }

        /// <summary>
        /// Конфигурация для каталога
        /// </summary>
        public CatalogConfig CatalogConfig { get; private set; }

        /// <summary>
        /// Глобальная конфигурация
        /// </summary>
        public IEnumerable<Config> Configs { get; private set; }
        #endregion


        private ProjectConfiguration()
        {
            var announcementConfigRepository = DependencyResolver.Current.GetService<IAnnouncementConfigRepository>();
            var articleConfigRepository = DependencyResolver.Current.GetService<IArticleConfigRepository>();
            var mainPageArticleConfigRepository = DependencyResolver.Current.GetService<IMainPageArticleConfigRepository>();
            var catalogConfigRepository = DependencyResolver.Current.GetService<ICatalogConfigRepository>();
            var configRepository = DependencyResolver.Current.GetService<IConfigRepository>();

            using (var context = new ReklamaContext())
            {
                announcementConfigRepository.Context = context;
                articleConfigRepository.Context = context;
                mainPageArticleConfigRepository.Context = context;
                catalogConfigRepository.Context = context;
                configRepository.Context = context;


                AnnouncementConfig = announcementConfigRepository.ReadConfig();
                ArticleConfig = articleConfigRepository.ReadConfig();
                MainPageArticleConfig = mainPageArticleConfigRepository.ReadConfig();
                CatalogConfig = catalogConfigRepository.ReadConfig();
                Configs = configRepository.Read().ToArray();
            }
        }


        public void Dispose()
        {
            _projectConfiguration = null;
            GC.SuppressFinalize(this);
        }
    }
}