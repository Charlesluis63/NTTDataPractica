using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO.Cliente
{
    public class GetClienteDTO
    {
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Contrasenia { get; set; }
        
        public bool Estado { get; set; }
        public int ClienteId { get; set; }


    }
}
