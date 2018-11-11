using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration.Conventions;


namespace s236328_s236374.Models
{
    public class BrukerContext : DbContext
    {
        public BrukerContext()
        : base("name=Bruker")
        {
            Database.CreateIfNotExists();
        }
        public DbSet<Bruker> Brukere { get; set; }
        public DbSet<Konto> Kontoer { get; set; }
        public DbSet<Betaling> Betalinger { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }

    public class Bruker
    {
        [Key]
        public int UserID { get; set; }

        public String Fornavn { get; set; }

        public String Etternavn { get; set; }

        [Required(ErrorMessage = "Personnummer er påkrevd.")]
        public String Personnummer { get; set; }

        [Required(ErrorMessage = "Passord er påkrevd.")]
        [DataType(DataType.Password)]
        public String Passord { get; set; }
        public virtual List<Konto> konto { get; set; }

    }
    public class Konto
    {
        [Key]
        public int id { get; set; }
        public string kontonr { get; set; }
        public string kontotype { get; set; }
        public string saldo { get; set; }
        public string Personnummer { get; set; }
        public virtual List<Betaling> betaling { get; set; }
        public virtual Bruker bruker { get; set; }


    }
    public class Betaling
    {
        [Key]
        public int id { get; set; }
        [Required(ErrorMessage = "Navn på mottaker er påkrevd")]
        public string navn { get; set; }
        [Required(ErrorMessage = "Kontonummer til mottaker er påkrevd")]
        public string kontonr { get; set; }
        public string personnummer { get; set; }
        [Required(ErrorMessage = "Skriv inn et beløp")]
        public string sum { get; set; }
        [Required(ErrorMessage = "Velg en dato som beløpet skal sendes på")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public string dato { get; set; }
        public virtual Konto konto { get; set; }
    }

}