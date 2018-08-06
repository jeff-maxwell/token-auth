using System.Threading.Tasks;
using dotnetcore.Models;
using dotnetcore.Services;

namespace dotnetcore.Helpers
{
    public class TestData
    {   
        public async Task AddTestUserData(DataContext context)
        {
            context.Database.EnsureCreated();

            UserService _userService = new UserService(context);

            await _userService.Create(new SignupDto { Username="john", Name="John Doe", Email="john.doe@test.com", Password="test" });
            await _userService.Create(new SignupDto { Username="jane", Name="Jane Doe", Email="jane.doe@test.com", Password="test" });
            await _userService.Create(new SignupDto { Username="tom", Name="Tom Jones", Email="tom.jones@test.com", Password="test" });
        
            context.SaveChanges();
        }
    }
}