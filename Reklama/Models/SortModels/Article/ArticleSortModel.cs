using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Reklama.Models.SortModels.Article
{
    public class ArticleSortModel: PagerSortModel
    {
        public int? SubsectionId { get; set; }

        public ArticleSortModel()
        {
            SubsectionId = null;
        }
    }
}