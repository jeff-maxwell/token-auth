using System.ComponentModel.DataAnnotations;

namespace dotnetcore.Models
{
    public class User
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string RandomKey { get; set; }
        public string Token { get; set; }
    }
}