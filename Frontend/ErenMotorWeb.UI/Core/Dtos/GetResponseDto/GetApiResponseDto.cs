namespace ErenMotorWeb.UI.Core.Dtos.GetResponseDto
{
    public record GetApiResponseDto<T>(T Data, bool Success, string Message, int StatusCode, Dictionary<string, List<string>>? Errors = null)
    {

    }
}
