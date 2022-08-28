using Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO.Movimientos
{
    public class GetMovimientoDTO
    {
       
        public DateTime Fecha { get; set; }
        public string TipoMovimiento { get; set; }
        public decimal Valor { get; set; }

        public decimal Saldo { get; set; }


        public string NumeroCuenta { get; set; }
        public string Cliente { get; set; }
    }
}
