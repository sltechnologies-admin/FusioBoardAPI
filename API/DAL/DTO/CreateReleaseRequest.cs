namespace API.DAL.DTO
{
    public class CreateReleaseRequest
    {
        public string ReleaseName { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Description { get; set; }
    }

}
