using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PrestamosdeJuegos.Entidades
{
    public class EntradasJuegos
    {
        [Key]
        public int EntradaJuegoId { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public int JuegoId { get; set; }
        public double Cantidad { get; set; }

        [ForeignKey("JuegoId")]
        public Juegos FK_Juegos { get; set; }

    }
}
