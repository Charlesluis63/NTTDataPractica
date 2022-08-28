namespace NTTChallenge.Exceptions.ClientesException
{
    public class ClientesException:System.Exception
    {
        public ClientesException()  { }

        public ClientesException(string message) : base(message)
        {

        }
        

    }


    public class ClienteInexistenteException : ClientesException
    {
        public ClienteInexistenteException(string message = "El cliente no existe o ha sido eliminado") : base(message)
        {

        }
    }

}
