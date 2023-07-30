using System.ComponentModel.DataAnnotations;

namespace Core.Dtos;

public class LoginDto
{
    [Required]
    public string? UserName { get; set; }
    
    [Required]
    [StringLength(100, MinimumLength = 4, ErrorMessage = "Password length must be at least 4")]
    public string? Password { get; set; }
}