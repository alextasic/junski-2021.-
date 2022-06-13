using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Template.Models
{
    public class Sastojak{
        [Key]
        public int ID{get;set;}

        public string Naziv{get;set;}

        [JsonIgnore]
        public List<ProizvodSastojak> ProizvodSastojak{get;set;}

        [JsonIgnore]
        public List<ProdavnicaSastojak> ProdavnicaSastojak{get;set;}
    }
        
}
