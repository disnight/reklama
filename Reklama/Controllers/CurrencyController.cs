using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Repository.Shared;
using Reklama.Models;

namespace Reklama.Controllers
{
    public class CurrencyController : Controller
    {
        private ReklamaContext rc = new ReklamaContext();

        private ICurrencyRepository _currencyRepository;

        public CurrencyController(ICurrencyRepository currencyRepository)
        {
            _currencyRepository = currencyRepository;
            _currencyRepository.Context = rc;
        }

        //
        // GET: /Currency/List

        public ActionResult SwitcherList()
        {
            var currencies = _currencyRepository.Read();
            return View(currencies);
        }


        protected override void Dispose(bool disposing)
        {
            rc.Dispose();
            base.Dispose(disposing);
        }
    }
}
