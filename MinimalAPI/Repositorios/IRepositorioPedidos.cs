using MinimalAPI.Entidades;

namespace MinimalAPI.Repositorios
{
    public interface IRepositorioPedidos
    {
        Task<List<Pedidos>> ObtenerTodos();
        Task<Pedidos?> ObtenerPorId(int id);
        Task<Pedidos?> ObtenerPedidoPorCardCode(string CardCode);
        Task<int> Crear(Pedidos pedidos);
        Task<bool> Existe(string cardcode);  
        Task Actualizar(Pedidos pedidos);
        Task Borrar(int id);
       
    }
}
