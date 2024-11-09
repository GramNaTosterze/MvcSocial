using System.Globalization;

namespace MvcSocial.Models;

public class UserRepository: IUserRepository
{
    private readonly List<User> _users = [new User
    {
        Login = "admin",
        DateOfCreation = DateTime.Now.ToString(CultureInfo.CurrentCulture),
    }];

    static UserRepository() {}

    private UserRepository() {}

    public static UserRepository Instance { get; } = new UserRepository();

    public async Task<List<string>> GetUserNames()
    {
        return await Task.FromResult(_users.Select(x => x.Login).ToList());
    }

    public List<User> Users => _users;
}