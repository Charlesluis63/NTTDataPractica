using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    internal interface IRepositoryModels
    {
    }


    public interface IPersonaRepository: IRepositoryBase<Persona>
    {

    }

    public interface IClienteRepository : IRepositoryBase<Cliente>
    {

    }

    public interface ICuentaRepository : IRepositoryBase<Cuenta>
    {

    }
    public interface IMovimientosRepository : IRepositoryBase<Movimientos>
    {

    }
}
