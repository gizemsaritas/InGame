using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InGame.Business.Concrete.DTO.Concrete.JWT;
using InGame.Business.Concrete.DTO.Concrete.User;

namespace InGame.Business.Tools.JWT.Interface
{
    public interface IJwtService
    {
        JwtToken GenerateJwt(UserLoginDto userLoginDto);
    }
}
