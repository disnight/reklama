using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Repository.Admin;
using Domain.Entity.Admin;
using Reklama.Models.Shared;

namespace Reklama.Models.Admin
{
    public class ActualArticleModel: BaseModel<ActualArticle>, IActualArticleRepository
    {
    }
}