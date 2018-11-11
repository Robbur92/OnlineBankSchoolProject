using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using s236328_s236374.Models;
using System.Data.Entity;

namespace s236328_s236374.Controllers
{
    public class BetalingController : Controller
    {
        public ActionResult ListAlleBetalinger()
        {
            var db = new Models.BrukerContext();
            List<Models.Betaling> listeAvBetalinger = db.Betalinger.ToList();
            return View(listeAvBetalinger);
        }

        public ActionResult OpprettBetaling()
        {
            return View();
        }

        //Her blir en ny betaling lagt til i databasen, vi har med personnummer for å linke til konto.
        [HttpPost]
        public ActionResult OpprettBetaling(FormCollection innListe)
        {
            try
            {
                using (var db = new BrukerContext())
                {
                    var nyBetaling = new Models.Betaling();
                   
                    nyBetaling.navn = innListe["Navn"];
                    nyBetaling.kontonr = innListe["Kontonr"];
                    nyBetaling.personnummer = HttpContext.Session["Personnummer"].ToString();               
                    nyBetaling.sum = innListe["Sum"] + "kr";
                    nyBetaling.dato = innListe["Dato"];

                    db.Betalinger.Add(nyBetaling);
                    db.SaveChanges();
                    return RedirectToAction("ListAlleBetalinger");
                }
            }
            catch (Exception feil)
            {
                return View();
            }
        }

        public ActionResult Slettbetaling()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Slettbetaling(FormCollection slett, int id =0)
        {
            var db = new BrukerContext();
            Betaling betaling = db.Betalinger.Find(id);
            if(betaling == null)
            {
                return HttpNotFound();
            }
            db.Betalinger.Remove(betaling);
            db.SaveChanges();
            return RedirectToAction("ListAlleBetalinger");
        }
        public ActionResult EndreBetaling(int id = 0)
        {
            var db = new BrukerContext();
            Betaling betaling = db.Betalinger.Find(id);
            if(betaling == null)
            {
                return HttpNotFound();
            }
            return View(betaling);
        }
        [HttpPost]
        public ActionResult EndreBetaling(Betaling betaling)
        {
            var db = new BrukerContext();
            if (ModelState.IsValid)
            {
                using (db)
                {
                    var automatisk = new Betaling();
                    automatisk.personnummer = HttpContext.Session["Personnummer"].ToString();
                    db.Entry(betaling).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("ListAlleBetalinger");
                }

            }
            return View(betaling);

        }
    }
}