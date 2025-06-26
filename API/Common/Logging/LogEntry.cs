namespace API.Common.Logging
{
    //public class LogEntry
    //{
    //    public string LogLevel { get; set; } = "Error";
    //    public string? EventCode { get; set; }
    //    public string? CorrelationId { get; set; }
    //    public string? UserMessage { get; set; }
    //    public string? TechnicalDetails { get; set; }
    //    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    //}

    public class LogEntry
    {
        public string LogLevel { get; set; } = "Error";
        public string EventCode { get; set; } = string.Empty;
        public string CorrelationId { get; set; } = string.Empty;
        public string UserMessage { get; set; } = string.Empty;
        public string TechnicalDetails { get; set; } = string.Empty;

        public string? Module { get; set; }
        public string? Layer { get; set; }
        public string? Method { get; set; }

        public string? RequestedBy { get; set; }
        public string? Source { get; set; }

        public int? UserId { get; set; }
        public object? InputParameters { get; set; }

        public string? RequestUrl { get; set; }
        public string? HttpMethod { get; set; }
        public string? ClientIP { get; set; }
    }

}
