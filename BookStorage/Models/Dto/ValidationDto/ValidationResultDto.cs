using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BookStorage.Models.Dto.ValidationDto
{
    public class ValidationResultDto
    {
        List<ValidationErrorDto> Errors { get; }

        public ValidationResultDto(ModelStateDictionary modelState)
        {
            Errors = modelState.Keys
                .SelectMany(key =>
                    modelState[key].Errors.Select(error => new ValidationErrorDto(key, error.ErrorMessage)))
                .ToList();
        }
    }
}