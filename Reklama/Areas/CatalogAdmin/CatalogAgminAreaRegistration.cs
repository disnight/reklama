using System.Web.Mvc;

namespace Reklama.Areas.CatalogAdmin
{
    public class CatalogAdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "CatalogAdmin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "CatalogAdmin_default",
                "CatalogAdmin/{controller}/{action}/{id}",
                new {  controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "Reklama.Areas.CatalogAdmin.Controllers" }
            );
        }
    }
}
