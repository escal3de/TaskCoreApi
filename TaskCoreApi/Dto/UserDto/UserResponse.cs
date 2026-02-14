namespace TaskCoreApi.Dto.UserDto;

public class UserResponse
{
    public int Id  { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }

    public List<ProjectResponse> Projects { get; set; } = new();
}