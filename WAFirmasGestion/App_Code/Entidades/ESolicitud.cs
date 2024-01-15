using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WAFirmasGestion.App_Code.Entidades
{
    public class ESolicitud
    {
        public int DocumentoID { get; set; }
        public string NombreDocumento { get; set; }
        public int UsuarioID { get; set; }
        public string Estado { get; set; }

    }
}