using Domain.Entity.Admin;
using Domain.Repository.Admin;
using Reklama.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Reklama.Models.Admin
{
    public class ArticleConfigModel: BaseModel<ArticleConfig>, IArticleConfigRepository
    {
        public ArticleConfig ReadConfig()
        {
            return ((ReklamaContext)Context).ArticleConfigs
                .Include("Best1")
                .Include("Best2")
                .Include("Best3")
                .Include("Best4")
                .Include("Best1.Comments")
                .Include("Best2.Comments")
                .Include("Best3.Comments")
                .Include("Best4.Comments")
                .First();
        }
    }
}