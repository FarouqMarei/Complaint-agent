using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DTOs;
using WebAPI.Interfaces;
using WebAPI.Models;
using WebAPI.Utilities;
using static WebAPI.Utilities.Enum;

namespace WebAPI.Services
{
    public class UserService : IUserService
    {
        public readonly ComplaintAgentDBContext _dbContext;
        public UserService(ComplaintAgentDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ResponseResult<LoginResonse>> Register(RegisterationDto dto)
        {
            ResponseResult<LoginResonse> result = new ResponseResult<LoginResonse>();
            List<User> users = new List<User>();
            users = _dbContext.Users.ToList();
            if (String.IsNullOrEmpty(dto.Name))
            {
                result.Data = null;
                result.Errors = new List<string> { "Name is required" };
                result.Status = StatusType.Failed;
            }
            else if (String.IsNullOrEmpty(dto.Password))
            {
                result.Data = null;
                result.Errors = new List<string> { "Password is required" };
                result.Status = StatusType.Failed;
            }
            else if (dto.Type < 0)
            {
                result.Data = null;
                result.Errors = new List<string> { "Select User type" };
                result.Status = StatusType.Failed;
            }
            else if (users != null && users.Count > 0 && users.Any(u => u.Name.ToLower() == dto.Name.ToLower()))
            {
                result.Data = null;
                result.Errors = new List<string> { "Name already used" };
                result.Status = StatusType.Failed;
            }
            else
            {
                try
                {
                    var isSaved = false;
                    User user = new User();
                    string encryptedPassword = EncryptPassword(dto.Password);
                    user.Name = dto.Name;
                    user.Password = encryptedPassword;
                    user.Type = dto.Type;
                    _dbContext.Users.Add(user);
                    isSaved = await _dbContext.SaveChangesAsync() > 0;
                    if (isSaved)
                    {
                        User savedUser = _dbContext.Users.Where(u => u.Name.ToLower() == dto.Name.ToLower()).FirstOrDefault();
                        result.Data = new LoginResonse {Id = savedUser.Id, Name = savedUser.Name, Type = savedUser.Type };
                        result.Errors = null;
                        result.Status = StatusType.Success;
                    }
                    else
                    {
                        result.Data = null;
                        result.Errors = new List<string> { "Something went wrong, please contact the developer" };
                        result.Status = StatusType.Failed;
                    }
                }
                catch(Exception ex)
                {
                    result.Data = null;
                    result.Errors = new List<string> { "Something went wrong, please contact the developer" };
                    result.Status = StatusType.Failed;
                }
            }

            return result;
        }

        public async Task<ActionResult<ResponseResult<LoginResonse>>> Login(LoginDto dto)
        {
            ResponseResult<LoginResonse> result = new ResponseResult<LoginResonse>();
            List<User> users = new List<User>();
            users = _dbContext.Users.ToList();
            if (String.IsNullOrEmpty(dto.Name))
            {
                result.Data = null;
                result.Errors = new List<string> { "Name is required" };
                result.Status = StatusType.Failed;
            }
            else if (String.IsNullOrEmpty(dto.Password))
            {
                result.Data = null;
                result.Errors = new List<string> { "Password is required" };
                result.Status = StatusType.Failed;
            }
            else if (users == null || users.Count < 1 || users.Any(u => u.Name.ToLower() == dto.Name.ToLower()) == false)
            {
                result.Data = null;
                result.Errors = new List<string> { "Name does not exist" };
                result.Status = StatusType.Failed;
            }
            else
            {
                try
                {
                    bool isValidPassword = ValidatePassword(dto.Password, users.Where(u => u.Name.ToLower() == dto.Name.ToLower()).FirstOrDefault());
                    if (isValidPassword)
                    {
                        User loggedInUser = _dbContext.Users.Where(u => u.Name.ToLower() == dto.Name.ToLower()).FirstOrDefault();
                        result.Data = new LoginResonse { Id = loggedInUser.Id, Name = loggedInUser.Name, Type = loggedInUser.Type };
                        result.Errors = null;
                        result.Status = StatusType.Success;
                    }
                    else
                    {
                        result.Data = null;
                        result.Errors = new List<string> { "Incorrect password" };
                        result.Status = StatusType.Failed;
                    }
                }
                catch (Exception ex)
                {
                    result.Data = null;
                    result.Errors = new List<string> { "Something went wrong, please contact the developer" };
                    result.Status = StatusType.Failed;
                }
            }

            return result;
        }

        private bool ValidatePassword(string password, User user)
        {
            string savedPasswordHash = user.Password;
            byte[] hashBytes = Convert.FromBase64String(savedPasswordHash);
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);

            var pdkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pdkdf2.GetBytes(20);

            int ok = 1;
            for (int i = 0; i < 20; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                    ok = 0;
            }
            if (ok == 1)
            {
                return true;
            }

            return false;
        }

        private string EncryptPassword(string password)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            var pdk = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pdk.GetBytes(20);
            byte[] hashBytes = new byte[36];

            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            string savedPasswordHash = Convert.ToBase64String(hashBytes);

            return savedPasswordHash;
        }
    }
}
