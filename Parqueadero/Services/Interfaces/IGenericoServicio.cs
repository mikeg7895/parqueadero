namespace Parqueadero.Services.Interfaces;

public interface IGenericoServicio<T>
{
    Task<T> Insertar(T valor);
    Task<T?> ObtenerPorId(int id);
    Task<IEnumerable<T>> ObtenerTodo();
    void Actualizar(T valor);
    Task<bool> Eliminar(int id);
}