namespace TaskCoreApi.Dto.TaskItemDto;

public class CreateTaskItemRequest
{
    [Required]
    [MinLength(3)]
    [MaxLength(32)]
    public string Name { get; set; }
    
    [Required]
    [MinLength(10)]
    [MaxLength(500)]
    public string Description { get; set; }

    public List<string> Comments { get; set; } = new();
    public List<Tag> Tags { get; set; } = new();
}