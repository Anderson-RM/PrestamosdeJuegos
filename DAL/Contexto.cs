using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.EntityFrameworkCore;
using PrestamosdeJuegos.Entidades;

namespace PrestamosdeJuegos.DAL
{
    public class Contexto : DbContext
    {
        public DbSet<Juegos> Juegos { get; set; }
        public DbSet<Amigos> Amigos { get; set; }
        public DbSet<Prestamos> Prestamos { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source = Data\PrestamosdeJuegos.db");
        }
    }
}