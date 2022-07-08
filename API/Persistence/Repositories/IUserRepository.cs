using Domain;

namespace Persistence.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> SelectAllColumnsOfUserAsync();
        Task<List<User>> SelectCustomColumnsOfUserAsync(string[]? columnNames);
    }
}