using System.Data;
using Microsoft.Data.SqlClient;

namespace API.Data.Interfaces
{
    public interface IDatabaseService
    {
        Task<int> ExecuteNonQueryAsync(string query, List<SqlParameter> parameters);
        Task<object> ExecuteScalarAsync(string query, List<SqlParameter> parameters);
        Task<DataTable> ExecuteQueryAsync(string query, List<SqlParameter> parameters);
        Task<List<T>> ExecuteReaderAsync<T>(string query, List<SqlParameter> parameters, Func<SqlDataReader, T> map);

    }
}
