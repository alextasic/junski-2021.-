using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Template.Models;
using Microsoft.EntityFrameworkCore;

namespace Template
{
    public class IspitDbContext : DbContext
    {
        public DbSet <Prodavnica> Prodavnica{get;set;}
        public DbSet <Proizvod> Proizvod{get;set;}
        public DbSet <Sastojak> Sastojak{get;set;}
        public DbSet <ProdavnicaSastojak> ProdavnicaSastojak {get;set;}
        public DbSet <ProizvodSastojak> ProizvodSastojak {get;set;}

        public IspitDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
