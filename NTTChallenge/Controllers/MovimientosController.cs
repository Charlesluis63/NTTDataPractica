using AutoMapper;
using Data.DatabaseContexts;
using Data.DTO.Movimientos;
using Data.Entities;
using Data.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NTTChallenge.Exceptions.ClientesException;
using NTTChallenge.Exceptions.CuentasException;
using NTTChallenge.Exceptions.MovimientosException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static NTTChallenge.Exceptions.MovimientosException.MovimientosException;

namespace NTTChallenge.Controllers
{
    [ApiController]
    [Route("movimientos")]
    public class MovimientosController:ControllerBase
    {
        private IGeneralRepository _repository;
        private IMapper _mapper;
        public MovimientosController(IGeneralRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> CrearMovimiento([FromBody] PostMovimientoDTO datosNuevosMovimientosDTO)
        {
            NTTContext contexto = _repository.getContext();
            await contexto.Database.BeginTransactionAsync();

            try
            {
                Cuenta cuentaDelMovimiento = _repository.FindByCondition<Cuenta>(c => c.NumeroCuenta == datosNuevosMovimientosDTO.IdCuenta).Include(c => c.ClientePersonaFK).AsNoTracking().SingleOrDefault();
                
                
                if (cuentaDelMovimiento == null)
                {
                    throw new CuentaInexistenteException();
                }

                //if(cuentaDelMovimiento.ClientePersonaFK.ClienteId != clienteDelMovimiento.ClienteId)
                //{
                //    throw new CuentaNoPerteneceUsuarioException();
                //}


                Movimientos nuevoMovimiento = _mapper.Map<Movimientos>(datosNuevosMovimientosDTO);
                nuevoMovimiento.SaldoInicial = cuentaDelMovimiento.SaldoActual;

                if (nuevoMovimiento.TipoMovimiento.Equals("Deposito"))
                {
                     DepositarDinero(nuevoMovimiento, cuentaDelMovimiento);
                }
                else if (nuevoMovimiento.TipoMovimiento.Equals("Retiro"))
                {
                    RetirarDinero(nuevoMovimiento, cuentaDelMovimiento);
                }
                


                _repository.Create<Movimientos>(nuevoMovimiento);
                contexto.Cuenta.Attach(cuentaDelMovimiento);
                nuevoMovimiento.Cuenta = cuentaDelMovimiento;

                _repository.Save();
                


                cuentaDelMovimiento.SaldoActual = nuevoMovimiento.Saldo;
                _repository.Update(cuentaDelMovimiento);
                _repository.Save();

                await contexto.Database.CommitTransactionAsync();







                return Ok(nuevoMovimiento);
                



            }
            catch(ClientesException ex)
            {
                await contexto.Database.RollbackTransactionAsync();
                return new ConflictObjectResult(ex.Message);
            }
            catch (CuentasException ex)
            {
                await contexto.Database.RollbackTransactionAsync();

                return new ConflictObjectResult(ex.Message);
            }
            catch (MovimientosException ex)
            {
                await contexto.Database.RollbackTransactionAsync();

                return new ConflictObjectResult(ex.Message);
            }
            catch (System.Exception)
            {
                await contexto.Database.RollbackTransactionAsync();

                throw;
            }
        }

        public static void  RetirarDinero(Movimientos nuevoMovimiento, Cuenta cuentaDelMovimiento)
        {
            decimal nuevoSaldo = cuentaDelMovimiento.SaldoActual - nuevoMovimiento.Valor;
            if (nuevoSaldo < 0 || cuentaDelMovimiento.SaldoActual == 0)
            {
                throw new ClienteSinSaldoMovimientoException();
            }
            nuevoMovimiento.Saldo = nuevoSaldo;
            nuevoMovimiento.Valor = -1 * nuevoMovimiento.Valor;

        }

        public static void  DepositarDinero(Movimientos nuevoMovimiento, Cuenta cuentaDelMovimiento)
        {
            nuevoMovimiento.Saldo = cuentaDelMovimiento.SaldoActual + nuevoMovimiento.Valor;


        }





        [HttpGet]
        [Route("{idNumeroCuenta}")]
        public ActionResult<GetMovimientoDTO> ObtenerMovimientoByNumeroCuenta([FromRoute] string idNumeroCuenta)
        {
            try
            {
                Movimientos movimiento = _repository.FindByCondition<Movimientos>(mov => mov.Cuenta.NumeroCuenta == idNumeroCuenta)
                                        .Include(c => c.Cuenta)
                                        .Include(c=> c.Cuenta.ClientePersonaFK)
                                        .FirstOrDefault();

                GetMovimientoDTO movimientomapeado = _mapper.Map<GetMovimientoDTO>(movimiento);
                return movimientomapeado;
            }
            catch (System.Exception)
            {

                throw;
            }
        }


        [HttpGet]
        [Route("reportes")]
        [Produces("application/json")]
        public ActionResult<GetReporteMovimientoDTO> ObtenerReporte([FromQuery] int idCliente, [FromQuery] string rangoFecha)
        {
            try
            {
                string[] fechas = rangoFecha.Split("-");
                DateTime fechaInicio = Convert.ToDateTime(fechas[0]);
                DateTime fechaFin = Convert.ToDateTime(fechas[1]);

                IEnumerable<Movimientos> movimientos = _repository
                    .FindByCondition<Movimientos>(mov => mov.Cuenta.ClientePersonaFK.ClienteId == idCliente && mov.Fecha >= fechaInicio && mov.Fecha <= fechaFin)
                    .Include(m => m.Cuenta)
                    .Include(m => m.Cuenta.ClientePersonaFK)
                    .OrderByDescending(m => m.Fecha)
                    ;

                List<GetReporteMovimientoDTO> reporte = _mapper.Map<List<GetReporteMovimientoDTO>>(movimientos.ToList());

                return new JsonResult(reporte);                                                                    

            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}
