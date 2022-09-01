using Dapper;
using ManjeoPresupuesto.Models;
using Microsoft.Data.SqlClient;

namespace ManjeoPresupuesto.Services
{
    public class RepositorioTransacciones : IRepositorioTransacciones
    {
        private readonly string ConnectionString;

        public RepositorioTransacciones(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("Conexion");
        }

        public async Task Crear(Transaccion transaccion)
        {
            using var connection = new SqlConnection(ConnectionString);
            var id = await connection.QuerySingleAsync<int>("Transacciones_Insertar",
                new
                {
                    transaccion.UsuarioId,
                    transaccion.FechaTransaccion,
                    transaccion.Monto,
                    transaccion.Nota,
                    transaccion.CuentaId,
                    transaccion.CategoriaId
                },
                commandType: System.Data.CommandType.StoredProcedure
                );
            transaccion.Id = id;
        }

        public async Task Actualizar(Transaccion transaccion, decimal montoAnterior, 
            int cuentaAnteriorId)
        {
            using var connection = new SqlConnection(ConnectionString);
            await connection.ExecuteAsync("Transacciones_Actualizar",
                new
                {
                    transaccion.Id,
                    transaccion.FechaTransaccion,
                    transaccion.Monto,
                    transaccion.CategoriaId,
                    transaccion.CuentaId,
                    transaccion.Nota,
                    montoAnterior,
                    cuentaAnteriorId
                },
                commandType: System.Data.CommandType.StoredProcedure
             );
        }

        public async Task<Transaccion> ObtenerPorId(int id, int usuarioId)
        {
            using var connection = new SqlConnection(ConnectionString);
            return await connection.QueryFirstOrDefaultAsync<Transaccion>(
                @"SELECT Transacciones.*, cat.TipoOperacionId FROM Transacciones
                INNER JOIN Categorias cat
                ON cat.Id = Transacciones.CategoriaId
                WHERE Transacciones.ID = @Id AND Transacciones.UsuarioId = @UsuarioId", 
                new {id, usuarioId}
                );
        }

        public async Task Borrar(int id)
        {
            using var connection = new SqlConnection(ConnectionString);
            await connection.ExecuteAsync("Transacciones_Borrar",

                    new {id},
                    commandType: System.Data.CommandType.StoredProcedure

                );
        }
    }
}
