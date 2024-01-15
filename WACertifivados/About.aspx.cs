using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

using WACertifivados.AppCode.Controladoras;

using WACertifivados.ServiceReference1;

namespace WACertifivados
{
    public partial class About : Page
    {
        private ICertificadoService _servicioCertificados;
        private CGestorSolicitudCertificados _gestorSolicitud;

        private string Direccion { get; set; }
        private string Titular { get; set; }

        public About()
        {
            
            _servicioCertificados = new CertificadoServiceClient();
            _gestorSolicitud = new CGestorSolicitudCertificados();

            
        }

        public About(string Direccion, string nombreTitular)
        {

            _servicioCertificados = new CertificadoServiceClient();
            _gestorSolicitud = new CGestorSolicitudCertificados();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Recuperar valores de las variables de sesión
            string ruta = Session["RutaPfx"] as string;
            string titular = Session["NombreTitular"] as string;

            if (ruta != null && titular != null)
            {
                Direccion = ruta;
                Titular = titular;
            }
            else
            {
                Direccion = "";
                Titular = "none";
            }

            lblNombreTitular.Text = Titular;
            lblRuta.Text = Direccion;
        }

        protected void btn_EnviarSolicitudCSR_Click(object sender, EventArgs e)
        {
            RSA privateKey = _gestorSolicitud.ExtraerClavePrivadaDePfx(this.Direccion, "1234");

            byte[] csr = _gestorSolicitud.GenerarCSR(this.Titular, privateKey);

            string ruta = "E:/VS/VS 2017/SWLNFirmaPDF/certificados/csr/";
            string nombreArchivo = this.Titular + "_Solicitud.csr";

            File.WriteAllBytes(ruta + nombreArchivo, csr);

            Response.Write("<script>alert('Solicitud de certificado generada con exito')</script>");
        }
    }
}