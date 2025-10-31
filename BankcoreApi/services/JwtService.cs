using BankCoreApi.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BankCoreApi.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _config;

        public JwtService(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateToken(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            // ✅ Read JWT settings safely
            var jwtSection = _config.GetSection("Jwt");
            var keyString = jwtSection["Key"] ?? throw new Exception("JWT Key not found in configuration.");
            var issuer = jwtSection["Issuer"];
            var audience = jwtSection["Audience"];
            var expiresInMinutes = int.TryParse(jwtSection["ExpiresInMinutes"], out var minutes) ? minutes : 60;

            // ✅ Build user claims
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
                new Claim("UserId", user.Id.ToString()),
                new Claim("Username", user.Username ?? string.Empty),
                new Claim("FullName", $"{user.FirstName} {user.LastName}".Trim())
            };

            // ✅ Add role claims (if available)
            if (user.Roles != null)
            {
                foreach (var role in user.Roles)
                {
                    if (!string.IsNullOrWhiteSpace(role.Name))
                        claims.Add(new Claim(ClaimTypes.Role, role.Name));
                }
            }

            // ✅ Generate the token
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expiresInMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
