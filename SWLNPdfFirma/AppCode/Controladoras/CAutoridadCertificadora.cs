using System;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;


namespace SWLNPdfFirma.AppCode.Controladoras
{
    class CAutoridadCertificadora
    {
        private X509Certificate2 _certificadoCA;

        public CAutoridadCertificadora(X509Certificate2 certificadoCA)
        {
            _certificadoCA = certificadoCA;
        }

        public X509Certificate2 FirmarCertificado(CertificateRequest solicitud)
        {
            // Asegúrate de que la CA tiene una clave privada para firmar
            if (!_certificadoCA.HasPrivateKey)
            {
                throw new InvalidOperationException("La CA no tiene una clave privada.");
            }

            // Firma la solicitud de certificado y emite el certificado
            var certificadoFirmado = solicitud.Create(_certificadoCA.SubjectName, X509SignatureGenerator.CreateForRSA((RSA)_certificadoCA.GetRSAPrivateKey(), RSASignaturePadding.Pkcs1), DateTimeOffset.Now, DateTimeOffset.Now.AddYears(1), Guid.NewGuid().ToByteArray());
            return certificadoFirmado;
        }

        // Implementa RevocarCertificado(), GenerarListaRevocacion(), ValidarCertificado()...
    }
}
