using BookStorage.Models.Dto.EndpointResultDto;
using BookStorage.Models.ViewModels.AccountViewModel;

namespace BookStorage.Services.AccountService
{
    public interface IAccountService
    {
        public bool IsUserAuthenticated();
        public Task SignOutAsync();
        Task<EndpointResultDto> TrySignInAsync(SignInViewModel viewModel);
        Task<EndpointResultDto> TrySignUpAsync(SignUpViewModel viewModel);

    }
}