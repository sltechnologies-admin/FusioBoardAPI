namespace API.Common.Logging
{
    public class LogEntry
    {
        public string? LogLevel { get; set; }
        public string? EventCode { get; set; }
        public string? CorrelationId { get; set; }
        public string? Message { get; set; }
        public string? Exception { get; set; }
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    }
}
