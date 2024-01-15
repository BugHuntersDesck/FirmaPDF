using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.ServiceModel;
using System.Text;

using SWLNPdfFirma.AppCode.Controladoras;

namespace SWLNPdfFirma.AppCode.Servicio
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "CertificadoService" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione CertificadoService.svc o CertificadoService.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class CertificadoService : ICertificadoService
    {
        #region Atributos
        private CGeneradorCertificados _generadorCertificados;
        #endregion

        public CertificadoService()
        {
            _generadorCertificados = new CGeneradorCertificados();
        }

        public string GenerarCertificadoYClave2(string nombreTitular, string rutaPfx, string clave)
        {
            return _generadorCertificados.GenerarCertificadoYClave(nombreTitular, rutaPfx, clave);
        }

        public RSA ExtraerClavePrivadaDePfx(string rutaPfx, string contrasenaPfx)
        {
            return _generadorCertificados.ExtraerClavePrivadaDePfx(rutaPfx, contrasenaPfx);
        }

        public string GenerarCertificadoYClave(string nombreTitular, string rutaPfx)
        {
            return _generadorCertificados.GenerarCertificadoYClave(nombreTitular, rutaPfx);
        }

        public byte[] GenerarCSR(string nombreTitular, RSA clavePrivada)
        {
            return _generadorCertificados.GenerarCSR(nombreTitular, clavePrivada);
        }
    }
}
