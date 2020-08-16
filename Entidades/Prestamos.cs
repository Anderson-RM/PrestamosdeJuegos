using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrestamosdeJuegos.Entidades
{
    public class Prestamos
    {
        [Key]
        public int PrestamoId { get; set; }
        public int AmigoId { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public string Observacion { get; set; }
        public double CantidadJuegosTotal { get; set; }

        [ForeignKey("PrestamoId")]
        public virtual List<PrestamosDetalle> Detalle { get; set; } = new List<PrestamosDetalle>();

        [ForeignKey("AmigoId")]
        public virtual Amigos FK_AmigoId { get; set; }
    }
}
