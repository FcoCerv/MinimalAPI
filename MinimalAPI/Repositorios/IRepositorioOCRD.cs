using MinimalAPI.Entidades;

namespace MinimalAPI.Repositorios
{
    public interface IRepositorioOCRD
    {
        Task<List<OCRD_SalesForce>> ObtenerTodos();
        Task<OCRD_SalesForce?> ObtenerPorId(int id);
        Task<OCRD_SalesForce?> ObtenerPorCardCode(string cardName);
        Task<int> Crear(OCRD_SalesForce ocrd);
        //Task<bool> Existe(int id);
        Task<bool> Existe(string cardcode);
        Task Actualizar(OCRD_SalesForce ocrd);
        Task Borrar(int id);
    }
}
