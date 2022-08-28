using Data.DatabaseContexts;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class PersonaRepository : NTTRepositoryBase<Persona>,IPersonaRepository
    {
        public PersonaRepository(NTTContext repositoryContext) : base(repositoryContext)
        {
        }
    }


    public class ClienteRepository : NTTRepositoryBase<Cliente>, IClienteRepository
    {
        public ClienteRepository(NTTContext repositoryContext) : base(repositoryContext)
        {
        }
    }

    public class CuentaRepository : NTTRepositoryBase<Cuenta>, ICuentaRepository
    {
        public CuentaRepository(NTTContext repositoryContext) : base(repositoryContext)
        {
        }
    }

    public class MovimientoRepository : NTTRepositoryBase<Movimientos>, IMovimientosRepository
    {
        public MovimientoRepository(NTTContext repositoryContext) : base(repositoryContext)
        {
        }
    }
    


}
