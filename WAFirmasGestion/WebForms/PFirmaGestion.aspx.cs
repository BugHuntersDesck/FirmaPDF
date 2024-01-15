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
    public partial class PFirmaGestion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GridViewSolicitudes.DataSource = ObtenerDocumentos();
                GridViewSolicitudes.DataBind();
            }
        }

        
        private List<DocumentDetails> ObtenerDocumentos()
        {
            List<DocumentDetails> documentos = new List<DocumentDetails>();
            string folderPath = Server.MapPath("~/UploadedFiles/");

            foreach (var jsonFilePath in Directory.GetFiles(folderPath, "*.json"))
            {
                string jsonData = File.ReadAllText(jsonFilePath);
                DocumentDetails documento = JsonConvert.DeserializeObject<DocumentDetails>(jsonData);
                if (documento != null)
                {
                    documentos.Add(documento);
                }
            }

            return documentos;
        }

        private void CambiarEstadoDocumento(string documentId, string nuevoEstado)
        {
            string folderPath = Server.MapPath("~/UploadedFiles/");
            string jsonFilePath = Path.Combine(folderPath, documentId + ".json");

            if (File.Exists(jsonFilePath))
            {
                string jsonData = File.ReadAllText(jsonFilePath);
                DocumentDetails documento = JsonConvert.DeserializeObject<DocumentDetails>(jsonData);

                if (documento != null)
                {
                    documento.Estado = nuevoEstado;
                    string updatedJson = JsonConvert.SerializeObject(documento);
                    File.WriteAllText(jsonFilePath, updatedJson);

                    // Recargar el GridView
                    GridViewSolicitudes.DataSource = ObtenerDocumentos();
                    GridViewSolicitudes.DataBind();
                }
            }
        }
        // Este método asume que ya tienes el DocumentId del archivo que quieres firmar.
        private string ObtenerRutaDelArchivoDesdeJson(string documentId)
        {
            string folderPath = Server.MapPath("~/UploadedFiles/");
            string jsonFilePath = Path.Combine(folderPath, documentId + ".json");

            if (File.Exists(jsonFilePath))
            {
                string jsonData = File.ReadAllText(jsonFilePath);
                DocumentDetails documento = JsonConvert.DeserializeObject<DocumentDetails>(jsonData);
                if (documento != null)
                {
                    // Retorna la ruta completa del archivo almacenada en el JSON
                    return documento.FilePath;
                }
            }
            return null;
        }

        protected void GridViewSolicitudes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string documentId = e.CommandArgument.ToString();
            string filePath;
            switch (e.CommandName)
            {
                case "Ver":
                    filePath = ObtenerRutaDelArchivo(documentId);
                    string fileName = Path.GetFileName(filePath);
                    Response.Redirect($"PVerDocumentos.aspx?docId={documentId}&fileName={HttpUtility.UrlEncode(fileName)}");
                    break;
                case "Rechazar":
                    CambiarEstadoDocumento(documentId, "Rechazado");
                    break;
                case "Firmar":
                    CambiarEstadoDocumento(documentId, "Firmado");
                    string fileUrl = ObtenerRutaDelArchivoDesdeJson(documentId);
                    if (!string.IsNullOrEmpty(fileUrl))
                    {
                        // Redirige a la página de firma y pasa tanto el documentId como la ruta del archivo
                        Response.Redirect($"PFirmarSolicitud.aspx?documentId={documentId}&fileUrl ={ HttpUtility.UrlEncode(fileUrl)}");
                    }
                    break;
                case "Descargar":
                    filePath = Path.Combine(Server.MapPath("~/UploadedFiles/"), documentId + "_nombreDelArchivo.pdf");
                    if (File.Exists(filePath))
                    {
                        Response.ContentType = "application/pdf";
                        Response.AppendHeader("Content-Disposition", $"attachment; filename={Path.GetFileName(filePath)}");
                        Response.TransmitFile(filePath);
                        Response.End();
                    }
                    break;
            }
        }

        private string ObtenerRutaDelArchivo(string documentId)
        {
            string folderPath = Server.MapPath("~/UploadedFiles/");
            string jsonFilePath = Path.Combine(folderPath, documentId + ".json");

            if (File.Exists(jsonFilePath))
            {
                string jsonData = File.ReadAllText(jsonFilePath);
                DocumentDetails documento = JsonConvert.DeserializeObject<DocumentDetails>(jsonData);
                if (documento != null)
                {
                    return Path.Combine(folderPath, documento.FileName);
                }
            }
            return null;
        }
        #region metodosPrivados
        private void DescargarPDF(string filePath)
        {
            string fileName = Path.GetFileName(filePath);
            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
            Response.TransmitFile(filePath);
            Response.End();
        }
        #endregion
    }
}
