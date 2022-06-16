using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Template.Models;///menjano

namespace Template.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IspitController : ControllerBase
    {
        IspitDbContext Context { get; set; }

        public IspitController(IspitDbContext context)
        {
            Context = context;
        }
        [Route("PreuzmiProdavnice")]
        [HttpGet]
        public async Task<List<Prodavnica>> PreuzmiProdavnice(){
            var nadjeneProdavnice=await Context.Prodavnica.ToListAsync();
            
            return nadjeneProdavnice;
        }
        [Route("PreuzmiProizvode/{idProd}")]
        [HttpGet]
        public async Task<List<Proizvod>> PreuzmiProizvode(int idProd){
            var nadjeniProizvodi=await Context.Proizvod.Where(p=>p.Prodavnica.ID==idProd).ToListAsync();
         return nadjeniProizvodi;
        }

        [Route("Poruci/{idProdavnice}/{idProizvoda}/{izabranaKolicina}")]
        [HttpPut]
        public async Task<ActionResult> Poruci(int idProdavnice,int idProizvoda,int izabranaKolicina){


            /*var nadjeniNedostajuciSastojci=
            await Context.Sastojak.Include(p=>p.ProizvodSastojak).Where(p=>p.ProizvodSastojak.Any(q=>q.Proizvod.ID==idProizvoda))
                                .Include(p=>p.ProdavnicaSastojak).Where(p=>p.ProdavnicaSastojak.Any((t=>t.Prodavnica.ID==idProdavnice && t.Kolicina<izabranaKolicina))).ToListAsync();*/

            ///vraca proizvode kojih nema dovoljno na zalihama
           
            var nadjeniProizvodSastojak=await Context.ProizvodSastojak.Include(p=>p.Sastojak).Where(p=>p.Proizvod.ID==idProizvoda).ToListAsync();

            List<ProizvodSastojak> listaZaVracanje = new List<ProizvodSastojak>();///obrati paznju na definisanje lista

            foreach(ProizvodSastojak element in nadjeniProizvodSastojak){
                var nadjenaKolicinaSastojkaUFrizideru=await Context.ProdavnicaSastojak.Include(p=>p.Sastojak).Where(p=>p.Prodavnica.ID==idProdavnice && p.Sastojak.ID==element.Sastojak.ID).FirstAsync();
                               
                if (izabranaKolicina*element.Kolicina > nadjenaKolicinaSastojkaUFrizideru.Kolicina)
                 listaZaVracanje.Add(element);//return new JsonResult(nadjeniProizvodSastojak);

                 else nadjenaKolicinaSastojkaUFrizideru.Kolicina-=izabranaKolicina*element.Kolicina;
                 Context.ProdavnicaSastojak.Update(nadjenaKolicinaSastojkaUFrizideru);
                 await Context.SaveChangesAsync();

            }
            if (listaZaVracanje!=null){
            return Ok(listaZaVracanje);}
            else return BadRequest("Nije bilo kupovine");

        }

        [Route("MenadzerPorucuje/{idProdavnice}/{stringIDevaSastojka}")]
        [HttpPut]
        public async Task<ActionResult> MenadzerPorucuje(int idProdavnice,string stringIDevaSastojka){

          foreach(char elem in stringIDevaSastojka){

            if (elem!='a') {
            int idSastojka = elem-'0';////////////konvertovanje chara u int
            var nadjeniProdavnicaSastojak=await Context.ProdavnicaSastojak.Where(p=>p.Prodavnica.ID==idProdavnice && p.Sastojak.ID==idSastojka).FirstAsync();
            nadjeniProdavnicaSastojak.Kolicina+=300;
            Context.ProdavnicaSastojak.Update(nadjeniProdavnicaSastojak);
            await Context.SaveChangesAsync();
            }
          }

            return Ok("azurirana vrednost u bazi ");
        }
    }
}
/*
[
  {
    "id": 1,
    "kolicina": 100,
    "sastojak": {
      "id": 1,
      "naziv": "sunka"
    },
    "proizvod": null
  },
  {
    "id": 2,
    "kolicina": 100,
    "sastojak": {
      "id": 1,
      "naziv": "sunka"
    },
    "proizvod": null
  },
  {
    "id": 3,
    "kolicina": 100,
    "sastojak": {
      "id": 1,
      "naziv": "sunka"
    },
    "proizvod": null
  },
  {
    "id": 14,
    "kolicina": 100,
    "sastojak": {
      "id": 2,
      "naziv": "salata"
    },
    "proizvod": null
  },
  {
    "id": 15,
    "kolicina": 100,
    "sastojak": {
      "id": 3,
      "naziv": "krastavac"
    },
    "proizvod": null
  },
  {
    "id": 16,
    "kolicina": 30,
    "sastojak": {
      "id": 3,
      "naziv": "krastavac"
    },
    "proizvod": null
  },
  {
    "id": 17,
    "kolicina": 30,
    "sastojak": {
      "id": 3,
      "naziv": "krastavac"
    },
    "proizvod": null
  },
  {
    "id": 18,
    "kolicina": 30,
    "sastojak": {
      "id": 3,
      "naziv": "krastavac"
    },
    "proizvod": null
  }
]*/