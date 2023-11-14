using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using usipav_cockpit.Application.Interfaces;
using usipav_cockpit.Domain.Entities;

namespace usipav_cockpit.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IConfiguration _configuration;
        public AuthenticationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool ValidateToken(string token)
        {
            try
            {
                var keyJwt = _configuration.GetSection("Keys").GetValue<string>("KEY_JWT");

                if (string.IsNullOrWhiteSpace(keyJwt))
                {
                    return false;
                }

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(keyJwt);

                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

                SecurityToken validatedToken;
                ClaimsPrincipal principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out validatedToken);

                return true;
            }
            catch (SecurityTokenException)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public async Task<string> ValidateUserPasswordAsync(User user, string password)
        {
            bool passwordMatches = false;

            if (!string.IsNullOrWhiteSpace(user.Password) && BCrypt.Net.BCrypt.Verify(password, user.Password))
                passwordMatches = true;

            if (!passwordMatches)
                return string.Empty;

            return await GenerateUserJWTAsync(user);
        }

        public async Task<string> GenerateUserJWTAsync(User user)
        {
            object? keyJWT;

            try
            {
                keyJWT = _configuration.GetSection("Keys").GetValue<string>("KEY_JWT") ?? string.Empty;

                if (keyJWT == null)
                    throw new Exception("The JWT encryption key dont exists in the Vault.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Cant connect to Vault Server: {ex.Message}");
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, user.Email ?? string.Empty)
            };

            var bytesKeyJWT = Encoding.UTF8.GetBytes(keyJWT?.ToString() ?? string.Empty);
            var symmetricSecurityKey = new SymmetricSecurityKey(bytesKeyJWT);

            var credentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: credentials,
                expires: DateTime.Now.AddHours(1));

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        public string EncryptUserPassword(string password)
        {
            var salt = BCrypt.Net.BCrypt.GenerateSalt();
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);

            return hashedPassword;
        }
    }
}
