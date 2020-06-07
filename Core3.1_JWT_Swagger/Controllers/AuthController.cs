using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;
using Microsoft.IdentityModel.Tokens;

namespace Core3._1_JWT_Swagger.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public IActionResult GetToken(string name, string password)
        {
            if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(password))
            {
                string iss = Appsettings.app(new string[] { "Audience", "Issuer" });
                string aud = Appsettings.app(new string[] { "Audience", "Audience" });
                string secret = Appsettings.app(new string[] { "Audience", "Secret" });
                List<Claim> claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Nbf,$"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}"),//生效时间
                    new Claim(JwtRegisteredClaimNames.Iat,$"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}"),//签发时间
                    new Claim(JwtRegisteredClaimNames.Exp,$"{new DateTimeOffset(DateTime.Now.AddMinutes(5)).ToUnixTimeSeconds()}"),//过期时间
                    new Claim(ClaimTypes.Name,name)
                };
                SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
                SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                JwtSecurityToken token = new JwtSecurityToken(
                    issuer: iss,
                    audience: aud,
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: creds
                    );
                var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
                Console.WriteLine(jwtToken);
                return Ok(new { Token = jwtToken });
            }
            else
            {
                return BadRequest(new { Message = "name or password is incorrect." });
            }
        }
    }
}
