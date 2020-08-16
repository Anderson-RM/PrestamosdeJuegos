using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PrestamosdeJuegos.Entidades
{
    public class Amigos
    {
        [Key]
        public int AmigoId { get; set; } 
        public String NombreCompleto { get; set; } 
        public String Direccion { get; set; } 
        public long Telefono { get; set; }
        public long Celular { get; set; }
        public String Email { get; set; }
        public DateTime FechaNacimiento { get; set; } = DateTime.Now;        
        
    }
}
