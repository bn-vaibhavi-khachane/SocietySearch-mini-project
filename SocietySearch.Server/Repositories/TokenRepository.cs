using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SocietySearch.Server.Repositories
{
    public class TokenRepository
    {
        private readonly IConfiguration configuration;

        public TokenRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string GetToken(Manager manager, IList<string> Roles)
        {
           var claims = new List<Claim> {
               new Claim(
                   ClaimTypes.NameIdentifier,
                   manager.Id
                   ),
               new Claim(
                   ClaimTypes.Email,
                   manager.Email
                   ),
               new Claim(
                   "Name",
                   manager.Name
                   ),
               new Claim(
                   ClaimTypes.MobilePhone,
                   manager.PhoneNumber
                   )
           };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    configuration["Jwt:Key"]!
                    )
                );
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials                
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
