using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity.Articles;
using Domain.Repository.Shared;

namespace Domain.Repository.Articles
{
    public interface IArticleRepository: IRepository<Article>
    {
        IEnumerable<Article> GetBestToday4Articles();
        IEnumerable<Article> GetBestToday4ArticlesBySubsection(int subsectionId);
        IQueryable<Article> ReadByDate();
        void IncrementViewsCount(Article article);
        IQueryable<Article> ReadLastArticle();
        IQueryable<Article> ReadInactive();
    }
}
