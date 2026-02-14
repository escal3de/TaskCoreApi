namespace TaskCoreApi.Dto.ProjectDto;

public class UpdateProjectRequest
{
    [MinLength(3)]
    [MaxLength(32)]
    public string? Name { get; set; }
    
    [MinLength(10)]
    [MaxLength(500)]
    public string? Description { get; set; }
    public bool? HasCompleted { get; set; }
    public TaskItemStatus? Status { get; set; }

    public List<UpdateTaskRequest>? Tasks { get; set; }
}