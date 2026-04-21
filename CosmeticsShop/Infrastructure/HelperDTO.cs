namespace CosmeticsShop.Infrastructure;

public class HelperDTO
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public object? Data { get; set; }

    public static HelperDTO Ok(string? message = null, object? data = null) =>
        new() { Success = true, Message = message, Data = data };

    public static HelperDTO Fail(string message) =>
        new() { Success = false, Message = message };
}
