namespace NTTChallenge.Exceptions.CuentasException
{
    public class CuentasException:System.Exception
    {
        public CuentasException()  { }

        public CuentasException(string message) : base(message)
        {

        }
        

    }

    public  class SinUsuarioAsociarException : CuentasException
    {
        public SinUsuarioAsociarException(string message = "No es posible asignar la cuenta a un usuario inexistente") : base(message)
        {

        }
    }

    public class CuentaYaHaSidoCreadaException : CuentasException
    {
        public CuentaYaHaSidoCreadaException(string message = "Ya existe una cuenta con este numero") : base(message)
        {

        }
    }

    public class CuentaInexistenteException : CuentasException
    {
        public CuentaInexistenteException(string message = "La cuenta no existe o ya ha sido eliminada") : base(message)
        {

        }
    }


    public class CuentaNoPerteneceUsuarioException : CuentasException
    {
        public CuentaNoPerteneceUsuarioException(string message = "La cuenta no pertenece al usuario indicado") : base(message)
        {

        }
    }

}
