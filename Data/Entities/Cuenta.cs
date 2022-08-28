using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Cuenta
    {
        [Key]
        public string NumeroCuenta { get; set; }
        public string TipoCuenta { get; set; }
        public decimal SaldoInicial { get; set; }
        public decimal SaldoActual { get; set; }
        public bool Estado { get; set; }

        public Cliente ClientePersonaFK { get; set; }
    }
}
