using Microsoft.Data.SqlClient;

namespace API.Common.Extensions
{
    public static class SqlDataReaderExtensions
    {
        public static void DumpColumns(this SqlDataReader reader)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                Console.WriteLine($"{i}: {reader.GetName(i)}");
            }
        }

        public static int GetInt32(this SqlDataReader reader, string columnName)
        {
            int ordinal = reader.GetOrdinal(columnName);  // get index once
            return reader.GetInt32(ordinal);
        }
        public static string? GetSafeString(this SqlDataReader reader, string columnName)
        {
            int ordinal = reader.GetOrdinal(columnName);
            return reader.IsDBNull(ordinal) ? null : reader.GetString(ordinal);
        }
        public static string? GetString(this SqlDataReader reader, string columnName)
        {
            int index = reader.GetOrdinal(columnName); // look up column index once
            return reader.GetString(index);
        }

        public static bool GetBoolean(this SqlDataReader r, string name)
           => r.GetBoolean(r.GetOrdinal(name));

        public static DateTime GetDateTime(this SqlDataReader r, string name)
           => r.GetDateTime(r.GetOrdinal(name));

        public static DateTimeOffset GetDateTimeOffset(this SqlDataReader r, string name)
           => r.GetDateTimeOffset(r.GetOrdinal(name));
    }

}
