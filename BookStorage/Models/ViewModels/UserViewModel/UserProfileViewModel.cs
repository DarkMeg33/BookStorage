using BookStorage.Models.Entities.UserEntities;

namespace BookStorage.Models.ViewModels.UserViewModel
{
    public class UserProfileViewModel
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }

        public UserProfileViewModel(UserEntity entity)
        {
            UserId = entity.UserId!.Value;
            Username = entity.Username;
            Email = entity.Email;
        }
    }
}