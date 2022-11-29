using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using InGame.Business.Concrete.DTO.Concrete;
using InGame.Business.Concrete.DTO.Concrete.User;
using InGame.Business.Concrete.Enum;
using InGame.Business.Interface;
using InGame.Business.Tools.JWT.Interface;
using Microsoft.AspNetCore.Identity;

namespace InGame.Business.Concrete.Manager
{
    public class UserManager:IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IJwtService _jwtManager;

        public UserManager(UserManager<IdentityUser> userManager, IJwtService jwtManager)
        {
            _userManager = userManager;
            _jwtManager = jwtManager;
        }
        public async Task<ServiceResult> RegisterUserAsync(UserRegisterDto userRegister)
        {

            ServiceResult serviceResult = new ServiceResult(ServiceResultType.Notknown);

            try
            {
                if (userRegister == null) throw new NullReferenceException();

                if (!userRegister.Password.Equals(userRegister.ConfirmPassword))
                {
                    serviceResult.ServiceResultType = ServiceResultType.Error;
                    serviceResult.Message = "Confirm password and password not match";
                    return serviceResult;
                }

                var newUser = new IdentityUser
                {
                    Email = userRegister.Email,
                    UserName = userRegister.Email
                };
                var result = await _userManager.CreateAsync(newUser, userRegister.Password);
                if (result.Succeeded)
                {
                    serviceResult.ServiceResultType = ServiceResultType.Success;
                    serviceResult.Message = "User created";
                    return serviceResult;
                }

                serviceResult.ServiceResultType= ServiceResultType.Error;
                serviceResult.Message = "User didnt created";
                serviceResult.Data = result.Errors.Select(m => m.Description).FirstOrDefault();
                return serviceResult;
            }
            catch (Exception exception)
            {
                serviceResult.ServiceResultType = ServiceResultType.Error;
                serviceResult.Message = exception.Message;
            }

            return serviceResult;
            
        }

        public async Task<ServiceResult> LoginUserAsync(UserLoginDto userLoginDto)
        {
            ServiceResult serviceResult = new ServiceResult(ServiceResultType.Notknown);
            try
            {
                var user = await _userManager.FindByEmailAsync(userLoginDto.Email);
                if (user == null)
                {
                    serviceResult.ServiceResultType = ServiceResultType.Error;
                    serviceResult.Message = "Cant find user from mail";
                    return serviceResult;
                }

                var result = await _userManager.CheckPasswordAsync(user, userLoginDto.Password);

                if (!result)
                {
                    serviceResult.ServiceResultType = ServiceResultType.Error;
                    serviceResult.Message = "Invalid password";
                    return serviceResult;
                }

                serviceResult.Data=_jwtManager.GenerateJwt(userLoginDto);
                serviceResult.ServiceResultType = ServiceResultType.Success;
                return serviceResult;

            }
            catch (Exception exception)
            {
                serviceResult.ServiceResultType = ServiceResultType.Error;
                serviceResult.Message = exception.Message;
            }

            return serviceResult;
        }
    }
}
