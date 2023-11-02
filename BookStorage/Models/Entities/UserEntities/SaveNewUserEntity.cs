using BookStorage.Models.ViewModels.AccountViewModel;

namespace BookStorage.Models.Entities.UserEntities
{
    public class SaveNewUserEntity
    {
        private string _password;

        public SaveNewUserEntity(SignUpViewModel viewModel)
        {
            Username = viewModel.Username;
            Email = viewModel.Email;
            Password = viewModel.Password;
        }

        public string Username { get; set; }
        public string Email { get; set; }

        public string Password
        {
            get => !string.IsNullOrEmpty(_password) ? BCrypt.Net.BCrypt.HashPassword(_password) : null;
            set => _password = value;
        }
    }
}