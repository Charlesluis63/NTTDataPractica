using Data.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NTTChallenge.Controllers;
using NTTChallenge.Exceptions.MovimientosException;
using static NTTChallenge.Exceptions.MovimientosException.MovimientosException;

namespace TestingNTTBank
{
    [TestClass]
    public class MovimientosControllerTest
    {
        [TestMethod]
        public void DepositoDebeIncrementarSaldo()
        {
            //Preparation
            Movimientos nuevoMovimiento = new Movimientos
            {
                Valor = 200,
            };

            Cuenta cuentaDelMovimiento = new Cuenta
            {
                SaldoActual = 2000,
            };


            //Testing

             MovimientosController.DepositarDinero(nuevoMovimiento,cuentaDelMovimiento);

            //Verification
            Assert.AreEqual(nuevoMovimiento.Saldo, 2200);
        }

        [TestMethod]
        public void RetiroDebeDisminuirSaldo()
        {
            //Preparation
            Movimientos nuevoMovimiento = new Movimientos
            {
                Valor = 200,
            };

            Cuenta cuentaDelMovimiento = new Cuenta
            {
                SaldoActual = 2000,
            };


            //Testing

            MovimientosController.RetirarDinero(nuevoMovimiento, cuentaDelMovimiento);

            //Verification
            Assert.AreEqual(nuevoMovimiento.Saldo, 1800);
        }

        [TestMethod]
        public void RetirarDineroSinSaldoArrojaExcepcion()
        {
            ClienteSinSaldoMovimientoException excepcionEsperada = null;

            //Preparation
            Movimientos nuevoMovimiento = new Movimientos
            {
                Valor = 200,
            };

            Cuenta cuentaDelMovimiento = new Cuenta
            {
                SaldoActual = 0,
            };


            //Testing
            try
            {
                MovimientosController.RetirarDinero(nuevoMovimiento, cuentaDelMovimiento);

            }
            catch (ClienteSinSaldoMovimientoException ex)
            {

                excepcionEsperada = ex;
            }

            
            if(excepcionEsperada == null)
            {
                Assert.Fail("Se esperaba excepcion usuario sin saldo disponible");
            }
            
        }
    }
}
