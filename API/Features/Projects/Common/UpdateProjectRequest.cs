namespace API.Features.Projects.Common
{
    public class UpdateProjectRequest
    {
        public int ProjectId { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? IsActive { get; set; }
    }
}
