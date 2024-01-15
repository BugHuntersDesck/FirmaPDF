using System;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Operators;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;

namespace WACertifivados.AppCode.Controladoras
{
    public class CGestorSolicitudCertificados
    {
        public CGestorSolicitudCertificados()
        {

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

        public X509Certificate2 FirmarCSR(string rutaCsr, string rutaPfxCA, string contraseñaPfxCA)
        {
            //// Extraer la clave privada de la CA
            //RSA clavePrivadaCA = ExtraerClavePrivadaDePfx(rutaPfxCA, contraseñaPfxCA);

            //// Leer la CSR
            //byte[] csrBytes = File.ReadAllBytes(rutaCsr);
            ////var csr = new CertificateRequest(csrBytes);

            //// Crear un nuevo certificado basado en la información de la CSR
            //var certificadoCA = new X509Certificate2(rutaPfxCA, contraseñaPfxCA);
            //var certificadoFirmado = csr.Create(certificadoCA.SubjectName, X509SignatureGenerator.CreateForRSA(clavePrivadaCA, RSASignaturePadding.Pkcs1), DateTimeOffset.Now, DateTimeOffset.Now.AddYears(1), Guid.NewGuid().ToByteArray());

            //// Exportar el certificado firmado
            //return new X509Certificate2(certificadoFirmado.Export(X509ContentType.Cert));
            return null;
        }

        public X509Certificate2 FirmarCSRConBouncyCastle(string rutaCsr, string rutaPfxCA, string contraseñaPfxCA)
        {
            // Leer el archivo CSR
            var csrBytes = File.ReadAllBytes(rutaCsr);
            var csr = new Pkcs10CertificationRequest(csrBytes);

            // Cargar el certificado y la clave privada de la CA
            var caCertificado = new X509Certificate2(rutaPfxCA, contraseñaPfxCA, X509KeyStorageFlags.Exportable);
            var caClavePrivada = DotNetUtilities.GetKeyPair(caCertificado.PrivateKey).Private;

            // Extraer información del CSR
            var csrInfo = csr.GetCertificationRequestInfo();

            // Crear el certificado firmado
            var generadorCert = new X509V3CertificateGenerator();
            generadorCert.SetSerialNumber(BigIntegers.CreateRandomInRange(BigInteger.One, BigInteger.ValueOf(long.MaxValue), new SecureRandom()));
            generadorCert.SetIssuerDN(PrincipalUtilities.GetSubjectX509Principal(caCertificado));
            generadorCert.SetNotBefore(DateTime.UtcNow.Date);
            generadorCert.SetNotAfter(DateTime.UtcNow.Date.AddYears(1));
            generadorCert.SetSubjectDN(csr.GetCertificationRequestInfo().Subject);
            generadorCert.SetPublicKey(csr.GetPublicKey());

            // Configuraciones adicionales aquí, como extensiones de certificado

            // Crear SignatureFactory
            ISignatureFactory signatureFactory = new Asn1SignatureFactory("SHA256WithRSA", caClavePrivada);

            // Firmar el certificado
            var certificadoFirmado = generadorCert.Generate(signatureFactory);

            // Convertir a X509Certificate2
            return new X509Certificate2(certificadoFirmado.GetEncoded());
        }

        public X509Certificate2 ConvertirBouncyCastleCertificadoADotNet(Org.BouncyCastle.X509.X509Certificate certificadoBC)
        {
            byte[] certificadoBytes = certificadoBC.GetEncoded();
            return new X509Certificate2(certificadoBytes);
        }
    }
}