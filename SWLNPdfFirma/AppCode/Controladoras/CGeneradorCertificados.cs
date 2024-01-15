using System;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace SWLNPdfFirma.AppCode.Controladoras
{
    class CGeneradorCertificados
    {
        public CGeneradorCertificados()
        {

        }
        public (RSA, RSA) GenerarParClaves()
        {
            var clavePrivada = RSA.Create(2048); // Tamaño de la clave puede ser configurado
            var clavePublica = RSA.Create(clavePrivada.ExportParameters(false));
            return (clavePrivada, clavePublica);
        }

        public X509Certificate2 CrearCertificado(RSA clavePublica, string nombreTitular)
        {
            var request = new CertificateRequest(
                new X500DistinguishedName($"CN={nombreTitular}"), clavePublica, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);

            // Definir el periodo de validez del certificado
            var inicioValidez = DateTimeOffset.Now;
            var finValidez = DateTimeOffset.Now.AddYears(1); // 1 año de validez

            // Crear el certificado auto-firmado (en un escenario real, debería ser firmado por una CA)
            var certificado = request.CreateSelfSigned(inicioValidez, finValidez);

            return certificado;
        }

        //1er paso
        public string GenerarCertificadoYClave(string nombreTitular, string rutaPfx)
        {
            // Generar un nuevo par de claves RSA
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                // Crear una solicitud de certificado
                var request = new CertificateRequest(
                    new X500DistinguishedName($"CN={nombreTitular}"),
                    rsa,
                    HashAlgorithmName.SHA256,
                    RSASignaturePadding.Pkcs1);

                // Definir el periodo de validez del certificado
                var inicioValidez = DateTimeOffset.Now;
                var finValidez = DateTimeOffset.Now.AddYears(1); // 1 año de validez

                // Crear el certificado auto-firmado
                var certificado = request.CreateSelfSigned(inicioValidez, finValidez);

                // Exportar el certificado con la clave privada en formato PFX
                byte[] pfxData = certificado.Export(X509ContentType.Pfx);

                // Escribir el archivo PFX en la ruta especificada
                File.WriteAllBytes(rutaPfx, pfxData);

                return rutaPfx;
            }
        }

        public string GenerarCertificadoYClave(string nombreTitular, string rutaPfx, string clave)
        {
            // Generar un nuevo par de claves RSA
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                // Crear una solicitud de certificado
                var request = new CertificateRequest(
                    new X500DistinguishedName($"CN={nombreTitular}"),
                    rsa,
                    HashAlgorithmName.SHA256,
                    RSASignaturePadding.Pkcs1);

                // Definir el periodo de validez del certificado
                var inicioValidez = DateTimeOffset.Now;
                var finValidez = DateTimeOffset.Now.AddYears(1); // 1 año de validez

                // Crear el certificado auto-firmado
                var certificado = request.CreateSelfSigned(inicioValidez, finValidez);

                // Exportar el certificado con la clave privada en formato PFX con la contraseña especificada
                byte[] pfxData = certificado.Export(X509ContentType.Pfx, clave);

                // Escribir el archivo PFX en la ruta especificada
                File.WriteAllBytes(rutaPfx, pfxData);

                return rutaPfx;
            }
        }

        // 2do paso
        public RSA ExtraerClavePrivadaDePfx(string rutaPfx, string contraseñaPfx)
        {
            var certificado = new X509Certificate2(rutaPfx, contraseñaPfx, X509KeyStorageFlags.Exportable);
            return certificado.GetRSAPrivateKey();
        }

        //3er paso
        public byte[] GenerarCSR(string nombreTitular, RSA clavePrivada)
        {
            var request = new CertificateRequest(
                new X500DistinguishedName($"CN={nombreTitular}"),
                clavePrivada,
                HashAlgorithmName.SHA256,
                RSASignaturePadding.Pkcs1);

            // Agregar: O, OU, C, ST, L, E.

            return request.CreateSigningRequest();
        }
    }
}
