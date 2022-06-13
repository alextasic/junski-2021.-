using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Template.Models
{
    public class ProdavnicaSastojak{
        [Key]
        public int ID{get;set;}

        public int Kolicina{get;set;}

        public virtual Sastojak Sastojak{get;set;}

        public virtual Prodavnica Prodavnica{get;set;}
    }
    
}
