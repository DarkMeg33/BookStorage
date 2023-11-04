using BookStorage.Models.Dto.EndpointResultDto;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using BookStorage.Models.Results;
using BookStorage.Extensions;

namespace BookStorage.Controllers
{
    public class BaseController : Controller
    {
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult RedirectToHome()
        {
            return RedirectToAction("Index", "Home");
        }

        //TODO add access-denied page

        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult InternalServerError()
        {
            return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult ErrorResponse(Dictionary<string, string> errors)
        {
            ModelState.AddErrorsFromDictionary(errors);

            return errors != null && errors.Any() ? new ValidationFailedResult(ModelState) : InternalServerError();
        }
        
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult DynamicResultResponse(EndpointResultDto result)
        {
            return result.IsSuccess ? Ok() : ErrorResponse(result.Errors);
        }
    }
}
