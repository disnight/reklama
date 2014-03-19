using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Entity.Admin;
using Domain.Repository.Admin;
using Reklama.Models.Shared;

namespace Reklama.Models.Admin
{
    public class MainPageArticleConfigModel: BaseModel<MainPageArticleConfig>, IMainPageArticleConfigRepository
    {
        public MainPageArticleConfig ReadConfig()
        {
            return ((ReklamaContext)Context).MainPageArticleConfigs
                .Include("Article1")
                .Include("Article2")
                .Include("Article3")
                .Include("Article4")
                .Include("Article1.Comments")
                .Include("Article2.Comments")
                .Include("Article3.Comments")
                .Include("Article4.Comments")
                .First();
        }
    }
}