using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Domain.Entity.Other;
using Domain.Repository.Announcements;
using Domain.Repository.Articles;
using Domain.Repository.Other;
using Domain.Repository.Realty;
using Domain.Repository.Shared;
using Domain.Repository.Catalogs;
using Ninject;
using Reklama.Models;
using Reklama.Models.Announcements;
using Reklama.Models.Articles;
using Reklama.Models.Other;
using Reklama.Models.Realty;
using Reklama.Models.Shared;
using Reklama.Models.Admin;
using Reklama.Models.Catalog;
using Domain.Repository.Admin;
using Reklama.Models.SortModels;
using Reklama.Models.ViewModels.Announcement;

namespace Reklama.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IKernel _kernel;

        public NinjectDependencyResolver()
        {
            _kernel = new StandardKernel();
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }


        private void AddBindings()
        {
            // Announcement
            _kernel.Bind<IAnnouncementRepository>().To<AnnouncementModel>();
            _kernel.Bind<IAnnouncementBookmarkRepository>().To<AnnouncementBookmarkModel>();
            _kernel.Bind<IAnnouncementImageRepository>().To<AnnouncementImageModel>();
            _kernel.Bind<ISectionRepository>().To<SectionModel>();
            _kernel.Bind<ISubsectionRepository>().To<SubsectionModel>();
            _kernel.Bind<ICategoryRepository>().To<CategoryModel>();
            _kernel.Bind<FilterParams>().To<FilterParams>();
            _kernel.Bind<IAnnouncementCommentRepository>().To<AnnouncementCommentModel>();
            _kernel.Bind<IAnnouncementsPremiumsRefRepository>().To<AnnouncementsPremiumsRefModel>();


            //Realty
            _kernel.Bind<IRealtyBookmarkRepository>().To<RealtyBookmarkModel>();
            _kernel.Bind<IRealtyCategoryRepository>().To<RealtyCategoryModel>();
            _kernel.Bind<IRealtyCommentRepository>().To<RealtyFeedbackModel>();
            _kernel.Bind<IRealtyPaymentHistoryRepository>().To<RealtyPaymentHistoryModel>();
            _kernel.Bind<IRealtyPaymentRepository>().To<RealtyPaymentModel>();
            _kernel.Bind<IRealtyPhotoRepository>().To<RealtyPhotoModel>();
            _kernel.Bind<IRealtyRepository>().To<RealtyModel>();
            _kernel.Bind<IRealtySectionRepository>().To<RealtySectionModel>();


            //Admin panel
            _kernel.Bind<IActualArticleRepository>().To<ActualArticleModel>();
            _kernel.Bind<IArticleAndReviewBlockRepository>().To<ArticleAndReviewBlockModel>();
            _kernel.Bind<IBannerRepository>().To<BannerModel>();
            _kernel.Bind<IBannerSectionRepository>().To<BannerSectionModel>();
            _kernel.Bind<IConfigRepository>().To<ConfigModel>();
            _kernel.Bind<IGraphicBannerWithTextRepository>().To<GraphicBannerWithTextModel>();
            _kernel.Bind<INewInCatalogRepository>().To<NewInCatalogModel>();
            _kernel.Bind<INewSectionInCatalogRepository>().To<NewSectionInCatalogModel>();
            _kernel.Bind<IPopularProductRepository>().To<PopularProductModel>();
            _kernel.Bind<IPopularSectionInCatalogRepository>().To<PopularSectionInCatalogModel>();
            _kernel.Bind<IPopularSubsectionRepository>().To<PopularSubsectionModel>();
            _kernel.Bind<IPremiumRepository>().To<PremiumModel>();
            _kernel.Bind<ITopForumTopicRepository>().To<TopForumTopicModel>();
            _kernel.Bind<IAnnouncementConfigRepository>().To<AnnouncementConfigModel>();
            _kernel.Bind<IArticleConfigRepository>().To<ArticleConfigModel>();
            _kernel.Bind<IMainPageArticleConfigRepository>().To<MainPageArticleConfigModel>();
            _kernel.Bind<IPaymentRepository>().To<PaymentModel>();
            _kernel.Bind<IPopularAnnouncementRepository>().To<PopularAnnoucementModel>();


            //Shared
            _kernel.Bind<ICityRepository>().To<CityModel>();
            _kernel.Bind<ICurrencyRepository>().To<CurrencyModel>();
            _kernel.Bind<IProfileRepository>().To<ProfileModel>();
            _kernel.Bind<IPrivateMessageRepository>().To<PrivateMessageModel>();


            // Article
            _kernel.Bind<IArticleRepository>().To<ArticleModel>();
            _kernel.Bind<IArticleCommentRepository>().To<ArticleCommentModel>();
            _kernel.Bind<IArticleSectionRepository>().To<ArticleSectionModel>();
            _kernel.Bind<IArticleSubsectionRepository>().To<ArticleSubsectionModel>();


            //Other
            _kernel.Bind<IMenuRepository>().To<MenuModel>();
            _kernel.Bind<IPageRepository>().To<PageModel>();
            _kernel.Bind<IMenuPageRefRepository>().To<MenuPageRefModel>();
            _kernel.Bind<IComputerRepository>().To<ComputerModel>();
            _kernel.Bind<IComputerAnnouncementRefRepository>().To<ComputerAnnouncementRefModel>();
            _kernel.Bind<IComputerRealtyRefRepository>().To<ComputerRealtyRefModel>();


            //Catalog
            _kernel.Bind<ICatalogConfigRepository>().To<CatalogConfigModel>();
            _kernel.Bind<IShopRepository>().To<ShopModel>();
            _kernel.Bind<ICatalogCategoryRepository>().To<CatalogCategoryModel>();
            _kernel.Bind<ICatalogSecondCategoryRepository>().To<CatalogSecondCategoryModel>();
            _kernel.Bind<ICatalogThirdCategoryRepository>().To<CatalogThirdCategoryModel>();
            _kernel.Bind<ICatalogParamSubsectionRepository>().To<CatalogParamSubsectionModel>();
            _kernel.Bind<IProductRepository>().To<ProductModel>();
            _kernel.Bind<IProductFeedbackRepository>().To<ProductFeedbackModel>();
            _kernel.Bind<IProductBookmarkRepository>().To<ProductBookmarkModel>();
            _kernel.Bind<IProductParamRepository>().To<ProductParamModel>();
            _kernel.Bind<IProductParamValueRepository>().To<ProductParamValueModel>();
            _kernel.Bind<IProductSectionParamRefRepository>().To<ProductSectionParamRefsModel>();
            _kernel.Bind<IProductSubsectionParamRefRepository>().To<ProductSubsectionParamRefsModel>();
            _kernel.Bind<IUnitRepository>().To<UnitModel>();
            _kernel.Bind<IShopCommentRepository>().To<ShopCommentModel>();
            _kernel.Bind<IShopCategoryRefRepository>().To<ShopCategoryRefModel>();
            _kernel.Bind<IShopProductRefRepository>().To<ShopProductRefModel>();
            _kernel.Bind<IProductImageRepository>().To<ProductImageModel>();
        }
    }
}