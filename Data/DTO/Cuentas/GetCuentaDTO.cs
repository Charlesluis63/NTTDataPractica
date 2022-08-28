using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO.Cuentas
{
    public class GetCuentaDTO
    {
        public string NumeroCuenta { get; set; }
        public string TipoCuenta { get; set; }
        public decimal SaldoInicial { get; set; }
        public decimal SaldoActual { get; set; }

        public bool Estado { get; set; }
        public string Cliente { get; set; }
    }
}
