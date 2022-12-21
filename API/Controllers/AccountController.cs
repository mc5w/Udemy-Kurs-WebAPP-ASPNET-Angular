using System.Runtime.InteropServices.JavaScript;
using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController: BaseApiController
    {
        private readonly DataContext context;
        private readonly ITokenService tokenService;

        public AccountController(DataContext context, ITokenService tokenService)
        {
            this.context = context;
            this.tokenService = tokenService;
        }

        [HttpPost("register")] // POST: api/account/register
        public async Task<ActionResult<AppUser>>Register(RegisterDto dto){

            if(await UserExists(dto.userName)){
                return BadRequest("Username is already taken");
            }
            using var hmac = new HMACSHA512();

            var user = new AppUser{
                UserName = dto.userName.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.passWord)),
                PasswordSalt = hmac.Key
            };

            context.Users.Add(user);
            await context.SaveChangesAsync();
            return user;
        }

        private async Task<bool> UserExists(string userName){
            return await context.Users.AnyAsync(x => x.UserName == userName.ToLower());
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto dto){
            var user = await context.Users.FirstOrDefaultAsync(x => x.UserName == dto.UserName.ToLower());
            if(user == null) return Unauthorized("invalid Username");

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.PassWord));
            for (int i = 0; i < computedHash.Length; i++)
            {
                if(computedHash[i] != user.PasswordHash[i])
                    return Unauthorized("invalid Password");
            }

            return new UserDto{
                username = user.UserName,
                token = tokenService.CreateToken(user)
            };
        }
    }
}