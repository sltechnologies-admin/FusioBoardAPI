namespace API.Models
{
    public class ProjectStructure
    {
    }
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Module> Modules { get; set; }
        public ICollection<Release> Releases { get; set; }
    }

    public class Module
    {
        public  int Id { get; set; }
        public string Name { get; set; }
        public int ProjectId { get; set; }

        public Project Project { get; set; }
    }

    public class Release
    {
        public int Id { get; set; }
        public string Version { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int ProjectId { get; set; }

        public Project Project { get; set; }
    }

}
