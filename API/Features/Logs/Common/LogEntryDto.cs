namespace API.Features.Logs.Common
{
        public class LogEntryDto
        {
            public int LogId { get; set; }
            public string LogLevel { get; set; }
            public string EventCode { get; set; }
            public string CorrelationId { get; set; }
            public string UserMessage { get; set; }
            public string TechnicalDetails { get; set; }
            public string Module { get; set; }
            public string Layer { get; set; }
            public string Method { get; set; }
            public string RequestedBy { get; set; }
            public string Source { get; set; }
            public int? UserId { get; set; }
            public string InputParameters { get; set; }
            public string RequestUrl { get; set; }
            public string HttpMethod { get; set; }
            public string ClientIP { get; set; }
            public DateTimeOffset CreatedAt { get; set; }
        }
}

