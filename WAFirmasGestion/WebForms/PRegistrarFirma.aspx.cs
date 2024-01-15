using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Security.Cryptography;
using System.Text;
using QRCoder;
using System.IO.Compression;
using System.IO;
using System.Text.Json;

namespace WAFirmasGestion.WebForms
{
    public partial class PRegistrarFirma : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
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
        #region MetodosPrivados

        private byte[] GenerarClaveAleatoria()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var randomKey = new byte[32];
                rng.GetBytes(randomKey);
                return randomKey;
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
        #endregion

    }
}