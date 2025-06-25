namespace API.DAL.DTO
{
    public class AssignRoleRequest
    {
        public int ProjectId { get; set; }
        public int RoleId { get; set; }
        public int UserId { get; set; }
    }
}
