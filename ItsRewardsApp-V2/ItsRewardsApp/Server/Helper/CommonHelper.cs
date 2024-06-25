using ItsRewardsApp.Server.Interfaces;
using ItsRewardsApp.Shared.Models;
using ItsRewardsApp.Shared.ViewModels;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Mail;
using Microsoft.EntityFrameworkCore;
namespace ItsRewardsApp.Server.Helper
{
    public class CommonHelper
    {
        public bool encryptAES(string Password, ref byte[] Result)
        {
            bool Success;
            try
            {
                AesManaged aes = new AesManaged();
                byte[] byteInput = Encoding.UTF8.GetBytes(Password);
                ASCIIEncoding encoding = new ASCIIEncoding();
                aes.Key = encoding.GetBytes("gCjK+DZ/GCYbKIGiAt1qCA==");
                aes.IV = encoding.GetBytes("47l5QsSe1POo31ad");
                Result = encryptMemoryString(aes, aes.IV, aes.Key, byteInput);
                Success = true;
            }
            catch (Exception)
            {
                Success = false;
            }
            return Success;
        }

        private byte[] encryptMemoryString(SymmetricAlgorithm provider, byte[] IV, byte[] KeyData, byte[] byteInput)
        {
            byte[] Result = null;
            using (MemoryStream ms = new MemoryStream())
            {
                ICryptoTransform transform = provider.CreateEncryptor(KeyData, IV);
                CryptoStream cryptoStream = new CryptoStream(ms, transform, CryptoStreamMode.Write);
                cryptoStream.Write(byteInput, 0, byteInput.Length);
                cryptoStream.FlushFinalBlock();
                Result = ms.ToArray();
            }
            return Result;
        }
        public string SendToMobile(string Message, string CellPhone)
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                String username = "MANDNJMMU3ZMRIMTI1NJ";
                String password = "ZDQxMzcyODEzNWE1MmE1MzQzMDgwMTYxMGY2ZDcz";
                String encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(username + ":" + password));

                //Form Data
                //var formData = "dst=" + CellPhone + "&src=18609624045&text=ItsReward's Confirmation Pin:" + Pin;
                var formData = "dst=" + CellPhone + "&src=18609624045&text=" + Message;
                var encodedFormData = Encoding.ASCII.GetBytes(formData);

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.plivo.com/v1/Account/MANDNJMMU3ZMRIMTI1NJ/Message/");
                request.ContentType = "application/x-www-form-urlencoded";
                request.Method = "POST";
                request.ContentLength = encodedFormData.Length;
                request.Headers.Add("Authorization", "Basic " + encoded);
                request.PreAuthenticate = true;

                using (var stream = request.GetRequestStream())
                {
                    stream.Write(encodedFormData, 0, encodedFormData.Length);
                }

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                return responseString;
            }
            catch (Exception ex)
            {
                return "";
                throw;
            }
        }
    }
}
