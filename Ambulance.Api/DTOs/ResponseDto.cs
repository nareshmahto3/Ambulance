namespace Ambulance.Api.DTOs
{
    public class ResponseDto
    {
        public bool IsSuccess { get; set; } = true;
        public string? Message { get; set; } = string.Empty;
        public object? Data { get; set; }
        public static ResponseDto Fail(string message = "FAILED") => new ResponseDto { IsSuccess = false, Message = message };
        public static ResponseDto Success(object? Data, string message = "SUCCESS") => new ResponseDto { Data = Data, IsSuccess = true, Message = message };

    }
}
