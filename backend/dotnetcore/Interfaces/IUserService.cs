using System.Collections.Generic;
using System.Threading.Tasks;
using dotnetcore.Models;

namespace dotnetcore.Interfaces
{
    public interface IUserService
    {
        Task<User> Authenticate(string email, string password);
        Task<IEnumerable<User>> GetAll();
        Task<User> GetById(string userId);
        Task<User> Create(SignupDto user);
        Task<string> SetRandomKey(string email);
        Task<bool> Reset(ResetPasswordDto resetPasswordDto);

    }
}