namespace API.Common.Logging
{
        public interface ISqlLogger
        {
            Task LogAsync(LogEntry entry);
        }
}
