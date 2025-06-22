using Microsoft.Data.SqlClient;

namespace API.Common.Extensions
{
    public static class SqlDataReaderExtensions
    {
        public static int GetInt32(this SqlDataReader r, string name)
           => r.GetInt32(r.GetOrdinal(name));

        public static string GetString(this SqlDataReader r, string name)
           => r.GetString(r.GetOrdinal(name));

        public static bool GetBoolean(this SqlDataReader r, string name)
           => r.GetBoolean(r.GetOrdinal(name));

        public static DateTime GetDateTime(this SqlDataReader r, string name)
           => r.GetDateTime(r.GetOrdinal(name));

        public static DateTimeOffset GetDateTimeOffset(this SqlDataReader r, string name)
           => r.GetDateTimeOffset(r.GetOrdinal(name));
    }

}
