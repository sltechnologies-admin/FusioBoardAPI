namespace API.Models
{
    public class SystemLogs
    {
    }
    public class AuditLog
    {
        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
        public string Action { get; set; }
        public string Entity { get; set; }
        public string Details { get; set; }
        public DateTime Timestamp { get; set; }

        public User User { get; set; }
    }

}
