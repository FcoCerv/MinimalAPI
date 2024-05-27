using Microsoft.EntityFrameworkCore;
using MinimalAPI.Entidades;

namespace MinimalAPI.Repositorios
{
    public class RepositorioPedidos : IRepositorioPedidos
    {
        private readonly ApplicationDbContext context;

        public RepositorioPedidos(ApplicationDbContext context)
        {
            this.context = context;
        }       

        public async Task<int> Crear(Pedidos pedidos)
        {
            context.Add(pedidos);
            await context.SaveChangesAsync();
            return pedidos.Id;
        }

        public async Task Actualizar(Pedidos pedidos)
        {
            context.Update(pedidos);
            await context.SaveChangesAsync();
        }

        public async Task<bool> Existe(string cardcode)
        {
            return await context.Pedidos.AnyAsync(x => x.CardCode == cardcode);
        }

        public async Task<Pedidos?> ObtenerPedidoPorCardCode(string cardcode)
        {
            return await context.Pedidos.FirstOrDefaultAsync(x => x.CardCode == cardcode);
        }

        public async Task<Pedidos?> ObtenerPorId(int id)
        {
            return await context.Pedidos.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Pedidos>> ObtenerTodos()
        {
            //Con este lo podemos ordenar por el campo que escojamos
            //return await context.Pacientes.OrderBy(x => x.EstadoDeProspecto).ToListAsync();
            return await context.Pedidos.ToListAsync();
        }

        public async Task Borrar(int id)
        {
            await context.Pedidos.Where(x => x.Id == id).ExecuteDeleteAsync();    
        }
    }
}

