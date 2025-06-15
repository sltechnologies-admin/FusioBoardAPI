namespace API.Models
{
    public class AuthIdentity
    {
    }

    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public ICollection<UserProjectRole> ProjectRoles { get; set; }


    }



    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<RolePermission> Permissions { get; set; }
    }

    public class UserProjectRole
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProjectId { get; set; }
        public int RoleId { get; set; }

        public User User { get; set; }
        public Project Project { get; set; }
        public Role Role { get; set; }
    }

    public class Permission
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class RolePermission
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int PermissionId { get; set; }

        public Role Role { get; set; }
        public Permission Permission { get; set; }
    }

}
