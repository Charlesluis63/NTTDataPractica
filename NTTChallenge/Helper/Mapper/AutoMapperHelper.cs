using AutoMapper;
using Data.DTO.Cliente;
using Data.DTO.Cuentas;
using Data.DTO.Movimientos;
using Data.Entities;
using System;

namespace NTTChallenge.Helper.Mapper
{
    public class AutoMapperHelper:Profile
    {
        public AutoMapperHelper()
        {
            CreateMap<PostCuentaDTO, Cuenta>()
                .ReverseMap();

            CreateMap<GetCuentaDTO, Cuenta>()
               .ReverseMap();

            CreateMap<PostClienteDTO, Cliente>()
                .ReverseMap();

            CreateMap<GetClienteDTO, Cliente>()
                .ForMember(c => c.Nombres , e => e.MapFrom(mf => mf.Nombre))
                .ReverseMap();

            CreateMap<PostMovimientoDTO, Movimientos>()
                .ForMember(c => c.Fecha, m => m.MapFrom(e => DateTime.Now))
                .ReverseMap();

            CreateMap<Movimientos, GetMovimientoDTO>()
                .ForMember(c => c.NumeroCuenta, m => m.MapFrom(e => e.Cuenta.NumeroCuenta))
                .ForMember(c => c.Cliente, m => m.MapFrom(e => e.Cuenta.ClientePersonaFK.Nombres))
                .ReverseMap();

            CreateMap<Movimientos, GetReporteMovimientoDTO>()
               .ForMember(c => c.NumeroCuenta, m => m.MapFrom(e => e.Cuenta.NumeroCuenta))
               .ForMember(c => c.Cliente, m => m.MapFrom(e => e.Cuenta.ClientePersonaFK.Nombres))
               .ForMember(c => c.Tipo, m => m.MapFrom(e => e.Cuenta.TipoCuenta))
               .ForMember(c => c.SaldoInicial, m => m.MapFrom(e => e.SaldoInicial))
               .ForMember(c => c.Estado, m => m.MapFrom(e => e.Cuenta.Estado))
               .ForMember(c => c.Movimiento, m => m.MapFrom(e => e.Valor))
               .ForMember(c => c.SaldoDisponible, m => m.MapFrom(e => e.Saldo))

               .ReverseMap();


        }
    }
}
