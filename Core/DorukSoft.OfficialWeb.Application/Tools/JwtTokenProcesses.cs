using DorukSoft.OfficialWeb.Application.DTOs;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DorukSoft.OfficialWeb.Application.Tools
{
    public class JwtTokenProcesses
    {
        public static AppUserLoginDTO GenerateToken(AppUserLoginDTO dto)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Role, dto.AppRole!.Definition ?? ""));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, dto.AppUserId.ToString()));
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtTokenDefaults.Key));
            var signinCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expireDate = DateTime.UtcNow.AddDays(JwtTokenDefaults.Expire);
            JwtSecurityToken token = new JwtSecurityToken(issuer: JwtTokenDefaults.ValidIssuer, audience: JwtTokenDefaults.ValidAudience, claims: claims, notBefore: DateTime.UtcNow, expires: expireDate, signingCredentials: signinCredentials);
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            dto.Token = tokenHandler.WriteToken(token);
            dto.TokenExpireDateTime = expireDate;
            return dto;
        }
    }
}
