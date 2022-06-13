using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Template.Models
{
    public class Proizvod{
        [Key]
        public int ID{get;set;}

        public string Naziv{get;set;}

        public virtual Prodavnica Prodavnica{get;set;}

        [JsonIgnore]
        public List<ProizvodSastojak> ProizvodSastojak{get;set;}
    }
        
}
