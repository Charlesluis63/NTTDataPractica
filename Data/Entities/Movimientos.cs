using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Movimientos
    {
        [Key]
        public int IdMovimientos { get; set; }
        public DateTime Fecha { get; set; }
        public string TipoMovimiento { get; set; }
        public decimal Valor { get; set; }

        public decimal Saldo { get; set; }

        public decimal SaldoInicial { get; set; }
        public  Cuenta Cuenta { get; set; }

    }
}
