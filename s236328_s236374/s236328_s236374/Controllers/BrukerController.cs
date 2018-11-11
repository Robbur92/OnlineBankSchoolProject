using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using s236328_s236374.Models;

namespace s236328_s236374.Controllers
{
    public class BrukerController : Controller
    {
        public ActionResult Index()
        {
            using (BrukerContext db = new BrukerContext())
            {
                return View(db.Brukere.ToList());
            }
        }
        public ActionResult Login()
        {
            return View();
        }

        //Sjekker om Personnummer og passord stemmer, hvis den gjør det logger man inn
        [HttpPost]
        public ActionResult Login(Bruker brukerkonto)
        {
            using (BrukerContext db = new BrukerContext())
            {
                var usr = db.Brukere.Single(u => u.Personnummer == brukerkonto.Personnummer && u.Passord == brukerkonto.Passord);
                if (usr != null)
                {
                    Session["UserID"] = usr.UserID.ToString();
                    Session["Personnummer"] = usr.Personnummer.ToString();
                    return RedirectToAction("ListAlleKontoer", "Konto");
                }
                else
                {
                    ModelState.AddModelError("", "Personnummer eller passord er feil");
                }
            }
            return View();
        }
    }
}