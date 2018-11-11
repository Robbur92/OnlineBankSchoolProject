using s236328_s236374.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace s236328_s236374
{
    public class BrukerDatabase
    {
        //Metode som henter ut kontoene til en person
        public List<Konto> HentKonti(string Fødselsnummer)
        {
            var db = new BrukerContext();
            return db.Kontoer.Where(k => k.Personnummer.Equals(Fødselsnummer)).ToList();
        }
        //Metode som henter ut betalingene til kontoene
        public List<Betaling> HentBetalinger(string Personnummer)
        {
            var db = new BrukerContext();
            return db.Betalinger.Where(k => k.personnummer.Equals(Personnummer)).ToList();
        }
    }
}