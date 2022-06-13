using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Template.Models
{
    public class Prodavnica{
        [Key]
        public int ID{get;set;}

        public string Naziv{get;set;}

        public List<Proizvod> Proizvod{get;set;}

        [JsonIgnore]
        public List<ProdavnicaSastojak> ProdavnicaSastojak{get;set;}

    }
        
}
