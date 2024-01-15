using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;

namespace WAFirmasGestion.WebForms
{
    public class EFirma
    {
        public int FirmaID { get; set; }
        public string NombreDocumento { get; set; }
        public int UsuarioID { get; set; }
        public string Estado { get; set; }
    }
    public class DocumentDetails
    {
        public string DocumentId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; } // Nueva propiedad para la ruta del archivo
        public string Hash { get; set; }
        public string EncryptedHash { get; set; }
        public string Estado { get; set; }
        public string PublicKey { get; set; }
        public string PrivateKey { get; set; }
    }


    public class GenerarKeys
    {
        public static (string PublicKey, string PrivateKey) GenerateKeys()
        {
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                try
                {
                    string publicKey = rsa.ToXmlString(false);
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