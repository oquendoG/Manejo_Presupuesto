﻿using Dapper;
using ManjeoPresupuesto.Models;
using Microsoft.Data.SqlClient;

namespace ManjeoPresupuesto.Services
{
    public class RepositorioTiposCuentas : IRepositorioTiposCuentas
    {
        private readonly string connectionString;
        public RepositorioTiposCuentas(IConfiguration configuration)

        {
            connectionString = configuration.GetConnectionString("Conexion");
        }

        /// <summary>
        /// Crea un tipoCuenta en la base de datos
        /// </summary>
        /// <param name="tipoCuenta"></param>
        public async Task Crear(TipoCuenta tipoCuenta)
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>(
                $@"TiposCuentas_Insertar", 
                new {UsuarioId = tipoCuenta.UsuarioId, Nombre = tipoCuenta.Nombre},
                commandType: System.Data.CommandType.StoredProcedure);


            tipoCuenta.Id = id;
        }

        public async Task<bool> Existe(string nombre, int usuarioId)
        {
            using var connection = new SqlConnection(connectionString);
            var existe = await connection.QueryFirstOrDefaultAsync<int>
            (
              @"SELECT 1 FROM TiposCuentas 
              WHERE  Nombre = @Nombre AND UsuarioId = @UsuarioId",
              new { nombre, usuarioId }
            );

            return existe == 1;
        }

        public async Task<IEnumerable<TipoCuenta>> Obtener(int UsuarioId)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<TipoCuenta>
            (
                @"SELECT Id, Nombre, Orden FROM TiposCuentas
                WHERE UsuarioId = @UsuarioId ORDER BY Orden",
                new {UsuarioId}
            );
        }

        public async Task Actualizar(TipoCuenta tipoCuenta)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"UPDATE TiposCuentas
                                SET Nombre = @Nombre
                                WHERE Id = @Id
                            ", tipoCuenta);
        }

       
        public async Task<TipoCuenta> ObtenerPorId(int id, int usuarioId)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<TipoCuenta>
                (@"SELECT Id, Nombre, Orden From TiposCuentas
                    WHERE Id = @Id AND UsuarioId = @UsuarioId",
                   new { id, usuarioId });


        }
        
        public async Task Borrar(int id)
        {
            using var connection = new SqlConnection(connectionString);

            await connection.ExecuteAsync
                ("DELETE TiposCuentas WHERE Id  = @Id;", new {id});
        }

        public async Task Ordenar(IEnumerable<TipoCuenta> tiposCuentasOrdenados)
        {
            var query = "UPDATE TiposCuentas SET Orden = @Orden WHERE Id = @Id";
            using var connection = new SqlConnection(connectionString);

            await connection.ExecuteAsync(query, tiposCuentasOrdenados);
        }
    }

}