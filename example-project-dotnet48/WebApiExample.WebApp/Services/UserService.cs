using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using WebApiExample.Common.DataAccess;
using WebApiExample.DataStore.Models;

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
        private readonly IUnitOfWork _db;
        private readonly IDictionary<string, string> _bannedNames;

        public UserService(IUnitOfWork db, IOptions<BannedNamesOptions> options)
        {
            _db = db;
            _bannedNames = options.Value.BannedNames;
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

            // Use asynchronous add
            var newUser = await _db.AddAsync(user);

            // Commit changes asynchronously
            await _db.CommitAsync();

            return newUser;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            // Retrieve all users asynchronously
            var users = await _db.GetAllAsync<User>();
            return users;
        }

        public async Task<User> GetUserAsync(Guid id)
        {
            // Retrieve user by id asynchronously
            var user = await _db.GetAsync<User>(id);
            return user;
        }

        public async Task RemoveAsync(Guid id)
        {
            // Get existing user
            var existingUser = await _db.GetAsync<User>(id);
            if (existingUser == null)
                return;

            // Use asynchronous remove if available
            await _db.RemoveAsync(existingUser);

            // Commit removal asynchronously
            await _db.CommitAsync();
        }
    }
}