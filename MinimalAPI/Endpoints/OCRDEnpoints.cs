using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OutputCaching;
using MinimalAPI.DTOs;
using MinimalAPI.Repositorios;
using MinimalAPI.Entidades;
using System.Runtime.CompilerServices;
using AutoMapper;
namespace MinimalAPI.Endpoints
{
    public static class OCRDEnpoints
    {
        public static RouteGroupBuilder MapOcrd(this RouteGroupBuilder group)
        {           

            group.MapGet("/{cardcode}", ObtenerProspectoPorCardCode);     
            group.MapPost("/", CrearProspecto);
            group.MapPut("/{cardcode}", ActualizarProspecto);

            //endpointsprospectos_ocrd.MapDelete("/{id:int}", BorrarProspecto);
            //endpointsprospectos_ocrd.MapGet("/{id:int}", ObtenerProspectoPorId);
            //endpointsprospectos_ocrd.MapGet("/", ObtenerProspecto).CacheOutput(c => c.Expire(TimeSpan.FromSeconds(60)).Tag("prospectos-get"));
            //endpointsPacientes.MapGet("/", ObtenerPacientes).CacheOutput(c => c.Expire(TimeSpan.FromSeconds(60)).Tag("pacientes-get"));
            return group;
        }
        static async Task<Created<ProspectoDTO>> CrearProspecto(CrearProspectosDTO crearProspectosDTO,
            IRepositorioOCRD repositorio,
            IOutputCacheStore outputCacheStore,IMapper mapper)
        {
            
            var prospectos_ocrd = mapper.Map<OCRD_SalesForce>(crearProspectosDTO);

            var id = await repositorio.Crear(prospectos_ocrd);
            await outputCacheStore.EvictByTagAsync("prospectos-get", default);

            var prospectoDTO = mapper.Map<ProspectoDTO>(prospectos_ocrd);

            return TypedResults.Created($"/prospectos/{id}", prospectoDTO);
        }

        static async Task<Results<Ok<ProspectoDTO>, NotFound>> ObtenerProspectoPorCardCode(IRepositorioOCRD repositorio,IMapper mapper, string cardcode)        {
            
            var prospectos_ocrd = await repositorio.ObtenerPorCardCode(cardcode);           

            if (prospectos_ocrd == null)
            {
                return TypedResults.NotFound();
            }   
                        
            var prospectoDTO = mapper.Map<ProspectoDTO>(prospectos_ocrd);
            return TypedResults.Ok(prospectoDTO);
        }        


        static async Task<Results<NoContent, NotFound>> ActualizarProspecto(string cardcode, int id, CrearProspectosDTO crearProspectosDTO, 
            IRepositorioOCRD repositorio, 
            IOutputCacheStore outputCacheStore,
            IMapper mapper)
        {
            
            //var existe = await repositorio.Existe(cardcode);
            var existe = await repositorio.Existe2(id);
            if (!existe)
            {
                return TypedResults.NotFound();
            }

            var prospectos_ocrd = mapper.Map<OCRD_SalesForce>(crearProspectosDTO);
            prospectos_ocrd.CardCode = cardcode;
            prospectos_ocrd.Id = id;

            await repositorio.Actualizar(prospectos_ocrd);
            await outputCacheStore.EvictByTagAsync("prospectos-get", default);
            return TypedResults.NoContent();
        }

        //static async Task<Results<NoContent, NotFound>> BorrarProspecto(int id, IRepositorioOCRD repositorio, IOutputCacheStore outputCacheStore)
        //{

        //    var existe = await repositorio.Existe(id);
        //    if (!existe)
        //    {
        //        return TypedResults.NotFound();
        //    }

        //    await repositorio.Borrar(id);
        //    await outputCacheStore.EvictByTagAsync("prospectos-get", default);
        //    return TypedResults.NoContent();
        //}

        //static async Task<Results<Ok<MinimalAPI.Entidades.OCRD_SalesForce>, NotFound>> ObtenerProspectoPorId(IRepositorioOCRD repositorio, int id)
        //{
        //    var prospectos_ocrd = await repositorio.ObtenerPorId(id);

        //    if (prospectos_ocrd == null)
        //    {
        //        return TypedResults.NotFound();
        //    }
        //    return TypedResults.Ok(prospectos_ocrd);
        //}

        //Con esta estructura se espera mostrar al prospecto
        //static async Task<Ok<List<MinimalAPI.Entidades.OCRD_SalesForce>>> ObtenerProspecto(IRepositorioOCRD repositorio)
        //{
        //    var prospectos_ocrd = await repositorio.ObtenerTodos();
        //    return TypedResults.Ok(prospectos_ocrd);
        //}


    }
}
