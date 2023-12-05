using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Microsoft.IdentityModel.Tokens;

namespace API.Services
{
    public class TokenService
    {
        public string CreateToken(AppUser appUser){
            var claims = new List<Claim>{
                new Claim(ClaimTypes.Name , appUser.DisplayName),
                new Claim(ClaimTypes.NameIdentifier , appUser.Id),
                new Claim(ClaimTypes.Email , appUser.Email),

            };

            var  key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("x77qwsLXq9yKxJGLbF7ZnESXZ4AwsGXwmzugaPwx5VmFanAew6aH7EXRKFDGnLaE"));
            var creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor{
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);

        }
    }
}