namespace API.Features.Sprints.Common
{
    public class SprintCreateDto
    {
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public string? Goal { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

}
