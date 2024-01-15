using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace WAFirmasGestion.WebForms
{
    public partial class PVerDocumentos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string documentId = Request.QueryString["docId"];
                if (!string.IsNullOrEmpty(documentId))
                {
                    CargarDocumento(documentId);
                }
            }
        }

        private void CargarDocumento(string documentId)
        {
            string folderPath = Server.MapPath("~/UploadedFiles/");
            string jsonFilePath = Path.Combine(folderPath, documentId + ".json");

            if (File.Exists(jsonFilePath))
            {
                string jsonData = File.ReadAllText(jsonFilePath);
                DocumentDetails documento = JsonConvert.DeserializeObject<DocumentDetails>(jsonData);

                if (documento != null)
                {
                    lblNombreDocumento.Text = documento.FileName;

                    // Asegúrate de que el nombre del archivo incluya la extensión correcta y sea el nombre real del archivo.
                    string filePath = Path.Combine(folderPath, documento.FileName);

                    // Convertir la ruta del archivo a una URL relativa para el navegador.
                    string fileUrl = ResolveClientUrl(filePath);

                    // Establecer el atributo 'src' del iframe para apuntar a la URL del archivo.
                    iframeDocumento.Attributes["src"] = fileUrl;
                }
            }
        }

    }

}