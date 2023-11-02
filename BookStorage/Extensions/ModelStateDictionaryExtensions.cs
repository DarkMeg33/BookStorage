using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BookStorage.Extensions
{
    public static class ModelStateDictionaryExtensions
    {
        public static void AddErrorsFromDictionary(this ModelStateDictionary modelState,
            Dictionary<string, string> errors)
        {
            if (errors != null)
            {
                foreach (KeyValuePair<string, string> error in errors)
                {
                    modelState.AddModelError(error.Key, error.Value);
                }
            }
        }
    }
}