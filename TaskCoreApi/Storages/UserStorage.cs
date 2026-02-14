namespace TaskCoreApi.Storages;

public class UserStorage : IUserStorage
{
    public int CounterId { get; set; } = 1;
    public List<User> Users { get; set; } = new();

    public Task<List<User>> GetAllUsersAsync()
        => Task.FromResult(Users);

    public Task<User?> GetUserByIdAsync(int id)
    {
        var user = Users.FirstOrDefault(x => x.Id == id);
        return Task.FromResult(user);
    }
}