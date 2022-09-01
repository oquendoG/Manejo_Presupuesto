using ManjeoPresupuesto.Models;

namespace ManjeoPresupuesto.Services
{
    public interface IRepositorioTiposCuentas
    {
        Task Actualizar(TipoCuenta tipoCuenta);
        Task Borrar(int id);
        Task Crear(TipoCuenta tipoCuenta);
        Task<bool> Existe(string nombre, int usuarioId);
        Task<IEnumerable<TipoCuenta>> Obtener(int UsuarioId);

        /// <summary>
        /// Método que retorna un TipoCuenta del id especificado de un usuario especifico
        /// </summary>
        /// <param name="id">Parámetro que indica el identificador del tipo cuenta solicitado</param>
        /// <param name="usuarioId">Indica el identificador del usaurio al que pertenece</param>
        /// <returns>Un objeto TipoCuenta traido de la base de datos del usuario especificado en el usuarioId</returns>
        Task<TipoCuenta> ObtenerPorId(int id, int usuarioId);
        Task Ordenar(IEnumerable<TipoCuenta> tiposCuentasOrdenados);
    }
}
