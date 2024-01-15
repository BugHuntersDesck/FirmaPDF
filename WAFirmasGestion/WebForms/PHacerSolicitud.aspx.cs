using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI;
using Newtonsoft.Json;

namespace WAFirmasGestion.WebForms
{
    

    public partial class PHacerSolicitud : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var keys = GenerarKeys.GenerateKeys();
                Session["PublicKey"] = keys.PublicKey;
                Session["PrivateKey"] = keys.PrivateKey;
            }
        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            string publicKey = Session["PublicKey"] as string;
            string privateKey = Session["PrivateKey"] as string;

            if (fileUploadDocumento.HasFile)
            {
                string filename = Path.GetFileName(fileUploadDocumento.FileName);
                string folderPath = Server.MapPath("~/UploadedFiles/");

                // Verifica si la carpeta existe, si no, la crea
                if (!Directory.Exists(folderPath))

                {
                    Directory.CreateDirectory(folderPath);
                }
                string documentId = Guid.NewGuid().ToString();
                string fullPath = Path.Combine(folderPath, documentId + "_" + filename);

                try
                {
                    fileUploadDocumento.SaveAs(fullPath);

                    string hash = GenerateHash(fullPath);
                    string encryptedHash = EncryptHash(hash, publicKey);

                    var documentDetails = new DocumentDetails
                    {
                        DocumentId = documentId,
                        FileName = filename,
                        FilePath = fullPath, // Asignar la ruta del archivo aquí
                        Hash = hash,
                        EncryptedHash = encryptedHash,
                        Estado = "En espera",
                        PublicKey = publicKey,
                        PrivateKey = privateKey // Solo para propósitos de prueba
                    };

                    string json = JsonConvert.SerializeObject(documentDetails);
                    File.WriteAllText(Path.Combine(folderPath, documentId + ".json"), json);
                }
                catch (Exception ex)
                {
                    // Manejo de errores, por ejemplo, mostrando un mensaje al usuario
                    // Response.Write("Error: " + ex.Message);
                }
            }
        }

        private string GenerateHash(string filePath)
        {
            using (var sha256 = SHA256.Create())
            {
                using (var fileStream = File.OpenRead(filePath))
                {
                    var hash = sha256.ComputeHash(fileStream);
                    return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                }
            }
        }

        private string EncryptHash(string hash, string publicKey)
        {
            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(publicKey);
                var encryptedData = rsa.Encrypt(Encoding.UTF8.GetBytes(hash), true);
                return Convert.ToBase64String(encryptedData);
            }
        }
    }
}