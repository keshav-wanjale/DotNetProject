using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApiExample.DataStore.Models;
using WebApiExample.WebApp.Data;

namespace WebApiExample.WebApp.Services
{
    public interface IUserService
    {
        Task<User> AddUserAsync(string name, int age);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserAsync(Guid id);
        Task RemoveAsync(Guid id);
    }

    public class UserService : IUserService
    {
        private readonly UserContext _context;
        private readonly IDictionary<string, string> _bannedNames;

        public UserService(UserContext context)
        {
            _context = context;
            _bannedNames = new Dictionary<string, string>
            {
                ["admin"] = "admin",
                ["sa"] = "sa",
            };
        }

        public async Task<User> AddUserAsync(string name, int age)
        {
            if (_bannedNames.ContainsKey(name))
                throw new ArgumentException($"The name {name} is not allowed");

            var user = new User
            {
                Name = name,
                Age = age,
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.AsNoTracking().ToListAsync();
        }

        public async Task<User> GetUserAsync(Guid id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task RemoveAsync(Guid id)
        {
            var existing = await _context.Users.FindAsync(id);
            if (existing == null)
                return;

            _context.Users.Remove(existing);
            await _context.SaveChangesAsync();
        }
    }
}