using System.Net.Mail;
using System.Xml.Linq;

namespace API.Models
{
    public class Execution
    {
    }
    public class Task
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Guid? StoryId { get; set; }
        public Guid AssigneeId { get; set; }
        public Guid StatusId { get; set; }
        public Guid PriorityId { get; set; }
        public Guid TypeId { get; set; }

        public Story Story { get; set; }
        public User Assignee { get; set; }
        public TaskStatus Status { get; set; }
        public TaskPriority Priority { get; set; }
        public TaskType Type { get; set; }

        public ICollection<SubTask> SubTasks { get; set; }
        public ICollection<TaskProgressLog> ProgressLogs { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Attachment> Attachments { get; set; }
    }

    public class SubTask
    {
        public Guid Id { get; set; }
        public Guid ParentTaskId { get; set; }
        public string Title { get; set; }

        public Task ParentTask { get; set; }
    }

    public class TaskProgressLog
    {
        public Guid Id { get; set; }
        public Guid TaskId { get; set; }
        public string Status { get; set; }
        public string Comment { get; set; }
        public DateTime Timestamp { get; set; }

        public Task Task { get; set; }
    }

    public class Bug
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid TaskId { get; set; }

        public Task Task { get; set; }
    }

}
