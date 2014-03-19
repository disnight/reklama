using Domain.Entity.Articles;
using Domain.Repository.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository.Articles
{
    public interface IArticleSectionRepository: IRepository<ArticleSection>
    {
        ArticleSection ReadByName(string name);
    }
}
