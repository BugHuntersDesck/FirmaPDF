using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Security.Cryptography;
using System.Text;

namespace SWLNPdfFirma.AppCode.Servicio
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "ICertificadoService" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface ICertificadoService
    {
        [OperationContract]
        string GenerarCertificadoYClave(string nombreTitular, string rutaPfx);

        [OperationContract]
        string GenerarCertificadoYClave2(string nombreTitular, string rutaPfx, string clave);
    }
}
