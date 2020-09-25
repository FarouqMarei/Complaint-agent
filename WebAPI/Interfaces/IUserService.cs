using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DTOs;
using WebAPI.Utilities;

namespace WebAPI.Interfaces
{
    public interface IUserService
    {
        Task<ResponseResult<LoginResonse>> Register(RegisterationDto dto);
        Task<ActionResult<ResponseResult<LoginResonse>>> Login(LoginDto dto);
    }
}
