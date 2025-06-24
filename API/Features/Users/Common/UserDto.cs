namespace API.Features.Users.Common
{
    public class UserDto
    {
        public int? UserId { get; init; }
        public string Username { get; init; } = default!;
        public string Email { get; init; } = default!;
        public bool IsActive { get; init; }
        public DateTime? CreatedAt { get; init; }
        public DateTime? UpdatedAt { get; init; }
    }
    public class UserRoleDto
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; } = string.Empty;
    }
}
