using System;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace SWLNPdfFirma.AppCode.Controladoras
{
    class CAlmacenamientoCertificados
    {
        private readonly string _directorioAlmacenamiento;

        public CAlmacenamientoCertificados(string directorioAlmacenamiento)
        {
            _directorioAlmacenamiento = directorioAlmacenamiento;
            // Asegúrate de que el directorio de almacenamiento exista
            Directory.CreateDirectory(_directorioAlmacenamiento);
        }

        public void GuardarCertificado(X509Certificate2 certificado, string identificador)
        {
            string rutaArchivo = Path.Combine(_directorioAlmacenamiento, $"{identificador}.pfx");
            File.WriteAllBytes(rutaArchivo, certificado.Export(X509ContentType.Pfx));
        }

        public X509Certificate2 RecuperarCertificado(string identificador)
        {
            string rutaArchivo = Path.Combine(_directorioAlmacenamiento, $"{identificador}.pfx");
            if (File.Exists(rutaArchivo))
            {
                return new X509Certificate2(File.ReadAllBytes(rutaArchivo));
            }

            return null; // O manejar la situación de archivo no encontrado
        }

        // Métodos para guardar y recuperar claves privadas...
    }
}
