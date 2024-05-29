using AutoMapper;
using MinimalAPI.DTOs;
using MinimalAPI.Entidades;

namespace MinimalAPI.Utilidades
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles() 
        {
            CreateMap<CrearProspectosDTO, OCRD_SalesForce>();
            CreateMap<OCRD_SalesForce, ProspectoDTO>();   
            
        }
    }
}
