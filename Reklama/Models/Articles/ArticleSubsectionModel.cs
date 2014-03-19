using Domain.Entity.Articles;
using Domain.Repository.Articles;
using Reklama.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Reklama.Models.Articles
{
    public class ArticleSubsectionModel : BaseModel<ArticleSubsection>, IArticleSubsectionRepository
    {
        public ArticleSubsection ReadByName(string name)
        {
            return Context.Set<ArticleSubsection>().Include("Section").First(ss => ss.Name.Equals(name));
        }

        public IQueryable<ArticleSubsection> ReadBySection(int sectionId)
        {
            return
                from s in Context.Set<ArticleSubsection>().Include("Section") 
                where s.SectionId.Equals(sectionId) 
                orderby s.Name 
                select s;
        }


        public IEnumerable<ArticleSubsection> ReadLimit(int sectionId, int limit)
        {
            return Context.Set<ArticleSubsection>().Include("Section").Where(s => s.SectionId == sectionId).Take(limit).ToArray();
        }
    }
}