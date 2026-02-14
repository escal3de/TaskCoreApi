namespace TaskCoreApi.Models;

public class TaskItem
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime Created { get; set; }
    public TaskItemStatus Status { get; set; }
    
    public List<string> Comments { get; set; }
    public List<Tag> Tags { get; set; }
}