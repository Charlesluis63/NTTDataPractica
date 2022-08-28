using AutoMapper;
using Data.DatabaseContexts;
using Data.DTO.Cliente;
using Data.DTO.Cuentas;
using Data.Entities;
using Data.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NTTChallenge.Exceptions.ClientesException;
using NTTChallenge.Exceptions.CuentasException;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NTTChallenge.Controllers
{
    [ApiController]
    [Route("cuentas")]
    public class CuentasController : ControllerBase
    {
        private IGeneralRepository _repository;
        private IMapper _mapper;
        public CuentasController(IGeneralRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        [HttpGet]
        [Route("{idCuenta}", Name = "getCuentaEspecifica")]
        public async Task<ActionResult<GetCuentaDTO>> ObtenerCuenta([FromRoute] string idCuenta)
        {
            try
            {
                NTTContext contexto = _repository.getContext();

                IQueryable<GetCuentaDTO> queryCuenta = GetQueryCuentaDTO(contexto, idCuenta);
                if (!queryCuenta.Any())
                {
                    throw new System.Exception("No existe informacion sobre ese numero de Cuenta");
                }

                return await queryCuenta.SingleOrDefaultAsync();

            }
            catch (System.Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public IActionResult CrearCuenta([FromBody] PostCuentaDTO datosNuevaCuenta)
        {
            try
            {

                
                Cliente clienteDeCuenta = _repository.FindByCondition<Cliente>(c => c.ClienteId == datosNuevaCuenta.IdUsuario).FirstOrDefault();
                if (clienteDeCuenta == null)
                {
                    throw new SinUsuarioAsociarException();
                }

                

                Cuenta nuevaCuenta = _mapper.Map<Cuenta>(datosNuevaCuenta);
                _repository.Create<Cuenta>(nuevaCuenta);
                nuevaCuenta.ClientePersonaFK = clienteDeCuenta;
                nuevaCuenta.SaldoActual = nuevaCuenta.SaldoInicial;
                _repository.Save();
                return new CreatedAtRouteResult("getCuentaEspecifica", new { idCuenta = nuevaCuenta.NumeroCuenta }, nuevaCuenta);
            }
            catch (ClientesException ex)
            {
                return new ConflictObjectResult(ex.Message);
            }
            
            catch (System.Exception)
            {

                throw;
            }
        }


        [HttpDelete]
        [Route("{idCuenta}")]

        public  async Task<ActionResult<GetCuentaDTO>> DeleteCuenta([FromRoute] string idCuenta)
        {
            NTTContext contexto = _repository.getContext();

            try
            {
                Cuenta cuentaEliminada = _repository.FindByCondition<Cuenta>(c => c.NumeroCuenta == idCuenta).FirstOrDefault();
                GetCuentaDTO cuentaMapeada = GetQueryCuentaDTO(contexto,cuentaEliminada.NumeroCuenta).FirstOrDefault();
                
                if (cuentaEliminada == null)
                {
                    throw new CuentaInexistenteException();
                }

                await contexto.Database.BeginTransactionAsync();
                _repository.DeleteByCondition<Movimientos>(m => m.Cuenta.NumeroCuenta == idCuenta);
                _repository.Save();

                _repository.Delete<Cuenta>(cuentaEliminada);
                _repository.Save();

                await contexto.Database.CommitTransactionAsync();
                return cuentaMapeada;

            }
            catch (ClientesException ex)
            {
                await contexto.Database.RollbackTransactionAsync();
                return new ConflictObjectResult(ex.Message);
            }

            catch (System.Exception)
            {
                await contexto.Database.RollbackTransactionAsync();
                return new ConflictObjectResult("No ha sido posible realizar la operación");

            }
        }

        [HttpPut]
        [Route("desactivar/{idCuenta}")]

        public  ActionResult<GetCuentaDTO> DesactivarCuenta([FromRoute] string idCuenta)
        {
            NTTContext contexto = _repository.getContext();

            try
            {
                Cuenta cuentaDesactivada = _repository.FindByCondition<Cuenta>(c => c.NumeroCuenta == idCuenta).FirstOrDefault();

                if (cuentaDesactivada == null)
                {
                    throw new CuentaInexistenteException();
                }

                cuentaDesactivada.Estado = false;
                _repository.Update<Cuenta>(cuentaDesactivada);
                _repository.Save();
                GetCuentaDTO cuentaMapeada = GetQueryCuentaDTO(contexto, cuentaDesactivada.NumeroCuenta).FirstOrDefault();

                return cuentaMapeada;

            }
            catch (ClientesException ex)
            {
                return new ConflictObjectResult(ex.Message);
            }

            catch (System.Exception)
            {
                return new ConflictObjectResult("No ha sido posible realizar la operación");

            }
        }



        private IQueryable<GetCuentaDTO> GetQueryCuentaDTO(NTTContext contexto,string idCuenta)
        {
            IQueryable<GetCuentaDTO> query = (from Persona in contexto.Persona
                                     join Cuenta in contexto.Cuenta on Persona.IdPersona equals Cuenta.ClientePersonaFK.IdPersona
                                     where Cuenta.NumeroCuenta == idCuenta
                                     select new GetCuentaDTO
                                     {
                                         TipoCuenta = Cuenta.TipoCuenta,
                                         Cliente = Persona.Nombres,
                                         Estado = Cuenta.Estado,
                                         NumeroCuenta = Cuenta.NumeroCuenta,
                                         SaldoInicial = Cuenta.SaldoInicial,
                                         SaldoActual = Cuenta.SaldoActual
                                     });

            return query;

        }
    }
}
