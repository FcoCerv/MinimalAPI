using Microsoft.EntityFrameworkCore;
using MinimalAPI.Entidades;

namespace MinimalAPI.Repositorios
{
    public class RepositorioPacientes : IRepositorioGenero
    {
        private readonly ApplicationDbContext context;

        public RepositorioPacientes(ApplicationDbContext context)
        {
            this.context = context;
        }       

        public async Task<int> Crear(Paciente paciente)
        {
            context.Add(paciente);
            await context.SaveChangesAsync();
            return paciente.Id;
        }

        public async Task Actualizar(Paciente paciente)
        {
            context.Update(paciente);
            await context.SaveChangesAsync();
        }

        public async Task<bool> Existe(int id)
        {
            return await context.Pacientes.AnyAsync(x => x.Id == id);
        }

        public async Task<Paciente?> ObtenerPorCodigoSn(string codigosn)
        {
            return await context.Pacientes.FirstOrDefaultAsync(x => x.CodigoSN == codigosn);
        }

        public async Task<Paciente?> ObtenerPorId(int id)
        {
            return await context.Pacientes.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Paciente>> ObtenerTodos()
        {
            //Con este lo podemos ordenar por el campo que escojamos
            //return await context.Pacientes.OrderBy(x => x.EstadoDeProspecto).ToListAsync();
            return await context.Pacientes.ToListAsync();
        }

        public async Task Borrar(int id)
        {
            await context.Pacientes.Where(x => x.Id == id).ExecuteDeleteAsync();    
        }
    }
}

