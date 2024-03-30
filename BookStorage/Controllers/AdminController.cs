using BookStorage.Models.Dto.EndpointResultDto;
using BookStorage.Services.AdminService;
using Microsoft.AspNetCore.Mvc;

namespace BookStorage.Controllers
{
    public class AdminController : BaseController
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet("/admin-sign-in")]
        public async Task<IActionResult> AdminSignIn()
        {
            EndpointResultDto result = await _adminService.TrySingInAsAdminAsync();

            return result.IsSuccess ? RedirectToHome() : NotFound();
        }
    }
}
