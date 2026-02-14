namespace TaskCoreApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly ProjectStorage _projectStorage;
    private readonly TaskCounterStorage _taskCounterStorage;

    public ProjectsController(ProjectStorage projectStorage, TaskCounterStorage taskCounterStorage)
    {
        _projectStorage = projectStorage;
        _taskCounterStorage = taskCounterStorage;
    }

    [HttpGet]
    public async Task<ActionResult<List<ProjectResponse>>> Get()
    {
        var projects = await _projectStorage.GetAllProjectsAsync();

        var dto = projects.Select(project => new ProjectResponse
        {
            Id = project.Id,
            Name = project.Name,
            Description = project.Description,
            Created = project.Created == default ? DateTime.UtcNow : project.Created,
            HasCompleted = project.HasCompleted,
            Tasks = project.Tasks.Select(task => new TaskItemResponse
            {
                Id = task.Id,
                Name = task.Name,
                Description = task.Description,
                Created = task.Created == default ? DateTime.UtcNow : task.Created,
                Status = task.Status,
                Comments = task.Comments?.ToList() ?? new List<string>(),
                Tags = task.Tags?.ToList() ?? new List<Tag>()
            }).ToList()
        });

        return Ok(dto);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProjectResponse>> GetById(int id)
    {
        var project = await _projectStorage.GetProjectByIdAsync(id);

        return Ok(project);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] CreateProjectRequest projectRequest)
    {
        var project = new Project
        {
            Id = _projectStorage.CounterId++,
            Name = projectRequest.Name,
            Description = projectRequest.Description,
            Created = DateTime.UtcNow,
            HasCompleted = false,
            Tasks = projectRequest.Tasks.Select(task => new TaskItem
            {
                Id = _taskCounterStorage.TaskIdCounter++,
                Name = task.Name,
                Description = task.Description,
                Created = DateTime.UtcNow,
                Comments = task.Comments?.ToList() ?? new List<string>(),
                Tags = task.Tags?.ToList() ?? new List<Tag>()
            }).ToList()
        };

        _projectStorage.Projects.Add(project);

        return Ok(project);
    }

    [HttpPatch("{id:int}")]
    public async Task<ActionResult> Patch(int id, [FromBody] UpdateProjectRequest request)
    {
        if (request is null)
            return BadRequest("Body is null");

        var project = await _projectStorage.GetProjectByIdAsync(id);

        if (project is null)
            return NotFound($"Project with id {id} not found");

        if (request.Name is not null)
            project.Name = request.Name;
        
        if (request.Description is not null)
            project.Description = request.Description;
        
        if (request.HasCompleted.HasValue)
            project.HasCompleted = request.HasCompleted.Value;

        if (request.Tasks is not null)
        {
            project.Tasks = request.Tasks.Select(task => new TaskItem
            {
                Name = task.Name,
                Description = task.Description,
                Status = task.Status ?? TaskItemStatus.InProgress,
                Comments = task.Comments?.ToList() ?? new List<string>(),
                Tags = task.Tags?.ToList() ?? new List<Tag>()
            }).ToList();
        }

        return Ok(project);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var project = await _projectStorage.GetProjectByIdAsync(id);

        if (project is null) 
            return NotFound("Project not found");
        
        _projectStorage.Projects.Remove(project);
        
        return Ok();
    }
}