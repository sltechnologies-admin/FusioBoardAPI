namespace API.Common.Logging
{
    public class LogEntry
    {
        public string LogLevel { get; set; } = "Error";
        public string? EventCode { get; set; }
        public string? CorrelationId { get; set; }
        public string? UserMessage { get; set; }
        public string? TechnicalDetails { get; set; }
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    }

}
