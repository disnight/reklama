using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity.Articles;
using Domain.Repository.Shared;

namespace Domain.Repository.Articles
{
    public interface IArticleCommentRepository: IRepository<ArticleComment>
    {
    }
}
