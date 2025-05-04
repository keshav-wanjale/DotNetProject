using Microsoft.EntityFrameworkCore;
using WebApiExample.DataStore.Models;

namespace WebApiExample.WebApp.Data
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        // TODO: Add DbSet for other entities
    }
}