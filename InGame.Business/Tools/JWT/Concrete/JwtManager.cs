using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using InGame.Business.Concrete.DTO.Concrete.JWT;
using InGame.Business.Concrete.DTO.Concrete.User;
using InGame.Business.Tools.JWT.Interface;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace InGame.Business.Tools.JWT.Concrete
{
    public class JwtManager : IJwtService
    {
        private readonly IOptions<JwtInfo> _optionsJwt;
        public JwtManager(IOptions<JwtInfo> optionsJwt)
        {
            _optionsJwt = optionsJwt;
        }
        public JwtToken GenerateJwt(UserLoginDto userLoginDto)
        {
            var jwtInfo = _optionsJwt.Value;
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtInfo.SecurityKey));

            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(issuer: jwtInfo.Issuer, audience: jwtInfo.Audience, claims: SetClaims(userLoginDto), notBefore: DateTime.Now, expires: DateTime.Now.AddMinutes(jwtInfo.Expires), signingCredentials: signingCredentials);

            JwtToken jwtToken = new JwtToken();
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            jwtToken.Token = handler.WriteToken(jwtSecurityToken);
            return jwtToken;
        }

        private List<Claim> SetClaims(UserLoginDto userLoginDto)
        {
            List<Claim> claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.Email, userLoginDto.Email));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, userLoginDto.Id.ToString()));
            return claims;
        }
    }
}
