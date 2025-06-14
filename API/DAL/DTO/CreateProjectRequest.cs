﻿namespace API.DAL.DTO
{
    public class CreateProjectRequest
    {
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int CreatedBy { get; set; }
    }

}
