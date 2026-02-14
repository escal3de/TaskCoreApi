namespace TaskCoreApi.Interfaces;

public interface IProjectStorage
{
    Task<List<Project>> GetAllProjectsAsync();
    Task<Project> GetProjectByIdAsync(int id);
    Task<List<Project>> GetAllProjectsByIdsAsync(List<int> projectIds);
}