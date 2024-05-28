using BookStorage.Helpers.Formatter;
using BookStorage.Models.Entities.UserEntities;

namespace BookStorage.Models.ViewModels.UserViewModel
{
    public class UserProfileViewModel
    {
        public string Username { get; set; }
        public string Email { get; set; }

        public UserProfileViewModel() { }

        public UserProfileViewModel(RetrieveUserEntity entity)
        {
            Username = entity.Username;
            Email = entity.Email;
        }
    }
}