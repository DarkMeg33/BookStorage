using BookStorage.Models.Entities.UserEntities;

namespace BookStorage.Models.ViewModels.UserViewModel
{
    public class FormUserProfileViewModel : UserProfileViewModel
    {
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public FormUserProfileViewModel() { }

        public FormUserProfileViewModel(RetrieveUserEntity entity) : base(entity)
        {
        }
    }
}