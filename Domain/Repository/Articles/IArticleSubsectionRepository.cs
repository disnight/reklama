using Domain.Entity.Articles;
using Domain.Repository.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository.Articles
{
    public interface IArticleSubsectionRepository: IRepository<ArticleSubsection>
    {
        ArticleSubsection ReadByName(string name);
        IQueryable<ArticleSubsection> ReadBySection(int sectionId);
        IEnumerable<ArticleSubsection> ReadLimit(int sectionId, int limit);
    }
}
