using System.ComponentModel;

namespace TaskCoreApi.Dto.UserDto;

public class UpdateUserRequest
{
    [MinLength(3)]
    [MaxLength(32)]
    public string? Name { get; set; }
    
    [EmailAddress]
    public string? Email { get; set; }
    
    [MinLength(6)]
    [MaxLength(32)]
    [PasswordPropertyText]
    public string? Password { get; set; }

    public List<int>? ProjectsId { get; set; } = new();
}