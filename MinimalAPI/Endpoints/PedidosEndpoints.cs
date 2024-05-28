using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OutputCaching;
using MinimalAPI.Repositorios;
using MinimalAPI.Entidades;

namespace MinimalAPI.Endpoints
{
    public static class PedidosEndpoints
    {
        public static RouteGroupBuilder MapPedidos(this RouteGroupBuilder group)
        {
            group.MapGet("/{cardcode}", ObtenerPedidoPorCardCode);         
            group.MapPost("/", CrearPedido);
            group.MapPut("/{cardcode}", ActualizarPedido);

            //endpointsPacientes.MapDelete("/{id:int}", BorrarPaciente);
            //endpointsPacientes.MapGet("/{id:int}", ObtenerPacientePorId);

            return group;

        }
        static async Task<Results<Ok<Pedidos>, NotFound>> ObtenerPedidoPorCardCode(IRepositorioPedidos repositorio, string cardcode)
        {
            var pedidos = await repositorio.ObtenerPedidoPorCardCode(cardcode);

            if (pedidos == null)
            {
                return TypedResults.NotFound();
            }
            return TypedResults.Ok(pedidos);
        }

        static async Task<Created<Pedidos>> CrearPedido(Pedidos pedidos, IRepositorioPedidos repositorio, IOutputCacheStore outputCacheStore)
        {
            var id = await repositorio.Crear(pedidos);
            await outputCacheStore.EvictByTagAsync("pedidos-get", default);
            return TypedResults.Created($"/pedidos/{id}", pedidos);
        }

        static async Task<Results<NoContent, NotFound>> ActualizarPedido(string cardcode, Pedidos pedidos, IRepositorioPedidos repositorio, IOutputCacheStore outputCacheStore)
        {
            var existe = await repositorio.Existe(cardcode);
            if (!existe)
            {
                return TypedResults.NotFound();
            }

            await repositorio.Actualizar(pedidos);
            await outputCacheStore.EvictByTagAsync("pedidos-get", default);
            return TypedResults.NoContent();
        }

        //static async Task<Results<NoContent, NotFound>> BorrarPaciente (int id, IRepositorioGenero repositorio, IOutputCacheStore outputCacheStore)
        //{

        //    var existe = await repositorio.Existe(id);
        //    if (!existe)
        //    {
        //        return TypedResults.NotFound();
        //    }

        //    await repositorio.Borrar(id);
        //    await outputCacheStore.EvictByTagAsync("pacientes-get", default);
        //    return TypedResults.NoContent();
        //}

        //Con esta estructura se espera mostrar al cliente la estructura de los Endpoint
        //static async Task<Ok<List<Paciente>>> ObtenerPacientes (IRepositorioGenero repositorio)
        //{
        //    var pacientes = await repositorio.ObtenerTodos();
        //    return TypedResults.Ok(pacientes);
        //}

        //static async Task<Results<Ok<Paciente>, NotFound>> ObtenerPacientePorId(IRepositorioGenero repositorio, int id) 
        //{
        //    var paciente = await repositorio.ObtenerPorId(id);

        //    if (paciente == null)
        //    {
        //        return TypedResults.NotFound();
        //    }
        //    return TypedResults.Ok(paciente);
        //}
    }
}
