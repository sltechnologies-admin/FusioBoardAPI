using System.Data;
using Microsoft.Data.SqlClient;
using API.Data.Interfaces;

namespace API.Data
{
    public class DatabaseService : IDatabaseService
    {
        private readonly string _connectionString;

        public DatabaseService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<int> ExecuteNonQueryAsync(string query, List<SqlParameter> parameters)
        {
            using SqlConnection conn = new(_connectionString);
            using SqlCommand cmd = new(query, conn);
            cmd.Parameters.AddRange(parameters.ToArray());

            await conn.OpenAsync();
            return await cmd.ExecuteNonQueryAsync();
        }

        public async Task<object> ExecuteScalarAsync(string query, List<SqlParameter> parameters)
        {
            using SqlConnection conn = new(_connectionString);
            using SqlCommand cmd = new(query, conn);
            cmd.Parameters.AddRange(parameters.ToArray());

            await conn.OpenAsync();
            return await cmd.ExecuteScalarAsync();
        }

        public async Task<DataTable> ExecuteQueryAsync(string query, List<SqlParameter> parameters)
        {
            using SqlConnection conn = new(_connectionString);
            using SqlCommand cmd = new(query, conn);
            cmd.Parameters.AddRange(parameters.ToArray());

            using SqlDataAdapter adapter = new(cmd);
            DataTable dt = new();
            adapter.Fill(dt);
            return dt;
        }

        public async Task<List<T>> ExecuteReaderAsync<T>(string query, List<SqlParameter> parameters, Func<SqlDataReader, T> map)
        {
            using SqlConnection conn = new(_connectionString);
            using SqlCommand cmd = new(query, conn);
            cmd.Parameters.AddRange(parameters.ToArray());

            await conn.OpenAsync();

            using SqlDataReader reader = await cmd.ExecuteReaderAsync();
            List<T> results = new();

            while (await reader.ReadAsync())
            {
                results.Add(map(reader));
            }

            return results;
        }

    }
}