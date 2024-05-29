using Microsoft.EntityFrameworkCore;
using MinimalAPI.Entidades;

namespace MinimalAPI.Repositorios
{
    public class RepositorioOCRD : IRepositorioOCRD
    {
        private readonly ApplicationDbContext context;

        public RepositorioOCRD(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<int> Crear(OCRD_SalesForce ocrd)
        {
            context.Add(ocrd);
            await context.SaveChangesAsync();
            return ocrd.Id;
        }
        public async Task Actualizar(OCRD_SalesForce ocrd)
        {
            context.Update(ocrd);
            await context.SaveChangesAsync();
        }
        public async Task<bool> Existe(string cardcode)
        {
            return await context.OCRDs.AnyAsync(p => p.CardCode == cardcode);
        }
        public async Task<bool> Existe2(int id)
        {
            return await context.OCRDs.AnyAsync(p => p.Id == id);
        }
        public async Task<OCRD_SalesForce?> ObtenerPorCardCode(string cardCode)
        {
            return await context.OCRDs.FirstOrDefaultAsync(x => x.CardCode == cardCode);
        }
        async Task<OCRD_SalesForce?> IRepositorioOCRD.ObtenerPorId(int id)
        {
            return await context.OCRDs.FirstOrDefaultAsync(x => x.Id == id);
        }
        async Task<List<OCRD_SalesForce>> IRepositorioOCRD.ObtenerTodos()
        {
            return await context.OCRDs.ToListAsync();
        }
        public async Task Borrar(int id)
        {
            await context.Pedidos.Where(x => x.Id == id).ExecuteDeleteAsync();
        }
              

        
    }
}
