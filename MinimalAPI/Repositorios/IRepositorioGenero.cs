using MinimalAPI.Entidades;

namespace MinimalAPI.Repositorios
{
    public interface IRepositorioGenero
    {
        Task<List<Paciente>> ObtenerTodos();
        Task<Paciente?> ObtenerPorId(int id);
        Task<Paciente?> ObtenerPorCodigoSn(string codigoSn);
        Task<int> Crear(Paciente paciente);
        Task<bool> Existe(int id);  
        Task Actualizar(Paciente paciente);
        Task Borrar(int id);
    }
}
