using BookStorage.Models.Dto.EndpointResultDto;

namespace BookStorage.Services.AdminService
{
    public interface IAdminService
    {
        Task<EndpointResultDto> TrySingInAsAdminAsync();
    }
}