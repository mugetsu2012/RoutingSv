using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using RuteoPedidos.WebAdmin.DTO.Input;
using RuteoPedidos.WebAdmin.DTO.Output;

namespace RuteoPedidos.WebAdmin.ApplicationService
{

    public interface IUserService
    {
        string GenerarToken(string idUsuario, string nombreUsuario, int idCuenta, DateTime fechaExpiracion);
    }

    public class UserService: IUserService
    {
        private readonly string _secretKey;

        public UserService(string secretKey)
        {
            _secretKey = secretKey;
        }

        public string GenerarToken(string idUsuario, string nombreUsuario, int idCuenta, DateTime fechaExpiracion)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] llave = Encoding.ASCII.GetBytes(_secretKey);

            IdentityModelEventSource.ShowPII = true;

            //Creamos un descriptor
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, idUsuario),
                    new Claim(ClaimTypes.GivenName, nombreUsuario),
                    new Claim("IdCuenta", idCuenta.ToString()) 
                }),
                Expires = fechaExpiracion,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(llave), SecurityAlgorithms.HmacSha256Signature)
            };

            //Creamos el token
            SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor);

            string token = tokenHandler.WriteToken(securityToken);
            return token;
        }
    }
}
