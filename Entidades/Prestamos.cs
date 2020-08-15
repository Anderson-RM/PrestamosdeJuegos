using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PrestamosdeJuegos.Entidades
{
    public class Prestamos
    {
        [Key]
        public int PrestamoId { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public int AmigoId { get; set; }
        public string Observacion { get; set; }
        public int CantidadJuegosTotal { get; set; }

        [ForeignKey("PrestamoId")]
        public virtual List<PrestamosDetalle> Detalle { get; set; } = new List<PrestamosDetalle>();
    }
}
