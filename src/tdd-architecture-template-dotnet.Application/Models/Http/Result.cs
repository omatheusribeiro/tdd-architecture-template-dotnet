namespace tdd_architecture_template_dotnet.Application.Models.Http
{
    public class Result<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public int StatusCode { get; set; }
        public T? Data { get; set; }

        public static Result<T> Ok(T data, string message = "Success") =>
            new Result<T> { Success = true, Data = data, Message = message, StatusCode = 200 };

        public static Result<T> Fail(string message, int statusCode = 500) =>
            new Result<T> { Success = false, Message = message, StatusCode = statusCode };
    }
}
