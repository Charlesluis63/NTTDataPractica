using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO.Movimientos
{
    public class PostMovimientoDTO
    {
        public string TipoMovimiento { get; set; }
        public decimal Valor { get; set; }
        public string IdCuenta { get; set; }
    }
}
