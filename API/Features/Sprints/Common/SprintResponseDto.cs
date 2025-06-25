namespace API.Features.Sprints.Common
{
    namespace API.Features.Sprints.Common
    {
        public class SprintResponseDto
        {
            public int Id { get; set; }
            public int ProjectId { get; set; }
            public string Name { get; set; } = null!;
            public string? Goal { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public bool IsActive { get; set; }
        }
    }

}
