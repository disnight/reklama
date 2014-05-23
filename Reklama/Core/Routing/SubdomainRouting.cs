using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Reklama.Core.Routing
{
    public class SubdomainRouting : RouteBase
    {
        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            if (httpContext.Request.Url == null)
                return null;

            var host = httpContext.Request.Url.Host;

            var index = host.IndexOf(".");
            var path = httpContext.Request.Url.PathAndQuery;
            if (path.IndexOf('/') == 0)
            {
                path = path.Substring(1);
            }

            var segments = path.Split('/');

            if (index < 0)
                return null;

            var subdomain = host.Substring(0, index);


            var controller = "";
            var action = "";

            var param = new Dictionary<string, object>();

            var isRoute = false;

            switch (subdomain)
            {
                case "auto":
                    controller = (segments.Length > 0 && !String.IsNullOrEmpty(segments[0])) ? segments[0] : "Announcement";
                    action = (segments.Length > 1 && !String.IsNullOrEmpty(segments[1])) ? segments[1] : "List";
                    switch (action)
                    {
                        case "List": param.Add("SectionId", 3); break;
                        case "Details": param.Add("id", 401); break;
                    }
                    isRoute = true;
                    break;
            }

            if (isRoute)
            {
                var routeData = new RouteData(this, new MvcRouteHandler());
                routeData.Values.Add("controller", controller); //Goes to the relevant Controller  class
                routeData.Values.Add("action", action); //Goes to the relevant action method on the specified Controller
                foreach (var p in param)
                {
                    routeData.Values.Add(p.Key, p.Value); //pass subdomain as argument to action method
                }
                return routeData;
            }

            return null;
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            //Implement your formating Url formating here
            return null;
        }
    }
}