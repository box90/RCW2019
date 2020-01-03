using Microsoft.IdentityModel.Tokens;
using RCW2019.Common;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RWC2019.BL
{
    public static class AuthManager
    {
        public static string GetToken(User u)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Const.GetKey());
            var tokenDescriptor = new SecurityTokenDescriptor
            {

                Issuer = Const.GetIssuer(),
                Audience = Const.GetIssuer(),
                IssuedAt = DateTime.UtcNow,
                NotBefore = DateTime.UtcNow,
                Subject = new ClaimsIdentity(new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, u.UserName),
                    new Claim(JwtRegisteredClaimNames.Email, $"{u.UserName}@mail.com"),
                    new Claim(ClaimTypes.Name, u.UserName),
                    new Claim(JwtRegisteredClaimNames.Birthdate, "2019-01-01"),
                    new Claim(JwtRegisteredClaimNames.Jti, "000000")
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }

    }
}
