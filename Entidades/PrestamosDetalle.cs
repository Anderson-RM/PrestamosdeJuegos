using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Text;

namespace PrestamosdeJuegos.Entidades
{
    public class PrestamosDetalle
    {
        [Key]
        public int PrestamoDetalleId { get; set; }
        public int PrestamosId { get; set; }
        public int JuegoId { get; set; }
        public double CantidadJuegos { get; set; }

        [ForeignKey("JuegoId")]
        public virtual Juegos FK_Juegos { get; set; }
    }
}
