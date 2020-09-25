using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebAPI.DTOs;
using WebAPI.Interfaces;
using WebAPI.Utilities;
using static WebAPI.Utilities.Enum;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        public readonly IUserService _repo;
        private IConfiguration _config;
        public LoginController(IUserService repo, IConfiguration config)
        {
            _repo = repo;
            _config = config;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<ResponseResult<LoginResonse>>> Register([FromBody] RegisterationDto dto)
        {
            return await _repo.Register(dto);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<ResponseResult<LoginResonse>>> Login([FromBody] LoginDto dto)
        {
            //return await _repo.Login(dto);
            ResponseResult<LoginResonse> response = new ResponseResult<LoginResonse>();
            response.Data = new LoginResonse();
            var user = await _repo.Login(dto);

            if (user != null && user.Value.Status == StatusType.Success)
            {
                var tokenString = GenerateJSONWebToken(dto);
                response.Status = StatusType.Success;
                response.Data.Id = user.Value.Data.Id;
                response.Data.Name = user.Value.Data.Name;
                response.Data.Type = user.Value.Data.Type;
                response.Data.Token = tokenString;
            }
            else
            {
                response.Status = StatusType.Failed;
                response.Data = user.Value.Data;
                response.Errors = user.Value.Errors;
            }

            return response;
        }

        private string GenerateJSONWebToken(LoginDto dto)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
              null,
              expires: DateTime.Now.AddDays(1),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}