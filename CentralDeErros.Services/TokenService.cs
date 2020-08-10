using CentralDeErros.Core.Extensions;
using CentralDeErros.Model.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CentralDeErros.Services
{
    public class TokenService
    {
        private readonly AppSettingsJWT _appSettingsJWT;

        public TokenService(IOptions<AppSettingsJWT> appSettingsJWT)
        {
            _appSettingsJWT = appSettingsJWT.Value;
        }

        public string GenerateToken(Microsservice microsservice)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_appSettingsJWT.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, microsservice.Name.ToString())
                }),
                Issuer = _appSettingsJWT.Emissor,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);

        }

    }
}
