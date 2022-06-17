using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Core.Entities.Concrete;
using Core.Extensions;
using Core.Utilities.Security.Encryption;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Core.Utilities.Security.JWT
{
    public class JwtHelper : ITokenHelper
    {
        public IConfiguration Configuration { get; } // appsetting dosyasını okumaya yarıyor
        private TokenOptions _tokenOptions; // Okuduğumuz dosyayın token optionsa atıyoruz
        private DateTime _accessTokenExpiration; // Token ne zaman geçersiz olacak
        public JwtHelper(IConfiguration configuration)
        {
            Configuration = configuration;
            _tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();
            //Configuration = appsettings , Tokenoptions sectionumu getir ve benim tokenoptions dosyamla maple(tek tek atama yapıyor)
        }
        
        //Kullanıcı için token üretimi 
        public AccessToken CreateToken(User user, List<OperationClaim> operationClaims)
        {   //appsettingten aldığı 10dkyı geçerlilik süresine ekliyor 
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
            //Benim oluşturduğum security keyi oluşturma metodumu kullanabilirsin 
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
            //Algoritmayı ve kullanacağım anahtarı belirtiyorum
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
            //token'ımı oluşturabilirim şimdi bilgileri kullanarak
            var jwt = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials, operationClaims);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);

            return new AccessToken
            {
                Token = token,
                Expiration = _accessTokenExpiration
            };

        }

        public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, User user,
            SigningCredentials signingCredentials, List<OperationClaim> operationClaims)
        {
            var jwt = new JwtSecurityToken(
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                expires: _accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: SetClaims(user, operationClaims),
                signingCredentials: signingCredentials
            );
            return jwt;
        }


        
        private IEnumerable<Claim> SetClaims(User user, List<OperationClaim> operationClaims)
        {
            var claims = new List<Claim>();
            //kullanıcı id
            claims.AddNameIdentifier(user.Id.ToString());
            //kullanıcı maili
            claims.AddEmail(user.Email);
            //kullanıcı ismi-soyismi
            claims.AddName($"{user.FirstName} {user.LastName}");
            //kullanıcı rolü
            claims.AddRoles(operationClaims.Select(c => c.Name).ToArray());

            return claims;
        }
    }
}
