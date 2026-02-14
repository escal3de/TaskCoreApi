namespace TaskCoreApi.Storages;

public class ProjectStorage : IProjectStorage
{
    public int CounterId = 1;
    public List<Project> Projects { get; set; } = new();

    public async Task<List<Project>> GetAllProjectsAsync()
        => await Task.FromResult(Projects);
    

    public Task<Project?> GetProjectByIdAsync(int id)
    {
        var project = Projects.FirstOrDefault(x => x.Id == id);
        return Task.FromResult(project);
    }

    public Task<List<Project>> GetAllProjectsByIdsAsync(List<int> projectIds)
    {
        var projects = Projects.Where(x => projectIds.Contains(x.Id)).ToList();
        return Task.FromResult(projects);
    }
}