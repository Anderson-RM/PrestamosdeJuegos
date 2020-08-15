using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrestamosdeJuegos.Entidades
{
    public class Juegos
    {
        [Key]
        public int JuegoId { get; set; }
        public DateTime FechaCompra { get; set; } = DateTime.Now;
        public string Descripcion { get; set; }
        public double Precio { get; set; }
        public double Existencia { get; set; }
    }
}