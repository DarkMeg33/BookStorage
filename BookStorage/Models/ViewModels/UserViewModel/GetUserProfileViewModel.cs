using BookStorage.Helpers.Formatter;
using BookStorage.Models.Entities.UserEntities;

namespace BookStorage.Models.ViewModels.UserViewModel
{
    public class GetUserProfileViewModel : UserProfileViewModel
    {
        public string AvatarUrl { get; set; }

        public GetUserProfileViewModel() { }

        public GetUserProfileViewModel(UserEntity entity) : base(entity)
        {
            AvatarUrl = StringFormatter.ToAvatarUrl(entity.AvatarStorageReference);
        }
    }
}