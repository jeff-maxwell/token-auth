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

            await _userService.Create(new SignupDto { Username="john", FirstName = "John", LastName="Doe", Email="john.doe@test.com", Password="test" });
            await _userService.Create(new SignupDto { Username="jane", FirstName = "Jane", LastName="Doe", Email="jane.doe@test.com", Password="test" });
            await _userService.Create(new SignupDto { Username="tom", FirstName = "Tom", LastName="Jones", Email="tom.jones@test.com", Password="test" });
        
            context.SaveChanges();
        }
    }
}