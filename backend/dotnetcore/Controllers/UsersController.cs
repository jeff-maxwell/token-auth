using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using dotnetcore.Interfaces;
using dotnetcore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace dotnetcore.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private IUserService _userService;
        private IConfiguration _config;

        public UsersController(IUserService userService, IConfiguration config)
        {
            _userService = userService;
            _config = config;
        }

        [HttpGet]
        public async Task<IEnumerable<User>> Get()
        {
            return await _userService.GetAll();
        }

        [AllowAnonymous]
        [HttpPost("forgot")]
        public async Task<IActionResult> Forgot([FromBody]string email)
        {
            // Send an email with the following text.
            var randomKey = await _userService.SetRandomKey(email);

            var message = $"A request has been made to reset your password on site X.  "
                        + "If you want to reset your password click the link below and "
                        + "enter a new password:"
                        + "http://localhost:4200/auth/newpassword/{randomKey}";

            return Ok(message);
        }

        [AllowAnonymous]
        [HttpPost("reset")]
        public async Task<IActionResult> Reset([FromBody]ResetPasswordDto resetPasswordDto)
        {
            var result = await _userService.Reset(resetPasswordDto);

            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]UserDto userDto)
        {
            try
            {
                var user = await _userService.Authenticate(userDto.Email, userDto.Password);

                if (user == null)
                    return Unauthorized();

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_config.GetSection("AppSettings:Secret").Value);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[] 
                    {
                        new Claim(ClaimTypes.Name, user.Id)
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                // return basic user info (without password) and token to store client side
                return Ok(new {
                    Id = user.Id,
                    Username = user.Username,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Token = tokenString
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody]SignupDto user)
        {
            try
            {
                if (user.Password != user.ConfirmPassword)
                    return BadRequest("Passwords do not match");

                var newUser = await _userService.Create(user);
                return Ok(newUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
