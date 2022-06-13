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
        public async Task<JsonResult> Poruci(int idProdavnice,int idProizvoda,int izabranaKolicina){


            var nadjeniNedostajuciSastojci=
            await Context.Sastojak.Include(p=>p.ProizvodSastojak).Where(p=>p.ProizvodSastojak.Any(q=>q.Proizvod.ID==idProizvoda))
                                                                .Include(p=>p.ProdavnicaSastojak).Where(p=>p.ProdavnicaSastojak.Any((t=>t.Prodavnica.ID==idProdavnice && t.Kolicina<izabranaKolicina))).ToListAsync();

///vraca proizvode kojih nema dovoljno na zalihama
           
           /* var nadjeniProizvodSastojak=await Context.ProizvodSastojak.Include(p=>p.Sastojak).Where(p=>p.Proizvod.ID==idProizvoda).ToListAsync();

            List<string> listaZaVracanje= new "";

            foreach(ProizvodSastojak element in nadjeniProizvodSastojak){
                var nadjenaKolicinaSastojkaUFrizideru=await Context.ProdavnicaSastojak.Include(p=>p.Sastojak).Where(p=>p.Prodavnica.ID==idProdavnice && p.Sastojak.ID==element.Sastojak.ID).FirstAsync();
                               
                if (izabranaKolicina*element.Kolicina > nadjenaKolicinaSastojkaUFrizideru.Kolicina)
                 listaZaVracanje.Add(nadjenaKolicinaSastojkaUFrizideru.Sastojak.Naziv);//return new JsonResult(nadjeniProizvodSastojak);
            }
*/

         return new JsonResult(nadjeniNedostajuciSastojci);
        }
    }
}
