using ItsRewardsApp.Server.Interfaces;
using ItsRewardsApp.Shared.Models;
using ItsRewardsApp.Shared.ViewModels;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Mail;
using Microsoft.EntityFrameworkCore;
using ItsRewardsApp.Server.Helper;
using Plivo.XML;

namespace ItsRewardsApp.Server.Services
{
    public class Login : ILogin
    {
        private readonly LoyaltyUserDBContext _dbContext = new();
        public Login(LoyaltyUserDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public LoginViewModel LoginValidation(LoginViewModel loginViewModel)
        {
            LoyaltyUserProfile user = new LoyaltyUserProfile();
            string password = string.Empty;
            string cell = loginViewModel.CellPhone;
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            user = _dbContext.LoyaltyUserProfile.Where(x => x.CellPhone == cell 
                    && x.AppVerified == "Y"      ).FirstOrDefault();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
                              // && x.AppVerified != null && x.AppVerified == "Y"
            if (user != null)
            {
                
                
                DecryptAES(user.UserPass, ref password);
                if(password == loginViewModel.Password)
                {
                    loginViewModel.isValid = true;
                }
                else
                {
                    var encoding = Encoding.GetEncoding("iso-8859-1");
                    var Pass = encoding.GetString(user.UserPass);

                    if (Pass != string.Empty)
                    {
                        PasswordString password1 = new PasswordString(PasswordString.Provider.AES);
                        password1.Value = Pass;
                        if(password1.Value == loginViewModel.Password)
                            loginViewModel.isValid = true;
                        //DecryptAES(System.Text.UTF8Encoding.UTF8.GetBytes(Pass), ref password);

                    }
                }
                loginViewModel.isActive = user.isActive;
            }

            return loginViewModel;
        }
        private bool DecryptAES(byte[] byteInput, ref string Result)
        {
            bool Success;
            try
            {
                AesManaged aes = new AesManaged();
                //byte[] byteInput = Encoding.UTF8.GetBytes(Password);
                ASCIIEncoding encoding = new ASCIIEncoding();
                aes.Key = encoding.GetBytes("gCjK+DZ/GCYbKIGiAt1qCA==");
                aes.IV = encoding.GetBytes("47l5QsSe1POo31ad");
                Result = decryptMemoryStream(aes, aes.IV,
                    aes.Key, byteInput);
                Success = true;
            }
            catch (Exception ex)
            {
                Success = false;
            }
            return Success;
        }
        private string decryptMemoryStream(SymmetricAlgorithm provider, byte[] IV, byte[] KeyData, byte[] byteInput)
        {
            string Result;
            using (MemoryStream ms = new MemoryStream())
            {
                ICryptoTransform transform = provider.CreateDecryptor(KeyData, IV);
                CryptoStream cryptoStream = new CryptoStream(ms, transform, CryptoStreamMode.Write);
                cryptoStream.Write(byteInput, 0, byteInput.Length);
                cryptoStream.FlushFinalBlock();
                //Encoding encoding1 = charecterEnc == null ? Encoding.UTF8 : charecterEnc;
                Encoding encoding1 = Encoding.UTF8;
                Result = encoding1.GetString(ms.ToArray());
            }
            return Result;
        }
        public string? SendMail(string Email)
        {
            LoyaltyUserProfile user = new LoyaltyUserProfile();
            string password = string.Empty;
            user = _dbContext.LoyaltyUserProfile.Where(x => x.EMail.ToLower() == Email.ToLower()).FirstOrDefault();
            if (user != null)
            {
                string to = Email; //To address    
                string from = "abhi.avidclan@gmail.com"; //From address    
                MailMessage message = new MailMessage(from, to);

                string mailbody = "In this article you will learn how to send a email using Asp.Net & C#";
                message.Subject = "Sending Email Using Asp.Net & C#";
                message.Body = mailbody;
                message.BodyEncoding = Encoding.UTF8;
                message.IsBodyHtml = true;
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Gmail smtp
                client.UseDefaultCredentials = false;
                NetworkCredential basicCredential1 = new NetworkCredential("abhi.avidclan@gmail.com", "Test@123");
                client.EnableSsl = true;
                client.Credentials = basicCredential1;
                try
                {
                    client.Send(message);
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return user?.EMail;
        }
        public string ResetUserPassword(ResetPasswordViewModel model)
        {
            var result = string.Empty;
            byte[] password = null;
            CommonHelper commonHelp = new CommonHelper();
            commonHelp.encryptAES(model.Password, ref password);
            var encoding = Encoding.GetEncoding("iso-8859-1");
            var user = _dbContext.LoyaltyUserProfile.Where(x => x.CellPhone == model.CellPhone).FirstOrDefault();
            if (user != null)
            {
                user.UserPass = password;// encoding.GetBytes(password);
                _dbContext.Entry(user).State = EntityState.Modified;
                _dbContext.SaveChanges();
                result = "Success";
            }
            else
            {
                result = "Fail";
            }
            return result;
        }
        public string SendMessage(string CellPhone)
        {
            LoyaltyUserProfile validuser = new LoyaltyUserProfile();
            validuser = _dbContext.LoyaltyUserProfile.Where(x => x.CellPhone == CellPhone
                    && x.AppVerified == "Y").FirstOrDefault();
            if (validuser == null)
            {
                return "Invalid cellphone";
            }
            else {
                Random generator = new Random();
                String pin = generator.Next(0, 1000000).ToString("D6");

                CommonHelper commonHelper = new CommonHelper();
                var result = commonHelper.SendToMobile(pin, CellPhone);
                if (result == "")
                {
                    return "";
                }
                else
                {
                    return pin;
                }
            }
        }
    }
}
