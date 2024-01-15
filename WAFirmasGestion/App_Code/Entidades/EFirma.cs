using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WAFirmasGestion.WebForms;
namespace WAFirmasGestion.App_Code.Entidades
{
    public class EFirma
    {
        public int FirmaID { get; set; }
        public string NombreDocumento { get; set; }
        public int UsuarioID { get; set; }
        public string Estado { get; set; }
    }
}