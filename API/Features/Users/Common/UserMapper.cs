using API.Features.Users.Common;
using API.Features.Users.Entities;

namespace API.Features.Users.Common
{
    public static class UserMapper
    {
        public static UserDto ToDto(this UserEntity entity)
        {
            return new UserDto {
                UserId = entity.UserId,
                Username = entity.Username,
                Email = entity.Email,
                IsActive = entity.IsActive,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }

        public static List<UserDto> ToDtoList(this IEnumerable<UserEntity> entities)
        {
            return entities.Select(e => e.ToDto()).ToList();
        }
    }
}
