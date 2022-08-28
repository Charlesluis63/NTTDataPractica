using AutoMapper;
using Data.DTO.Cliente;
using Data.Entities;
using Data.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace NTTChallenge.Controllers
{
    [ApiController]
    [Route("clientes")]
    public class ClientesController
    {
        private IGeneralRepository _repository;
        private IMapper _mapper;
        public ClientesController(IGeneralRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<List<GetClienteDTO>> GetAllClientes(){
            try
            {
                List<Cliente> clientes = _repository.FindAll<Cliente>().ToList();
                return _mapper.Map<List<GetClienteDTO>>(clientes);
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        [HttpGet]
        [Route("{idCliente:int}",Name ="getClienteEspecifico")]
        public  ActionResult<GetClienteDTO> GetPersonaById([FromRoute] int idCliente)
        {
            try
            {
                Cliente clienteDeseado =  _repository.FindByCondition<Cliente>(p => p.ClienteId == idCliente).FirstOrDefault();
                GetClienteDTO clienteMapeado = _mapper.Map<GetClienteDTO>(clienteDeseado);
                return clienteMapeado;
            }
            catch (System.Exception)
            {

                throw;
            }
        }


        [HttpPost]
        public ActionResult<GetClienteDTO> PostCliente([FromBody] PostClienteDTO datosCliente)
        {
            try
            {
                Cliente nuevoCliente = _mapper.Map<Cliente>(datosCliente);
                _repository.Create<Cliente>(nuevoCliente);
                _repository.Save();
                nuevoCliente.ClienteId = nuevoCliente.IdPersona;
                _repository.Save();
                return new CreatedAtRouteResult("getClienteEspecifico",  new { idCliente = nuevoCliente.ClienteId }, nuevoCliente);
            }
            catch(DbUpdateException dbex)
            {
                return new ConflictObjectResult(dbex.InnerException.Message);
            }
            catch (System.Exception)
            {

                throw;
            }
        }


        [HttpDelete]
        [Route("{idCliente:int}")]
        public ActionResult<GetClienteDTO> DeleteCliente([FromRoute] int idCliente)
        {
            try
            {
                Cliente clienteEliminado = _repository.FindByCondition<Cliente>(c => c.ClienteId == idCliente).FirstOrDefault();
                GetClienteDTO clienteMapeado = _mapper.Map<GetClienteDTO>(clienteEliminado);
                _repository.Delete<Cliente>(clienteEliminado);
                _repository.Save();
                return clienteMapeado;

            }
            catch (System.Exception)
            {

                return new ConflictObjectResult("No ha sido posible realizar la operacion");
            }
        }


        [HttpPut]
        [Route("{idCliente:int}")]

        public ActionResult<GetClienteDTO> UpdateCliente([FromRoute] int idCliente,[FromBody] PostClienteDTO datosCliente)
        {
            try
            {
                Cliente clienteActualizado = _mapper.Map<Cliente>(datosCliente);
                clienteActualizado.IdPersona = idCliente;
                clienteActualizado.ClienteId = idCliente;
                _repository.Update<Cliente>(clienteActualizado);
                _repository.Save();
                return _mapper.Map<GetClienteDTO>(clienteActualizado);

            }
            catch (System.Exception)
            {

                throw;
            }
        }


    }
}
