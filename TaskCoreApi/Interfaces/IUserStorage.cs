namespace TaskCoreApi.Interfaces;

public interface IUserStorage
{
    Task<List<User>> GetAllUsersAsync();
    Task<User> GetUserByIdAsync(int userId);
}