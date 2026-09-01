namespace ErenMotorWeb.Dto.ServiceResponseDto
{
    public class ApiResponseDto<T>(T? data, bool success, int statusCode, string message, Dictionary<string, List<string>> errors = null)
    {
        public T? Data { get; set; } = data;
        public bool Success { get; set; } = success;
        public int StatusCode { get; set; } = statusCode;
        public string Message { get; set; } = message;
        public Dictionary<string, List<string>> Errors { get; set; } = errors ?? []; // for fluent validation
    }
}
