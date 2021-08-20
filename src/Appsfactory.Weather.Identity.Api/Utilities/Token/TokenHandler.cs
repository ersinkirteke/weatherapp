using Appsfactory.Weather.Identity.Api.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Appsfactory.Weather.Identity.Api.Utilities.Token
{
    public static class TokenHandler
    {
        public static JWTOptions _jwtOptions;
        public static UserOptions _userOptions;
        public static dynamic CreateAccessToken()
        {
            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtOptions.Security));
            DateTime now = DateTime.UtcNow;
            JwtSecurityToken jwt = new JwtSecurityToken(
                     issuer: _jwtOptions.Issuer,
                     audience: _jwtOptions.Audience,
                     claims: new List<Claim> {
                      new Claim(ClaimTypes.Name, _userOptions.UserName)
                     },
                     notBefore: now,
                     expires: now.Add(TimeSpan.FromMinutes(60)),
                     signingCredentials: new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256)
                 );

            return new
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(jwt),
                Expires = TimeSpan.FromMinutes(60).TotalSeconds
            };
        }
    }
}
