using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence.Extensions;

namespace Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        protected DatabaseContext Database { get; }

        public UserRepository(DatabaseContext database)
        {
            Database = database;
        }

        public async Task<List<User>> SelectAllColumnsOfUserAsync()
        {
            var query = 
                Database.Users
                .AsNoTracking()
                .AsQueryable();

            Console.WriteLine("Executed query: =====> " + query.ToQueryString());
            var result = await query.ToListAsync();           
            return result;
        }

        public async Task<List<User>> SelectCustomColumnsOfUserAsync(string[]? columnNames)
        {
            var query = 
                Database.Users
                .AsNoTracking()
                .AsQueryable();

            if(columnNames?.Length > 0)
            {
                query = 
                    query.SelectCustomColumns(columnNames);
            }
            
            Console.WriteLine("Executed query: =====> " + query.ToQueryString());
            var result = await query.ToListAsync();
            return result;
        }

    }
}