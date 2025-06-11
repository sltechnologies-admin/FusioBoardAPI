namespace API.Models
{
    public class Configuration
    {
    }
    public class TaskStatus
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class TaskType
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class TaskPriority
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

}
