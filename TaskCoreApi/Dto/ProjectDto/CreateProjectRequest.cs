namespace TaskCoreApi.Dto.ProjectDto;

public class CreateProjectRequest
{
    [Required]
    [MinLength(3)]
    [MaxLength(32)]
    public string Name { get; set; }
    
    [Required]
    [MinLength(10)]
    [MaxLength(500)]
    public string Description { get; set; }

    public List<CreateTaskItemRequest> Tasks { get; set; } = new();
}