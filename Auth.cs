using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWT
{
    public class Auth : IAuth
    {
        private readonly string username = "USTGlobal";
        private readonly string password = "pwd";
        private readonly string[] role = { "admin", "user" };
        private readonly string key;
        public Auth(string key)
        {
            this.key = "This_is_my_first_Test_Key_for_jwt_token";
        }
        public string Authentication(string username, string password)
        {
            
            if (!(username.Equals(username) || password.Equals(password)))
            {
                return null;
            }
            // 1. Create Security Token Handler
            var tokenHandler = new JwtSecurityTokenHandler();

            // 2. Create Private Key to Encrypted
            var tokenKey = Encoding.ASCII.GetBytes(key);
            //3. Create JWTdescriptor
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.Name, username),
                        new Claim(ClaimTypes.Role,role[0])
                    }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            //4. Create Token
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // 5. Return Token from method
            return tokenHandler.WriteToken(token);

        }
    }
}

/*JwtSecurityTokenHandler:
System.IdentityModel.Tokens.Jwt
 A SecurityTokenHandler designed for creating and validating Json Web Tokens.

SecurityTokenDescriptor:Microsoft.IdentityModel.Tokens
Contains some information which used to create a security token 

ClaimsIdentity:
 used to make authorization and authentication decisions
describe the entity that the corresponding identity represents

claim:
claims are simply statements (for example, name, identity, group), made about users, 
that are used primarily for authorizing access to claims-based applications located anywhere on the Internet
 
 Symmetric Key:
 The Symmetric Key is used both for signing and validation. For example, 
let's say John wants to share a secret with Jane, when the secret is told, 
John also tells Jane a password - the key - in order for the secret to being understood. 
In this way, John - the identity provider or the service - ensures that his secret is well 
kept by using the given password.*/