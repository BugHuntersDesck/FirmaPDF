using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
namespace WAFirmasGestion.App_Code.Controladoras
{
    public class GenerarKeys
    {
        public static (string PublicKey, string PrivateKey) GenerateKeys()
        {
            using (var rsa = new RSACryptoServiceProvider(2048)) // Elige el tamaño de la clave
            {
                try
                {
                    // Obtener la representación en cadena de la clave pública
                    string publicKey = rsa.ToXmlString(false);

                    // Obtener la representación en cadena de la clave privada(incluye la clave pública)
                    string privateKey = rsa.ToXmlString(true);
                    return (publicKey, privateKey);
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }
        }
    }
}
