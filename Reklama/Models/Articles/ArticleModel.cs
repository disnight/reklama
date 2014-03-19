using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Entity.Articles;
using Domain.Repository.Articles;
using Reklama.Models.Shared;

namespace Reklama.Models.Articles
{
    public class ArticleModel: BaseModel<Article>, IArticleRepository
    {
        public IEnumerable<Article> GetBestToday4Articles()
        {
            return GetTodayArticlesQuery().OrderByDescending(a => a.ViewsCount).Take(4);
        }

        public IEnumerable<Article> GetBestToday4ArticlesBySubsection(int subsectionId)
        {
            return GetTodayArticlesQuery().Where(a => a.SubsectionId == subsectionId)
                .OrderByDescending(a => a.ViewsCount).Take(4);
        }

        protected IQueryable<Article> GetTodayArticlesQuery()
        {
            return Read().Where(
                a => a.CreatedAt.Year == DateTime.Today.Year
                    && a.CreatedAt.Month == DateTime.Today.Month
                    && a.CreatedAt.Day == DateTime.Today.Day
                );
        }

        public IQueryable<Article> ReadByDate()
        {
            return Read().OrderByDescending(a => a.CreatedAt);
        }


        public void IncrementViewsCount(Article article)
        {
            article.ViewsCount++;
            Save(article);
        }

        public IQueryable<Article> ReadLastArticle()
        {
            return ReadByDate().Take(4);
        }


        public override IQueryable<Article> Read()
        {
            return base.Read().Where(a => a.IsActive).OrderByDescending(a => a.Id);
        }


        public IQueryable<Article> ReadInactive()
        {
            return base.Read().Where(a => !a.IsActive);
        }
    }
}