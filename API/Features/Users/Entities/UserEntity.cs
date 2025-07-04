namespace API.Features.Users.Entities
{
    public class UserEntity 
    {
        public int? UserId { get;  set; }
        public string Username { get;  set; } = default!;
        public string Email { get;  set; } = default!;
        public string PasswordHash { get;  set; } = default!;
        public bool IsActive { get;  set; }
        public DateTime? CreatedAt { get;  set; }
        public DateTime? UpdatedAt { get;  set; }

        // EF Core needs this
        public UserEntity() { }

        // Constructor for creating new users
        public UserEntity(string username, string email, string passwordHash)
        {
            Username = username?.Trim() ?? throw new ArgumentNullException(nameof(username));
            Email = email?.Trim() ?? throw new ArgumentNullException(nameof(email));
            PasswordHash = passwordHash ?? throw new ArgumentNullException(nameof(passwordHash));
            IsActive = true;
            CreatedAt = UpdatedAt = DateTime.UtcNow;
        }

        // Constructor for rehydration from persistence
        public UserEntity(int userId, string username, string email, string passwordHash, bool isActive, DateTime createdAt = default, DateTime updatedAt = default)
        {
            UserId = userId;
            Username = username;
            Email = email;
            PasswordHash = passwordHash;
            IsActive = isActive;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        // Domain behavior methods
        public void ChangeEmail(string newEmail)
        {
            if (string.IsNullOrWhiteSpace(newEmail))
                throw new ArgumentException("Email cannot be empty.", nameof(newEmail));

            Email = newEmail.Trim();
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetPassword(string newHashedPassword)
        {
            if (string.IsNullOrWhiteSpace(newHashedPassword))
                throw new ArgumentException("Password hash cannot be empty.", nameof(newHashedPassword));

            PasswordHash = newHashedPassword;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Activate() => IsActive = true;
        public void Deactivate() => IsActive = false;
    }
}
