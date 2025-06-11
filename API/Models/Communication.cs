namespace API.Models
{
    public class Communication
    {
    }
    public class Notification
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }

        public User User { get; set; }
    }

    public class Comment
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public Guid TaskId { get; set; }
        public Guid AuthorId { get; set; }

        public Task Task { get; set; }
        public User Author { get; set; }
    }

    public class Attachment
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string FileUrl { get; set; }
        public Guid TaskId { get; set; }

        public Task Task { get; set; }
    }

}
