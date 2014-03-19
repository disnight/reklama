using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading;
using System.Web.Mvc;
using Domain.Repository.Shared;
using Reklama.Infrastructure;
using Reklama.Models;
using Reklama.Models.Shared;
using WebMatrix.WebData;

namespace Reklama.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class InitializeSimpleMembershipAttribute : ActionFilterAttribute
    {
        private static SimpleMembershipInitializer _initializer;
        private static object _initializerLock = new object();
        private static bool _isInitialized;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Ensure ASP.NET Simple Membership is initialized only once per app start
            LazyInitializer.EnsureInitialized(ref _initializer, ref _isInitialized, ref _initializerLock);
        }

        private class SimpleMembershipInitializer
        {
            private object thisLock = new object();

            public SimpleMembershipInitializer()
            {
                //var context = new ReklamaContext();
                //var context = DependencyResolver.Current.GetService<IReklamaContextOperation>()
                //    .GetReklamaContext();
                //var context = _context.GetContext();

                using (var context = new ReklamaContext())
                {
                    try
                    {
                        lock (thisLock)
                        {
                            if (!context.Database.Exists())
                            {
                                // Create the SimpleMembership database without Entity Framework migration schema
                                Database.SetInitializer<ReklamaContext>(null);
                                ((IObjectContextAdapter)context).ObjectContext.CreateDatabase();
                            }
                        }

                        lock (thisLock)
                        {
                            if (!WebSecurity.Initialized)
                            {
                                Database.SetInitializer<ReklamaContext>(null);
                                WebSecurity.InitializeDatabaseConnection("UserDbConnection", "UserProfile", "UserId",
                                                                         "Email", autoCreateTables: true);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new InvalidOperationException("The ASP.NET Simple Membership database could not be initialized. For more information, please see http://go.microsoft.com/fwlink/?LinkId=256588", ex);
                    }
                }
            }
        }
    }
}
