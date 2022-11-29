using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InGame.Business.Concrete.DTO.Concrete;
using InGame.Business.Concrete.DTO.Concrete.User;

namespace InGame.Business.Interface
{
    public interface IUserService
    {
        Task<ServiceResult> RegisterUserAsync(UserRegisterDto userRegisterDto);
    }
}
