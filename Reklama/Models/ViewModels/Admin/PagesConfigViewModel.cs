using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Entity.Admin;
using Domain.Entity.Other;

namespace Reklama.Models.ViewModels.Admin
{
    public class PagesConfigViewModel
    {
        public Config PageConfig { get; set; }
        public IQueryable<Page> Pages { get; set; }
        public int SelectedPageId { get; private set; }

        public PagesConfigViewModel() { }

        public PagesConfigViewModel(Config pageConfig, IQueryable<Page> pages, int selectedPageId)
        {
            PageConfig = pageConfig;
            Pages = pages;
            SelectedPageId = selectedPageId;
        }
    }
}