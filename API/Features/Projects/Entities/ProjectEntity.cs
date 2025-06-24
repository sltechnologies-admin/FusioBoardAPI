namespace API.Features.Projects.Entities
{
    public class ProjectEntity
    {
        public int ProjectId { get; set; }

        public string Name { get; set; } = default!;
        public string? Description { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public string? CreatedBy { get; set; }
        public bool? IsActive { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Required for EF or Dapper materialization
        public ProjectEntity() { }

        public ProjectEntity(
            int projectId,
            string name,
            string? description,
            DateTime? startDate,
            DateTime? endDate,
            string? createdBy,
            bool? isActive,
            DateTime? createdAt = null,
            DateTime? updatedAt = null)
        {
            ProjectId = projectId;
            Name = name;
            Description = description;
            StartDate = startDate;
            EndDate = endDate;
            CreatedBy = createdBy;
            IsActive = isActive;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        public void Activate() => IsActive = true;
        public void Deactivate() => IsActive = false;
    }
}
