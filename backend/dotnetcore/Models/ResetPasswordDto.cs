namespace dotnetcore.Models
{
    public class ResetPasswordDto
    {
        public string RandomKey { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}