namespace API.Features.Projects.Common
{
    public class CreateProjectRequest
    {
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        // Required for creation
        public int CreatedBy { get; set; }
    }
}