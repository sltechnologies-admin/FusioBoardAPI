using System.Data;
using Microsoft.Data.SqlClient;

namespace API.Data.Interfaces
{
    public interface IDatabaseService
    {
        /// <summary>
        /// Executes a non-query command (e.g., INSERT, UPDATE, DELETE) and returns the number of affected rows.
        /// </summary>
        Task<int> ExecuteNonQueryAsync(
            string query,
            List<SqlParameter> parameters,
            CommandType commandType = CommandType.Text);

        /// <summary>
        /// Executes a scalar command and returns a single value (e.g., COUNT, MAX, etc.).
        /// </summary>
        Task<object> ExecuteScalarAsync(
            string query,
            List<SqlParameter> parameters,
            CommandType commandType = CommandType.Text);

        /// <summary>
        /// Executes a query and returns the result as a DataTable.
        /// </summary>
        Task<DataTable> ExecuteQueryAsync(
            string query,
            List<SqlParameter> parameters,
            CommandType commandType = CommandType.Text);

        /// <summary>
        /// Executes a query and maps each row of the result set to a strongly typed object.
        /// </summary>
        Task<List<T>> ExecuteReaderAsync<T>(
            string query,
            List<SqlParameter> parameters,
            Func<SqlDataReader, T> map,
            CommandType commandType = CommandType.Text);

        /// <summary>
        /// Executes a query that returns multiple result sets and allows custom async processing of the SqlDataReader.
        /// Useful for handling paginated data + metadata like TotalCount.
        /// </summary>
        Task ExecuteReaderMultiAsync(
            string query,
            List<SqlParameter> parameters,
            Func<SqlDataReader, Task> handleReaderAsync,
            CommandType commandType = CommandType.Text);
    }
}
