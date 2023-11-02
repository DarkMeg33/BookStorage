using Newtonsoft.Json;

namespace BookStorage.Models.Dto.ValidationDto
{
    public class ValidationErrorDto
    {
        public ValidationErrorDto(string field, string message)
        {
            Field = field != string.Empty ? field : null;
            Message = message; 
        }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Field { get; set; }
        public string Message { get; set; }
    }
}