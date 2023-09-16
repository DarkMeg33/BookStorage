namespace BookStorage.Models.Dto.EndpointResultDto
{
    public class EndpointResultDto
    {
        public bool IsSuccess { get; set; }
        public Dictionary<string, string> Errors { get; set; }

        public EndpointResultDto(bool isSuccess, Dictionary<string, string> errors)
        {
            IsSuccess = isSuccess;
            Errors = errors;
        }
    }
}