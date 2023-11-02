using BookStorage.Models.Entities.UserEntities;

namespace BookStorage.Models.Dto.UserDto
{
    public class UserDto
    {
        public int? UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }

        public UserDto(UserEntity entity)
        {
            UserId = entity.UserId;
            Username = entity.Username;
            Email = entity.Email;
        }
    }
}