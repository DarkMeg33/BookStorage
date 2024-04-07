using BookStorage.Models.ViewModels.UserViewModel;

namespace BookStorage.Models.Entities.UserEntities
{
    public class SaveUserProfileEntity : UserEntity
    {
        public string Password { get; set; }

        public SaveUserProfileEntity(FormUserProfileViewModel viewModel, 
            UserEntity existedUser, string oldPassword)
        {
            Password = !string.IsNullOrEmpty(viewModel.Password) 
                ? BCrypt.Net.BCrypt.HashPassword(viewModel.Password) : oldPassword;
            Username = viewModel.Username ?? existedUser.Username;
            Email = viewModel.Email ?? existedUser.Email;
            UserId = existedUser.UserId;
            AvatarStorageReference = existedUser.AvatarStorageReference;
        }
    }
}