using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JWTSample
{
    internal class UserService
    {
        private string _SecretKey = "*(&hbHBBHjjhkk%^%^&&";
        private IDictionary<string, string> _users = new Dictionary<string, string>()
        {
            {"root1", "test"},
            {"root2", "test"},
            {"root3", "test"},
            {"root4", "test"}
        };

        public string Authentificate(string username, string password)
        {
            if(string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return string.Empty;
            }

            int i = 0;
            foreach(KeyValuePair<string, string> pair in _users)
            {
                if( string.CompareOrdinal(pair.Key, username) == 0 &&
                    string.CompareOrdinal(pair.Value, password) == 0) 
                {
                    return CreateSessionToken(i);
                }
                i++;
            }
            return string.Empty;
        }

        private string CreateSessionToken(int id)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(_SecretKey);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]{
                        new Claim(ClaimTypes.NameIdentifier, id.ToString())
                    }),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), 
                SecurityAlgorithms.HmacSha256Signature)
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
