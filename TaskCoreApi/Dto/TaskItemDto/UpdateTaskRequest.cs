namespace TaskCoreApi.Dto.TaskItemDto;

public class UpdateTaskRequest
{
    [MinLength(3)]
    [MaxLength(32)]
    public string? Name { get; set; }
    
    [MinLength(10)]
    [MaxLength(500)]
    public string? Description { get; set; }
    public TaskItemStatus? Status { get; set; }

    public List<string>? Comments { get; set; }
    public List<Tag>? Tags { get; set; }
}