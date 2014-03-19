using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Reklama.App_Start
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
            name: "Search",
            url: "Search",
            defaults: new
                {
                    controller = "Search",
                    action = "Search",
                }
            );

            routes.MapRoute(
                name: "MyAnnouncements",
                url: "My/Announcements",
                defaults: new { controller = "Bookmarks", action = "MyAnnouncements" }
            );

            routes.MapRoute(
                name: "MyRealty",
                url: "My/Realty",
                defaults: new { controller = "Bookmarks", action = "MyRealty" }
            );

            routes.MapRoute(
                name: "MyAnnouncementBookmarks",
                url: "My/Bookmarks/Announcemets",
                defaults: new { controller = "Bookmarks", action = "Announcements" }
            );

            routes.MapRoute(
                name: "MyRealtyBookmarks",
                url: "My/Bookmarks/Realty",
                defaults: new { controller = "Bookmarks", action = "Realty" }
            );

            routes.MapRoute(
                name: "MyProductBookmarks",
                url: "My/Bookmarks/Products",
                defaults: new { controller = "Bookmarks", action = "Products" }
            );

            routes.MapRoute(
                name: "RestrictedAccess",
                url: "RestrictedAccess.html",
                defaults: new { controller = "Home", action = "RestrictedAccess" }
            );

            routes.Add("DomainRoute", new DomainRoute(
                "jay.reklama.tm", // Domain with parameters
                "Realty",//Controller name
                "{action}/{id}",    // URL with parameters 
                new { controller = "Realty", action = "List", id = "" }  // Parameter defaults 
            ));

            #region RedirectToSubdomain
            routes.MapRoute(
                name: "IgnoreRedirectRealtyCreate",
                url: "Realty/Create",
                defaults: new { controller = "Realty", action = "Create" }
                );

            routes.MapRoute(
                name: "IgnoreRedirectRealtyEdit",
                url: "Realty/Edit/{id}",
                defaults: new { controller = "Realty", action = "Edit", id = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "RedirectToSubdomain",
                url: "Realty/{actionName}/{id}",
                defaults: new { controller = "Realty", action = "RedirectToSubdomain", id = UrlParameter.Optional, actionName = UrlParameter.Optional }
                );
            #endregion

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}