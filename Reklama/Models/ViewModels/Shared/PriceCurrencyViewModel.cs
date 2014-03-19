using Domain.Entity.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Reklama.Models.ViewModels.Shared
{
    public class PriceCurrencyViewModel
    {
        public bool IsAuction { get; set; }
        public decimal? Price { get; set; }
        public Currency Currency { get; set; }
    }
}