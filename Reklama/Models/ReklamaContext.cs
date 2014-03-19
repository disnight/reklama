using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Domain.Entity.Announcements;
using Domain.Entity.Admin;
using Domain.Entity.Articles;
using Domain.Entity.Common;
using Domain.Entity.Other;
using Domain.Entity.Realty;
using Domain.Entity.Shared;
using Reklama.Models.Mappers;
using Domain.Entity.Catalogs;

namespace Reklama.Models
{
    public class ReklamaContext: DbContext
    {
        public ReklamaContext(): base("DefaultConnection") { }

        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Subsection> Subsections { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<AnnouncementImage> AnnouncementImages { get; set; }
        public DbSet<AnnouncementComment> AnnouncementComments { get; set; }
        public DbSet<AnnouncementBookmark> AnnouncementBookmarks { get; set; }
        public DbSet<AnnouncementsPremiumsRef> AnnouncementsPremiumsRefs { get; set; }

        //Admin panel
        public DbSet<Banner> Banners { get; set; }
        public DbSet<ActualArticle> ActualArticles { get; set; }
        public DbSet<ArticleAndReviewBlock> ArticlesAndReviewsBlock { get; set; }
        public DbSet<BannerSection> BannersSection { get; set; }
        public DbSet<Config> Configs { get; set; }
        public DbSet<GraphicBannerWithText> GraphicBannersWithText { get; set; }
        public DbSet<NewInCatalog> NewsInCatalog { get; set; }
        public DbSet<NewSectionInCatalog> NewSectionsInCatalog { get; set; }
        public DbSet<PopularProduct> PopularProducts { get; set; }
        public DbSet<PopularSectionInCatalog> PopularSectionsInCatalog { get; set; }
        public DbSet<PopularSubsection> PopularSubsections { get; set; }
        public DbSet<Premium> Premiums { get; set; }
        public DbSet<TopForumTopic> TopForumTopics { get; set; }
        public DbSet<AnnouncementConfig> AnnouncementConfigs { get; set; }
        public DbSet<MainPageArticleConfig> MainPageArticleConfigs { get; set; }
        public DbSet<ArticleConfig> ArticleConfigs { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PopularAnnouncement> PopularAnnouncement { get; set; }

        //Realty
        public DbSet<Domain.Entity.Realty.Realty> Realty { get; set; }
        public DbSet<RealtyBookmark> RealtyBookmarks { get; set; }
        public DbSet<RealtyCategory> RealtyCategories { get; set; }
        public DbSet<RealtyComment> RealtyFeedbacks { get; set; }
        public DbSet<RealtyPayment> RealtyPayments { get; set; }
        public DbSet<RealtyPaymentHistory> RealtyPaymentsHistory { get; set; }
        public DbSet<RealtyPhoto> RealtyPhotos { get; set; }
        public DbSet<RealtySection> RealtySections { get; set; }


        //Shared
        public DbSet<City> Cities { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<PrivateMessage> PrivateMessages { get; set; }


        //Other
        public DbSet<Menu> Menus { get; set; } 
        public DbSet<Page> Pages { get; set; }
        public DbSet<MenuPageRef> MenuPageRefs { get; set; }

        public DbSet<Computer> Computers { get; set; }
        public DbSet<ComputerAnnouncementRef> ComputerAnnouncementRefs { get; set; }
        public DbSet<ComputerRealtyRef> ComputerRealtyRefs { get; set; }



        // Article
        public DbSet<Article> Articles { get; set; }
        public DbSet<ArticleComment> ArticleComments { get; set; }
        public DbSet<ArticleSection> ArticleSections { get; set; }
        public DbSet<ArticleSubsection> ArticleSubsections { get; set; }

        //Catalog
        public DbSet<CatalogConfig> CatalogConfigs { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<CatalogCategory> CatalogCategories { get; set; }
        public DbSet<CatalogSecondCategory> CatalogSecondCategories { get; set; }
        public DbSet<CatalogThirdCategory> CatalogThirdCategories { get; set; }
        public DbSet<CatalogParamSubsection> CatalogParamSubsections { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductFeedback> ProductFeedbacks { get; set; }
        public DbSet<ProductBookmark> ProductBookmarks { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductParam> ProductParams { get; set; }
        public DbSet<ProductParamValue> ProductParamValues { get; set; }
        public DbSet<ProductSectionParamRef> ProductSectionParamRefs { get; set; }
        public DbSet<ProductSubsectionParamRef> ProductSubsectionParamRefs { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<ShopComment> ShopComments { get; set; }
        public DbSet<ShopCategoryRef> ShopCategoryRefs { get; set; }
        public DbSet<ShopProductRef> ShopProductRefs { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new UserProfileConfiguration());
            modelBuilder.Configurations.Add(new CategoryConfiguration());
            modelBuilder.Configurations.Add(new SectionConfiguration());
            modelBuilder.Configurations.Add(new SubsectionConfiguration());
            modelBuilder.Configurations.Add(new AnnouncementConfiguration());
            modelBuilder.Configurations.Add(new AnnouncementImageConfiguration());
            modelBuilder.Configurations.Add(new AnnouncementCommentConfiguration());
            modelBuilder.Configurations.Add(new AnnouncementBookmarkConfiguration());
            modelBuilder.Configurations.Add(new AnnouncementsPremiumsRefConfiguration());

            //Admin panel
            modelBuilder.Configurations.Add(new BannerConfiguration());
            modelBuilder.Configurations.Add(new ActualArticleConfiguration());
            modelBuilder.Configurations.Add(new ArticleAndReviewBlockConfiguration());
            modelBuilder.Configurations.Add(new BannerSectionConfiguration());
            modelBuilder.Configurations.Add(new ConfigConfiguration());
            modelBuilder.Configurations.Add(new GraphicBannerWithTextConfiguration());
            modelBuilder.Configurations.Add(new NewInCatalogConfiguration());
            modelBuilder.Configurations.Add(new NewSectionInCatalogConfiguration());
            modelBuilder.Configurations.Add(new PopularProductConfiguration());
            modelBuilder.Configurations.Add(new PopularSectionInCatalogConfiguration());
            modelBuilder.Configurations.Add(new PopularSubsectionConfiguration());
            modelBuilder.Configurations.Add(new PremiumConfiguration());
            modelBuilder.Configurations.Add(new TopForumTopicConfiguration());
            modelBuilder.Configurations.Add(new AnnouncementConfigConfiguration());
            modelBuilder.Configurations.Add(new ArticleConfigConfiguration());
            modelBuilder.Configurations.Add(new PaymentConfiguration());

            //Realty
            modelBuilder.Configurations.Add(new RealtyConfiguration());
            modelBuilder.Configurations.Add(new RealtyBookmarkConfiguration());
            modelBuilder.Configurations.Add(new RealtyCategoryConfiguration());
            modelBuilder.Configurations.Add(new RealtyCommentConfiguration());
            modelBuilder.Configurations.Add(new RealtyPaymentConfiguration());
            modelBuilder.Configurations.Add(new RealtyPaymentHistoryConfiguration());
            modelBuilder.Configurations.Add(new RealtyPhotoConfiguration());
            modelBuilder.Configurations.Add(new RealtySectionConfiguration());
            

            //Shared
            modelBuilder.Configurations.Add(new CityConfiguration());
            modelBuilder.Configurations.Add(new CurrencyConfiguration());
            modelBuilder.Configurations.Add(new PrivateMessageConfiguration());


            // Other
            modelBuilder.Configurations.Add(new MenuConfiguration());
            modelBuilder.Configurations.Add(new PageConfiguration());
            modelBuilder.Configurations.Add(new MenuPageRefConfiguration());
            modelBuilder.Configurations.Add(new ComputerConfiguration());
            modelBuilder.Configurations.Add(new ComputerAnnouncementRefConfiguration());
            modelBuilder.Configurations.Add(new ComputerRealtyRefConfiguration());


            // Article
            modelBuilder.Configurations.Add(new ArticleConfiguration());
            modelBuilder.Configurations.Add(new ArticleCommentConfiguration());

            //Catalog
            modelBuilder.Configurations.Add(new CatalogConfigConfiguration());
            modelBuilder.Configurations.Add(new ShopConfiguration());
            modelBuilder.Configurations.Add(new CatalogCategoryConfiguration());
            modelBuilder.Configurations.Add(new CatalogSecondCategoryConfiguration());
            modelBuilder.Configurations.Add(new CatalogThirdCategoryConfiguration());
            modelBuilder.Configurations.Add(new CatalogParamSubsectionConfiguration());
            modelBuilder.Configurations.Add(new ProductConfiguration());
            modelBuilder.Configurations.Add(new ProductFeedbackConfiguration());
            modelBuilder.Configurations.Add(new ProductBookmarkConfiguration());
            modelBuilder.Configurations.Add(new ProductImageConfiguration());
            modelBuilder.Configurations.Add(new ProductParamConfiguration());
            modelBuilder.Configurations.Add(new ProductParamValueConfiguration());
            modelBuilder.Configurations.Add(new ProductSectionParamRefConfiguration());
            modelBuilder.Configurations.Add(new ProductSubsectionParamRefConfiguration());
            modelBuilder.Configurations.Add(new UnitConfiguration());
            modelBuilder.Configurations.Add(new ShopCommentConfiguration());
            modelBuilder.Configurations.Add(new ShopCategoryRefConfiguration());
            modelBuilder.Configurations.Add(new ShopProductRefConfiguration());
        }
    }
}