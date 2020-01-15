using RealBlog.Models;
using System.Data.Entity;

namespace RealBlog.Dal
{
    public class MotoblogDbInitializer : DropCreateDatabaseIfModelChanges<MotoblogDbContext>
    {
        protected override void Seed(MotoblogDbContext context)
        {
            User defaultUser = new User
            {
                Login = "test",
                Password = "asdasd",
                Email = "test@test.ru"
            };

            context.Users.Add(defaultUser);
            context.SaveChanges();
        }
    }
}