namespace API.Features.Projects.Entities
{
    public class ProjectEntity
    {
        public int ProjectId { get;  set; }
        public string Name { get;  set; } = default!;
        public string? Description { get;  set; } = default!;

        public DateOnly? StartDate { get;  set; }
        public DateOnly? EndDate { get;  set; }

        public string? CreatedBy { get;  set; } = default!;
        public bool?  IsActive { get;  set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        
        public ProjectEntity() { } // for persistence

        public ProjectEntity(
            int projectId,
            string name,
            string? description,
            DateOnly? startDate,
            DateOnly? endDate,
            string createdBy,
            bool isActive, DateTime createdAt = default, DateTime updatedAt = default)
        {
            ProjectId = projectId;
            Name = name;
            Description = description;
            StartDate = startDate;
            EndDate = endDate;
            CreatedBy = createdBy;
            IsActive = isActive;
        }

        // Example behaviour: mark active/inactive
        public void Activate() => IsActive = true;
        public void Deactivate() => IsActive = false;
    }

}
