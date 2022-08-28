using System;
using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{
    public class Persona
    {
        [Key]
        public int IdPersona { get; set; }
        public string Nombres { get; set; }
        public string Genero { get; set; }
        public int Edad { get; set; }
        public string Identificacion { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
    }
}
