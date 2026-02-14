using TaskCoreApi.Dto.TaskItemDto;

namespace TaskCoreApi.Dto.ProjectDto;

public class ProjectResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime Created { get; set; }
    public bool HasCompleted { get; set; }
    
    public List<TaskItemResponse> Tasks { get; set; }
}