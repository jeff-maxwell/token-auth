using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetcore.Helpers;
using dotnetcore.Interfaces;
using dotnetcore.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnetcore.Services
{
    public class UserService : IUserService
    {
        private DataContext _context;

        public UserService(DataContext context)
        {
            _context = context;
        }

        public async Task<User> Authenticate(string email, string password)
        {
            await CheckUsers();

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return null;

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);

            // check if username exists
            if (user == null)
                return null;

            // check if password is correct
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            // authentication successful
            return user;
        }

        public async Task<User> Create(SignupDto user)
        {
            // validation
            if (string.IsNullOrWhiteSpace(user.Password))
                throw new Exception("Password is required");

            if (await _context.Users.AnyAsync(x => x.Username == user.Username))
                throw new Exception($"Username {user.Username} is already taken");

            User newUser = new User();

            if (newUser.Id == null)
            {
                newUser.Id = Guid.NewGuid().ToString();
            }

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(user.Password, out passwordHash, out passwordSalt);

            newUser.Email = user.Email;
            newUser.Username = user.Username;
            newUser.FirstName = user.FirstName;
            newUser.LastName = user.LastName;
            newUser.PasswordHash = passwordHash;
            newUser.PasswordSalt = passwordSalt;

            _context.Users.Add(newUser);
            _context.SaveChanges();

            return newUser;
        }

        private async Task CheckUsers()
        {
            TestData data = new TestData();
            if (_context.Users.Count() == 0) 
            {
                await data.AddTestUserData(_context);
            }
        }

        public async Task<User> GetUserById(string userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public async Task<string> SetRandomKey(string email)
        {
            var randomKey = System.Guid.NewGuid().ToString();

            var user = await _context.Users
                                    .Where(x => x.Email == email)
                                    .FirstOrDefaultAsync();

            user.RandomKey = randomKey;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return randomKey;
        }

        public async Task<bool> Reset(ResetPasswordDto resetPasswordDto)
        {
            var user = await _context.Users
                                .Where(x => x.RandomKey == resetPasswordDto.RandomKey)
                                .FirstOrDefaultAsync();

            if (user == null)
                return false;

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(resetPasswordDto.Password, out passwordHash, out passwordSalt);

            user.RandomKey = "";
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<User> GetById(string userId)
        {
            await CheckUsers();
            return await _context.Users.FindAsync(userId);
        }

        // private helper methods
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
    }
}