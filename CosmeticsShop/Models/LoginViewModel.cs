using System.ComponentModel.DataAnnotations;

namespace CosmeticsShop.Models;

public class LoginViewModel
{
    [Required(ErrorMessage = "Введите имя пользователя")]
    public string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = "Введите пароль")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    public bool RememberMe { get; set; }
    public string? ReturnUrl { get; set; }
}
