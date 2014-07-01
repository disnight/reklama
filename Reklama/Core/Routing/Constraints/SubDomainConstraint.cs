using System.Web;
using System.Web.Routing;

namespace Reklama.Core.Routing.Constraints
{
    public class SubDomainConstraint : ISubDomainConstraint
    {
        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (values.ContainsKey(parameterName))
            {
                switch (values[parameterName].ToString().ToLower())
                {
                    //case "jay":
                    //case "ulag": //auto
                    //case "bildiris":
                    //case "haryt":
                    //case "habarlar":
                    //case "forum":
                    case "www":
                    case "yourdomain":
                        return false;
                    default:
                        return true;
                }
            }
            return false;
        }
    }
}