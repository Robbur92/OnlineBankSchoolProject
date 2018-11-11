using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace s236328_s236374.Controllers
{
    public class KontoController : Controller
    {
        public ActionResult ListAlleKontoer()
        {
            var db = new Models.BrukerContext();
            List<Models.Konto> listeAvKontoer = db.Kontoer.ToList();
            return View(listeAvKontoer);
        }
    }
}