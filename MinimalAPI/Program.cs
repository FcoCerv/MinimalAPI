using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MinimalAPI;
using MinimalAPI.Endpoints;
using MinimalAPI.Entidades;
using MinimalAPI.Migrations;
using MinimalAPI.Repositorios;
using AutoMapper;

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
builder.Services.AddScoped<IRepositorioPedidos, RepositorioPedidos>();
builder.Services.AddScoped<IRepositorioOCRD, RepositorioOCRD>();
builder.Services.AddAutoMapper(typeof(Program));


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
app.MapGroup("/prospectos").MapOcrd();
app.MapGroup("/pedidos").MapPedidos();

//Esta expresion esta en formato Lambda
//app.MapGet("/", [EnableCors(policyName:"libre")]() => "Hola Mundo!");

// Fin de area de los middleware

app.Run();
