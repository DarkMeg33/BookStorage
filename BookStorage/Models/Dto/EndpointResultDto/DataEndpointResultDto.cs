namespace BookStorage.Models.Dto.EndpointResultDto
{
    public class DataEndpointResultDto<T> : EndpointResultDto
    {
        public T Data { get; set; }

        public DataEndpointResultDto(bool isSuccess, T data, Dictionary<string, string> errors) : base(isSuccess, errors)
        {
            Data = data;
        }
    }
}