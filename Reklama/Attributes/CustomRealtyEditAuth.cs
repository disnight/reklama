using System;
using System.Web;
using System.Web.Mvc;
using Reklama.Services;

namespace Reklama.Attributes
{
    public class CustomRealtyEditAuth : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var anonymousService = new AnonymousUserService();

            object routId;
            switch (httpContext.Request.HttpMethod)
            {
                case "POST":
                    routId = httpContext.Request.Params["Id"];
                    break;
                case "GET":
                    routId = httpContext.Request.RequestContext.RouteData.Values["id"];
                    break;
                default:
                    routId = httpContext.Request.RequestContext.RouteData.Values["id"];
                    break;
            }

            var annId = 0;
            if (routId != null)
                annId = Convert.ToInt32(routId);

            if (annId == 0 || !anonymousService.IsUserCanEditRealty(annId))
            {
                return base.AuthorizeCore(httpContext);
            }
            return true;
        }
    }
}