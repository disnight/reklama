using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Entity.Admin;
using Domain.Repository.Admin;
using Reklama.Models.Shared;

namespace Reklama.Models.Admin
{
    public class CatalogConfigModel : BaseModel<CatalogConfig>, ICatalogConfigRepository
    {
        public CatalogConfig ReadConfig()
        {
            return Read().First();
        }
    }
}
