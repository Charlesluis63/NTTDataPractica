namespace NTTChallenge.Exceptions.MovimientosException
{
    public class MovimientosException : System.Exception
    {
        public MovimientosException() { }

        public MovimientosException(string message) : base(message)
        {

        }

        public class ClienteSinSaldoMovimientoException : MovimientosException
        {
            public ClienteSinSaldoMovimientoException(string message = "Saldo no disponible") : base(message)
            {

            }
        }

    }




}
