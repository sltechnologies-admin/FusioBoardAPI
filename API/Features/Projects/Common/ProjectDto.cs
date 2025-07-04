﻿namespace API.Features.Projects.Common
{
    public class ProjectDto
    {
        public int ProjectId { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string CreatedBy { get; set; } = default!;
        public bool? IsActive { get; set; }
    }

}


