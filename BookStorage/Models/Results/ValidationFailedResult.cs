using BookStorage.Models.Dto.ValidationDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BookStorage.Models.Results
{
    public class ValidationFailedResult : ObjectResult
    {
        public ValidationFailedResult(ModelStateDictionary modelState) : base(new ValidationResultDto(modelState))
        {
            StatusCode = StatusCodes.Status400BadRequest;
        }
    }
}