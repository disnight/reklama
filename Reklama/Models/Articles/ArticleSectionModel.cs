using Domain.Entity.Articles;
using Domain.Repository.Articles;
using Reklama.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Reklama.Models.Articles
{
    public class ArticleSectionModel : BaseModel<ArticleSection>, IArticleSectionRepository
    {
        public ArticleSection ReadByName(string name)
        {
            return Context.Set<ArticleSection>().First(s => s.Name.Equals(name));
        }
    }
}