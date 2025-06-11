namespace API.Models
{
    public class Planning
    {
    }
    public class Epic
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Guid ProjectId { get; set; }

        public ICollection<Feature> Features { get; set; }
    }

    public class Feature
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Guid EpicId { get; set; }

        public Epic Epic { get; set; }
    }

    public class Story
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Guid? FeatureId { get; set; }

        public Feature Feature { get; set; }
        public ICollection<SprintStory> SprintMappings { get; set; }
    }

    public class Sprint
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public ICollection<SprintStory> Stories { get; set; }
    }

    public class SprintStory
    {
        public Guid Id { get; set; }
        public Guid SprintId { get; set; }
        public Guid StoryId { get; set; }

        public Sprint Sprint { get; set; }
        public Story Story { get; set; }
    }

}
