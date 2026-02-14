namespace TaskCoreApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly UserStorage _userStorage;
    private readonly ProjectStorage _projectStorage;

    public UsersController(UserStorage userStorage, ProjectStorage projectStorage)
    {
        _userStorage = userStorage;
        _projectStorage = projectStorage;
    }

    [HttpGet]
    public async Task<ActionResult<UserResponse>> Get()
    {
        var users = await _userStorage.GetAllUsersAsync();

        var allProjectsIds = users
            .SelectMany(x => x.ProjectsId)
            .Distinct()
            .ToList();

        var projects = await _projectStorage.GetAllProjectsByIdsAsync(allProjectsIds);

        var projectDict = projects.ToDictionary(x => x.Id);

        var dto = users.Select(u => new UserResponse
        {
            Id = u.Id,
            Name = u.Name,
            Email = u.Email,

            Projects = u.ProjectsId.Where(id => projectDict
                .ContainsKey(id)).Select(id =>
            {
                var project = projectDict[id];

                return new ProjectResponse
                {
                    Id = project.Id,
                    Name = project.Name,
                    Description = project.Description,
                    Created = project.Created,
                    HasCompleted = project.HasCompleted,

                    Tasks = project.Tasks.Select(t => new TaskItemResponse
                    {
                        Id = t.Id,
                        Name = t.Name,
                        Description = t.Description,
                        Created = t.Created,
                        Comments = t.Comments?.ToList() ?? new(),
                        Tags = t.Tags?.ToList() ?? new List<Tag>()
                    }).ToList()
                };
            }).ToList()
        }).ToList();

        return Ok(dto);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<UserResponse>> GetById(int id)
    {
        var user = await _userStorage.GetUserByIdAsync(id);
        
        if (user is null) 
            return NotFound("User not found");
        
        var projects = await _projectStorage.GetAllProjectsByIdsAsync(user.ProjectsId);
        
        var projectsDict = projects.ToDictionary(x => x.Id);

        var dto = new UserResponse
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            
            Projects = user.ProjectsId.Where(id => projectsDict.ContainsKey(id))
                .Select(projectsId =>
                {
                    var project = projectsDict[projectsId];

                    return new ProjectResponse
                    {
                        Id = project.Id,
                        Name = project.Name,
                        Description = project.Description,
                        Created = project.Created,
                        HasCompleted = project.HasCompleted,
                        
                        Tasks = project.Tasks.Select(t => new TaskItemResponse
                        {
                            Id = t.Id,
                            Name = t.Name,
                            Description = t.Description,
                            Created = t.Created,
                            Comments = t.Comments,
                            Status = t.Status,
                            Tags = t.Tags?.ToList() ?? new List<Tag>()
                        }).ToList()
                    };
                }).ToList(),
        };
        
        return Ok(dto);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] CreateUserRequest request)
    {
        var user = new User
        {
            Id = _userStorage.CounterId++,
            Name = request.Name,
            Email = request.Email,
            Password = request.Password,
            ProjectsId = request.ProjectsId
        };
        
        _userStorage.Users.Add(user);
        
        return StatusCode(StatusCodes.Status201Created, user);
    }

    [HttpPatch("{id:int}")]
    public async Task<ActionResult> Patch([FromBody] UpdateUserRequest request, int id)
    {
        if (request is null)
            return BadRequest("Body is null");
        
        var user = await _userStorage.GetUserByIdAsync(id);

        if (user is null)
            return NotFound("User not found");

        if (request.Name is not null)
            user.Name = request.Name;

        if (request.Email is not null)
            user.Email = request.Email;

        if (request.Password is not null)
            user.Password = request.Password;
        
        if (request.ProjectsId is not null)
            request.ProjectsId.ForEach(projectId =>
            {
                if (!user.ProjectsId.Contains(projectId))
                    user.ProjectsId.Add(projectId);
            });
        
        return Ok(user);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var user = await _userStorage.GetUserByIdAsync(id);
        
        if (user is null) 
            return NotFound("User not found");
        
        _userStorage.Users.Remove(user);
        
        return StatusCode(StatusCodes.Status204NoContent);
    }
}