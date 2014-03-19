using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Entity.Articles;
using Domain.Repository.Articles;
using Reklama.Models.Shared;

namespace Reklama.Models.Articles
{
    public class ArticleCommentModel: BaseModel<ArticleComment>, IArticleCommentRepository
    {
    }
}