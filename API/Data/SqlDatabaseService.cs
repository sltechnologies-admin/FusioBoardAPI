using System.Data;
using Microsoft.Data.SqlClient;
using API.Data.Interfaces;

namespace API.Data
{
    public class SqlDatabaseService : IDatabaseService
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public SqlDatabaseService(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<int> ExecuteNonQueryAsync(string query, List<SqlParameter> parameters, CommandType commandType = CommandType.Text)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(query, connection) {
                CommandType = commandType
            };
            command.Parameters.AddRange(parameters.ToArray());

            await connection.OpenAsync();
            return await command.ExecuteNonQueryAsync();
        }

        public async Task<object> ExecuteScalarAsync(string query, List<SqlParameter> parameters, CommandType commandType = CommandType.Text)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(query, connection) {
                CommandType = commandType
            };
            command.Parameters.AddRange(parameters.ToArray());

            await connection.OpenAsync();
            return await command.ExecuteScalarAsync();
        }

        public async Task<DataTable> ExecuteQueryAsync(string query, List<SqlParameter> parameters, CommandType commandType = CommandType.Text)
        {
            var dataTable = new DataTable();
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(query, connection) {
                CommandType = commandType
            };
            command.Parameters.AddRange(parameters.ToArray());

            using var adapter = new SqlDataAdapter(command);
            await Task.Run(() => adapter.Fill(dataTable));
            return dataTable;
        }

        public async Task<List<T>> ExecuteReaderAsync<T>(
            string query,
            List<SqlParameter> parameters,
            Func<SqlDataReader, T> map,
            CommandType commandType = CommandType.Text)
        {
            var results = new List<T>();

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(query, connection) {
                CommandType = commandType
            };

            command.Parameters.AddRange(parameters.ToArray());

            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                try
                {
                    results.Add(map(reader));
                }
                catch (Exception ex)
                {
                    // 🔥 This is your technical error (e.g. bad DB column value, null, cast failure)
                    throw new DataException($"Mapping failed for query '{query}': {ex.Message}", ex);
                }
            }

            return results;
        }


    }

}