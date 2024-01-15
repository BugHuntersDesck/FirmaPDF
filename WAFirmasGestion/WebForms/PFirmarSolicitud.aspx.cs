using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Collections.Generic;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Security.Cryptography;
using System.Text;
using QRCoder;
using System.Text.Json;
using System.IO.Compression;


namespace WAFirmasGestion.WebForms
{
    public partial class PFirmarSolicitud : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnSubir_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                byte[] imageBytes = FileUpload1.FileBytes;
                string base64Image = Convert.ToBase64String(imageBytes);
                byte[] key = GenerarClaveAleatoria();
                byte[] encryptedImage = Encriptar(Encoding.UTF8.GetBytes(base64Image), key);

                Session["EncryptedImage"] = encryptedImage;
                Session["EncryptionKey"] = key;
            }
        }

        protected void btnSubirPDF_Click(object sender, EventArgs e)
        {
            if (FileUploadPDF.HasFile)
            {
                string directoryPath = Server.MapPath("~/UploadedFiles/");
                string originalPdfPath = directoryPath + FileUploadPDF.FileName;
                string signedPdfPath = directoryPath + "Firmado_" + FileUploadPDF.FileName;

                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                FileUploadPDF.SaveAs(originalPdfPath);

                byte[] encryptedImage = Session["EncryptedImage"] as byte[];
                byte[] key = Session["EncryptionKey"] as byte[];

                if (encryptedImage != null && key != null)
                {
                    byte[] decryptedBytes = Desencriptar(encryptedImage, key);
                    string base64DecryptedImage = Encoding.UTF8.GetString(decryptedBytes);
                    byte[] imageBytes = Convert.FromBase64String(base64DecryptedImage);

                    InsertarImagenEnPDF(originalPdfPath, signedPdfPath, imageBytes);
                    GuardarEncriptadoComoZip(signedPdfPath, encryptedImage);
                    DescargarPDF(signedPdfPath);
                }
            }
        }

        private byte[] Encriptar(byte[] data, byte[] key)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = new byte[16];

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        csEncrypt.Write(data, 0, data.Length);
                    }
                    return msEncrypt.ToArray();
                }
            }
        }

        private byte[] Desencriptar(byte[] data, byte[] key)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = new byte[16];

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (var msDecrypt = new MemoryStream(data))
                {
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (var msResult = new MemoryStream())
                        {
                            csDecrypt.CopyTo(msResult);
                            return msResult.ToArray();
                        }
                    }
                }
            }
        }
        private void GuardarEncriptadoComoZip(string filePath, byte[] encryptedData)
        {
            string base64EncryptedData = Convert.ToBase64String(encryptedData);

            using (var memoryStream = new MemoryStream())
            {
                using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                {
                    var zipEntry = archive.CreateEntry("data.txt");
                    using (var entryStream = zipEntry.Open())
                    using (var streamWriter = new StreamWriter(entryStream))
                    {
                        streamWriter.Write(base64EncryptedData);
                    }
                }

                var json = JsonSerializer.Serialize(new { Data = base64EncryptedData });
                File.WriteAllText(Path.Combine(Server.MapPath("~/UploadedFiles/"), "data.json"), json);
            }
        }
        private byte[] GenerarClaveAleatoria()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var randomKey = new byte[32];
                rng.GetBytes(randomKey);
                return randomKey;
            }
        }

        private void InsertarImagenEnPDF(string originalPdfPath, string signedPdfPath, byte[] imageBytes)
        {
            using (FileStream fs = new FileStream(originalPdfPath, FileMode.Open, FileAccess.Read))
            {
                PdfReader pdfReader = new PdfReader(fs);
                using (FileStream outputStream = new FileStream(signedPdfPath, FileMode.Create, FileAccess.Write))
                {
                    using (PdfStamper pdfStamper = new PdfStamper(pdfReader, outputStream))
                    {
                        Image image = Image.GetInstance(imageBytes);
                        image.ScalePercent(50);
                        image.SetAbsolutePosition(180, 150);
                        PdfContentByte content = pdfStamper.GetUnderContent(1);
                        content.AddImage(image);
                        int numeroPaginas = pdfReader.NumberOfPages;

                        for (int Pagina = 1; Pagina <= numeroPaginas; Pagina++)
                        {
                            string pathSegundaImagen = Server.MapPath("~/UploadedFiles/Qr.png");
                            Image image2 = Image.GetInstance(pathSegundaImagen);
                            image2.ScalePercent(25);
                            image2.SetAbsolutePosition(100, 100);
                            PdfContentByte content1 = pdfStamper.GetUnderContent(Pagina);
                            content1.AddImage(image2);
                        }
                    }
                }
            }
        }

        private void DescargarPDF(string filePath)
        {
            string fileName = Path.GetFileName(filePath);
            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
            Response.TransmitFile(filePath);
            Response.End();
        }
    }
}