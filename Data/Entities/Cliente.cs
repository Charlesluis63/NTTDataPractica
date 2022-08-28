using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Cliente:Persona
    {
        public int ClienteId { get; set; }
        public string Contrasenia { get; set; }
        public bool Estado { get; set; } = true;
    }
}
