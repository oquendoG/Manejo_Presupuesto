using AutoMapper;
using ManjeoPresupuesto.Models;

namespace ManjeoPresupuesto.Services
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Cuenta, CuentaCreacionViewModel>();

            //El reverse map permite el mapeo en ambas direcciones
            CreateMap<TransaccionActualizacionViewModel, Transaccion>().ReverseMap();
        }
    }
}
