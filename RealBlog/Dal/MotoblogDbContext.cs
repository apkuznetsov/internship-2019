using System.Data.Entity;
using RealBlog.Models;

namespace RealBlog.Dal
{
    public class MotoblogDbContext : DbContext
    {
        static MotoblogDbContext()
        {
            Database.SetInitializer(new MotoblogDbInitializer());
        }

        public MotoblogDbContext() : base("MotoblogDatabase") { }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
    }
}