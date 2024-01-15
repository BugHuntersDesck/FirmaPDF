// <copyright file="CNLogin.cs" company="UNIVALLE">
// Esta clase está restringida para su uso, sin la previa autorización del departamento de NETValle.
// </copyright>
// <author>DenilsonMamani</author>

/// <summary>
/// Nombre de la aplicación: SWClassClock
/// Nombre del desarrollador: Denilson Gabriel Mamani Cochi
/// Fecha de creación: 30/12/23
/// </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
/*
using WALoginEmpleados.SWInformacionUsuarioValidada;

using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;*/

namespace WALoginEmpleados.App.Controladoras.Autenticacion
{
    public class ManejadorTokens
    {/*
        #region Atributos
        #endregion

        #region Constructor
        #endregion

     
        #region Propiedades
        public string GenerarTokenJWT(UsuarioExtendido usuario, string tipoUsuario)
        {
            var secretKeyString = "clave_secreta_muy_larga_y_segura";
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKeyString));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            // Definir emisor y audiencia según las URLs de tus aplicaciones
            var issuer = "http://localhost:62081";
            var audience = "https://localhost:44344";

            var tokenOptions = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: new List<Claim>
                {
                    new Claim(ClaimTypes.Name, usuario.nombre),
                    new Claim("TipoUsuarioId", usuario.iDUsuario.ToString()),
                    new Claim("TipoUsuario", tipoUsuario),
                    // Otros claims según sea necesario
                },
                expires: DateTime.Now.AddMinutes(3), // Tiempo de expiración del token
                signingCredentials: signinCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return tokenString;
        }

        public bool ValidarToken(string token, out string TipoUsuario, out string IdUsuario)
        {
            var secretKeyString = "clave_secreta_muy_larga_y_segura"; 
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKeyString));

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ValidateIssuer = true,
                ValidIssuer = "http://localhost:62081",
                ValidateAudience = true,
                ValidAudience = "https://localhost:44344", 
                ValidateLifetime = true, 
                ClockSkew = TimeSpan.Zero 
            };

            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);

                //tokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);

                TipoUsuario = principal.Claims.FirstOrDefault(c => c.Type == "TipoUsuario")?.Value;
                IdUsuario = principal.Claims.FirstOrDefault(c => c.Type == "TipoUsuarioId")?.Value;
                
                return validatedToken != null;
            }
            catch
            {
                TipoUsuario = null;
                IdUsuario = null;
                return false;
            }
        }

        public string GenerarTokenDocumento(EFirmaDocumento documento, string claveDocumento)
        {
            var claveSecreta = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(claveDocumento));
            var credencialesFirma = new SigningCredentials(claveSecreta, SecurityAlgorithms.HmacSha256);

            var emisor = "http://localhost:62081";
            var audiencia = "https://localhost:44344";

            var opcionesToken = new JwtSecurityToken(
                issuer: emisor,
                audience: audiencia,
                claims: new List<Claim>
                {
            new Claim("DocumentoID", documento.DocumentoID.ToString()),
            new Claim("UsuarioID", documento.UsuarioID.ToString()),
                },
                expires: DateTime.Now.AddHours(24),
                signingCredentials: credencialesFirma
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(opcionesToken);
            return tokenString;
        }

        public bool ValidarTokenDocumento(string token, out int DocumentoID, out DateTime FechaHoraFirma, string claveDocumento)
        {
            var claveSecretaString = claveDocumento;
            var clave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(claveSecretaString));

            var parametrosValidacionToken = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = clave,
                ValidateIssuer = true,
                ValidIssuer = "http://localhost:62081",
                ValidateAudience = true,
                ValidAudience = "https://localhost:44344",
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            try
            {
                var manejadorToken = new JwtSecurityTokenHandler();
                var principal = manejadorToken.ValidateToken(token, parametrosValidacionToken, out var tokenValidado);

                DocumentoID = int.Parse(principal.Claims.FirstOrDefault(c => c.Type == "DocumentoID")?.Value);
                FechaHoraFirma = DateTime.Parse(principal.Claims.FirstOrDefault(c => c.Type == "FechaHoraFirma")?.Value);

                return tokenValidado != null;
            }
            catch
            {
                DocumentoID = 0;
                FechaHoraFirma = DateTime.MinValue;
                return false;
            }
        }


        #endregion
        */
    }
}
