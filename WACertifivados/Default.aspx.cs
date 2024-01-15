using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WACertifivados
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarArchivos();
            }
        }

        private void CargarArchivos()
        {
            string rutaCarpeta = @"E:\VS\VS 2017\SWLNFirmaPDF\certificados\csr";
            var archivos = Directory.GetFiles(rutaCarpeta);
            var listaArchivos = new List<object>();

            foreach (var archivo in archivos)
            {
                listaArchivos.Add(new { NombreArchivo = Path.GetFileName(archivo), RutaArchivo = archivo });
            }

            GridViewArchivos.DataSource = listaArchivos;
            GridViewArchivos.DataBind();
        }

        protected void btnFirmar_Click(object sender, EventArgs e)
        {
            var boton = (Button)sender;
            string rutaArchivo = boton.CommandArgument;
            // Aquí puedes añadir tu lógica para firmar el archivo


        }
    }
}