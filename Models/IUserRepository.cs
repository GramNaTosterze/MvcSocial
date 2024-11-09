namespace MvcSocial.Models;

public interface IUserRepository
{
    Task<List<string>> GetUserNames();
    List<User> Users { get; }
}