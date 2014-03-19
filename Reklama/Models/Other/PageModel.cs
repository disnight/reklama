using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Entity.Other;
using Domain.Repository.Other;
using Reklama.Models.Shared;

namespace Reklama.Models.Other
{
    public class PageModel: BaseModel<Page>, IPageRepository
    {
    }
}