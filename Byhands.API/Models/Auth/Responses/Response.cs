namespace Byhands.API.Models.Auth.Responses;

public class Response<T>
{
    public bool IsSuccessful { get; set; }
    public string Message { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public T Data { get; set; } = default(T)!;
}
