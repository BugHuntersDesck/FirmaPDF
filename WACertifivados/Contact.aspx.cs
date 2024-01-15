using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography.X509Certificates;

using WACertifivados.ServiceReference1;

namespace WACertifivados
{
    public partial class Contact : Page
    {
        private ICertificadoService _servicioCertificados;

        public Contact()
        {
            _servicioCertificados = new CertificadoServiceClient();
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void GenerarClavesYCSR(object sender, EventArgs e)
        {
            string nombreTitular = titularName.Value;
            string clave = contrasena.Value;

            // Asignar valores a las variables de sesión
            Session["NombreTitular"] = nombreTitular;

            // metodo para pedir que se habra el explorador de archivos y se seleccione la carpeta, nombrar con el nombre.pxf y guardar en un string
            string path = retornarRuta();
            Session["RutaPfx"] = path;

            string rutaExito = _servicioCertificados.GenerarCertificadoYClave2(nombreTitular, path, clave);

            // emitir menasje en la pagina con rutaExito
            Response.Write("<script>alert('" + rutaExito + "')</script>");

            // redirigir a la pagina de About.aspx y utilizar el constructor que recibe un string
            solicitarCertificado();
            
        }

        private string retornarRuta()
        {
            // la ruta por defecto es E:\VS\VS 2017\SWLNFirmaPDF\certificados\pfxs concatenando el nombre mas la extencion .pfx
            string ruta = "E:/VS/VS 2017/SWLNFirmaPDF/certificados/pfxs/";
            string nombre = titularName.Value;
            string nombreArchivo = nombre + ".pfx";

            return ruta + nombreArchivo;
        }

        private void solicitarCertificado()
        {
           Response.Redirect("About.aspx?ruta=");
        }
    }
}