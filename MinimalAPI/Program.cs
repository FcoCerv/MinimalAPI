using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.EntityFrameworkCore;
using MinimalAPI;
using MinimalAPI.Entidades;
using MinimalAPI.Migrations;
using MinimalAPI.Repositorios;

var builder = WebApplication.CreateBuilder(args);
var origenesPermitidos = builder.Configuration.GetValue<string>("origenesPermitidos")!;

// Inicio de area de los servicios

builder.Services.AddDbContext<ApplicationDbContext>(opciones => 
    opciones.UseSqlServer("name=DefaultConnection", sqlOptions =>
    {
        sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(30),
            errorNumbersToAdd: null);
    }));

builder.Services.AddCors(opciones =>
 {
     opciones.AddDefaultPolicy(configuracion =>
    {
        configuracion.WithOrigins(origenesPermitidos).AllowAnyHeader().AllowAnyMethod();
    });
     opciones.AddPolicy("libre", configuracion =>
     {
         configuracion.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
     });
    
});

builder.Services.AddOutputCache();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IRepositorioGenero, RepositorioPacientes>();
builder.Services.AddScoped<IRepositorioOCRD, RepositorioOCRD>();


// Fin de area de los servicios

var app = builder.Build();

// Inicio de area de los middleware
//Esta condicion solo aplicaria si quisiera que solo lo visualicen en modo development
//if (builder.Environment.IsDevelopment()) { app.UseSwagger(); app.UseSwaggerUI(); }

app.UseSwagger();
app.UseSwaggerUI();
app.UseCors();
app.UseOutputCache();

//Aqui se crea un EndPoint post para enviar data
var endpointsprospectos_ocrd = app.MapGroup("/prospectos");
var endpointsPacientes = app.MapGroup("/pacientes");
//Esta expresion esta en formato Lambda
//app.MapGet("/", [EnableCors(policyName:"libre")]() => "Hola Mundo!");

//endpointsprospectos_ocrd.MapGet("/", ObtenerProspecto).CacheOutput(c => c.Expire(TimeSpan.FromSeconds(60)).Tag("prospectos-get"));

endpointsprospectos_ocrd.MapGet("/{cardcode}", ObtenerProspectoPorCardCode);

//endpointsprospectos_ocrd.MapGet("/{id:int}", ObtenerProspectoPorId);

endpointsprospectos_ocrd.MapPost("/", CrearProspecto);

endpointsprospectos_ocrd.MapPut("/{cardcode}", ActualizarProspecto);

//endpointsprospectos_ocrd.MapDelete("/{id:int}", BorrarProspecto);


//endpointsPacientes.MapGet("/", ObtenerPacientes).CacheOutput(c => c.Expire(TimeSpan.FromSeconds(60)).Tag("pacientes-get"));

//endpointsPacientes.MapGet("/{codigosn}", ObtenerPacientePorCodigoSN);

//endpointsPacientes.MapGet("/{id:int}", ObtenerPacientePorId);

//endpointsPacientes.MapPost("/", CrearPaciente);

//endpointsPacientes.MapPut("/{id:int}", ActualizarPaciente);

//endpointsPacientes.MapDelete("/{id:int}", BorrarPaciente);

// Fin de area de los middleware

app.Run();

//Con esta estructura se espera mostrar al cliente la estructura de los Endpoint
//static async Task<Ok<List<Paciente>>> ObtenerPacientes (IRepositorioGenero repositorio)
//{
//    var pacientes = await repositorio.ObtenerTodos();
//    return TypedResults.Ok(pacientes);
//}

//static async Task<Results<Ok<Paciente>, NotFound>> ObtenerPacientePorCodigoSN(IRepositorioGenero repositorio, string codigosn)
//{
//    var paciente = await repositorio.ObtenerPorCodigoSn(codigosn);

//    if (paciente == null)
//    {
//        return TypedResults.NotFound();
//    }
//    return TypedResults.Ok(paciente);
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

//static async Task<Created<Paciente>> CrearPaciente (Paciente paciente, IRepositorioGenero repositorio, IOutputCacheStore outputCacheStore)
//{
//    var id = await repositorio.Crear(paciente);
//    await outputCacheStore.EvictByTagAsync("pacientes-get", default);
//    return TypedResults.Created($"/pacientes/{id}", paciente);
//}

//static async Task<Results<NoContent, NotFound>> ActualizarPaciente (int id, Paciente paciente, IRepositorioGenero repositorio, IOutputCacheStore outputCacheStore)
//{
//    var existe = await repositorio.Existe(id);
//    if (!existe)
//    {
//        return TypedResults.NotFound();
//    }

//    await repositorio.Actualizar(paciente);
//    await outputCacheStore.EvictByTagAsync("pacientes-get", default);
//    return TypedResults.NoContent();
//}

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



//Con esta estructura se espera mostrar al prospecto
//static async Task<Ok<List<MinimalAPI.Entidades.OCRD_SalesForce>>> ObtenerProspecto(IRepositorioOCRD repositorio)
//{
//    var prospectos_ocrd = await repositorio.ObtenerTodos();
//    return TypedResults.Ok(prospectos_ocrd);
//}

static async Task<Results<Ok<MinimalAPI.Entidades.OCRD_SalesForce>, NotFound>> ObtenerProspectoPorCardCode(IRepositorioOCRD repositorio, string cardcode)
{
    var prospectos_ocrd = await repositorio.ObtenerPorCardCode(cardcode);

    if (prospectos_ocrd == null)
    {
        return TypedResults.NotFound();
    }
    return TypedResults.Ok(prospectos_ocrd);
}

//static async Task<Results<Ok<MinimalAPI.Entidades.OCRD_SalesForce>, NotFound>> ObtenerProspectoPorId(IRepositorioOCRD repositorio, int id)
//{
//    var prospectos_ocrd = await repositorio.ObtenerPorId(id);

//    if (prospectos_ocrd == null)
//    {
//        return TypedResults.NotFound();
//    }
//    return TypedResults.Ok(prospectos_ocrd);
//}

static async Task<Created<MinimalAPI.Entidades.OCRD_SalesForce>> CrearProspecto(MinimalAPI.Entidades.OCRD_SalesForce prospectos_ocrd, IRepositorioOCRD repositorio, IOutputCacheStore outputCacheStore)
{
    var id = await repositorio.Crear(prospectos_ocrd);
    await outputCacheStore.EvictByTagAsync("prospectos-get", default);
    return TypedResults.Created($"/prospectos/{id}", prospectos_ocrd);
}

static async Task<Results<NoContent, NotFound>> ActualizarProspecto(string cardcode, MinimalAPI.Entidades.OCRD_SalesForce prospectos_ocrd, IRepositorioOCRD repositorio, IOutputCacheStore outputCacheStore)
{
    var existe = await repositorio.Existe(cardcode);
    if (!existe)
    {
        return TypedResults.NotFound();
    }

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

