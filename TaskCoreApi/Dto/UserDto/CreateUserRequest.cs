using System.ComponentModel;

namespace TaskCoreApi.Dto.UserDto;

public class CreateUserRequest
{
    [Required]
    [MinLength(3)]
    [MaxLength(32)]
    public string Name { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    [PasswordPropertyText]
    [MinLength(6)]
    [MaxLength(32)]
    public string Password { get; set; }

    public List<int> ProjectsId { get; set; } = new();
}