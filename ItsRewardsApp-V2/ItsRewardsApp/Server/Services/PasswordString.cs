using System.Security.Cryptography;
using System.Text;

namespace ItsRewardsApp.Server.Services
{
    public class PasswordString
    {
        // Identify that the key has been encrypted already
        private const string _cEncrypted = "<1>";
        private const string _cEncrypted11 = "<1.1>";

        /// <summary>The desired encryption looking to use</summary>
        public enum Provider
        {
            TripleDes,
            Des,
            AES
        }

        // The type indicated by the instance
        private Provider _provider;
        private string _storeKey;
        private byte[] _IVData;
        private Encoding _charecterEnc;
        private string _cryptPre;

        /// <summary>
        /// The Construction
        /// </summary>
        /// <param name="P"></param>
        public PasswordString(Provider P)
        {
            _provider = P;
        }

        public PasswordString(Provider P, string StoreKey, byte[] IVData)
            : this(P, StoreKey, IVData, null, null)
        {
            //_provider = P;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="P"></param>
        /// <param name="StoreKey"></param>
        /// <param name="IVData"></param>
        public PasswordString(Provider P, string StoreKey, byte[] IVData,
            Encoding CharEncoding = null, string EncrypPre = null)
        {
            _provider = P;
            _storeKey = StoreKey;
            _IVData = IVData;
            _charecterEnc = CharEncoding;
            _cryptPre = EncrypPre;
        }

        /// <summary></summary>
        public string Value
        {
            get
            {
                return decryptString(_inPutValue);
            }
            set
            {

                string Result;
                if (encryptString(value, out Result))
                    _inPutValue = Result;

            }
        }

        /// <summary>The set encrypted value</summary>
        public string Encrypted
        {
            get
            {
                if (!_inPutValue.StartsWith(_cEncrypted))
                    _inPutValue = _cEncrypted + _inPutValue;
                return _inPutValue;
            }
        }
        private string _inPutValue;


        /// <summary>
        /// Encrypt the 
        /// </summary>
        /// <param name="InputString"></param>
        /// <param name="Result"></param>
        /// <returns></returns>
        private bool encryptString(string InputString, out string Result)
        {
            bool Success;
            Result = null;
            if (InputString.StartsWith(_cEncrypted, StringComparison.InvariantCultureIgnoreCase))
            {
                // Value is already encrypted 
                Result = InputString.Substring(3);
                Success = true;
            }
            else if (_provider == Provider.TripleDes)
                Success = encryptTripleDes(InputString, ref Result);
            else if (_provider == Provider.AES)
                Success = encryptAES(InputString, ref Result);
            else
            {
                Success = encryptDes(InputString, ref Result);
            }
            return Success;
        }

        // 
        // Helper function to encode in AES
        //
        private bool encryptAES(string InputString, ref string Result)
        {
            bool Success;
            try
            {
                AesManaged aes = new AesManaged();
                byte[] byteInput = _charecterEnc == null
                        ? Encoding.UTF8.GetBytes(InputString)
                        : _charecterEnc.GetBytes(InputString);
                aes.Key = ReadBytes("gCjK+DZ/GCYbKIGiAt1qCA==");
                aes.IV = ReadBytes("47l5QsSe1POo31ad");
                Result = encryptMemoryString(aes, ReadBytes("47l5QsSe1POo31ad"),
                    ReadBytes("gCjK+DZ/GCYbKIGiAt1qCA=="), byteInput);
                Success = true;
            }
            catch (Exception)
            {
                Success = false;
            }
            return Success;
        }

        // 
        // Helper function to encrypt the triple des
        //
        private bool encryptDes(string InputString, ref string Result)
        {
            bool Success;
            try
            {
                DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
                byte[] IV = _IVData;
                string Key = _storeKey;


                byte[] KeyData = ReadBytes(Key); // Convert.FromBase64CharArray(Key.ToCharArray(), 0, Key.ToCharArray().Length);
                byte[] byteInput = _charecterEnc == null
                        ? Encoding.UTF8.GetBytes(InputString)
                        : _charecterEnc.GetBytes(InputString);
                Result = encryptMemoryString(provider, IV, KeyData, byteInput);
                Success = true;
            }
            catch (Exception ex)
            {
               // BaseApplication.LogAppError(ex, "Unable To Encrypt");
                Success = false;
            }
            return Success;
        }

        //
        // If triple des encrypting
        //
        private bool encryptTripleDes(string InputString, ref string Result)
        {
            bool Success;
            try
            {
                TripleDESCryptoServiceProvider provider = new TripleDESCryptoServiceProvider();
                byte[] IV = new byte[] { 236, 46, 160, 1, 62, 42, 6, 0 }; //BaseApplication.Common.GetPasswordIV();
                string Key = "jAV6eBmhn9a2e8keoIQmWgDQmqe2wFGO"; //BaseApplication.Common.GetPasswordKey();

                byte[] KeyData = Convert.FromBase64CharArray(Key.ToCharArray(), 0, Key.ToCharArray().Length);
                byte[] byteInput = Encoding.UTF8.GetBytes(InputString);

                Result = encryptMemoryString(provider, IV, KeyData, byteInput);
                Success = true;
            }
            catch (Exception ex)
            {
                Success = false;
              //  BaseApplication.LogAppError(ex, "Unable To Encrypt");
            }
            return Success;
        }

        // 
        // Helper function to encrypt the string into memory
        //
        private string encryptMemoryString(SymmetricAlgorithm provider, byte[] IV, byte[] KeyData, byte[] byteInput)
        {
            string Result = string.Empty;
            using (MemoryStream ms = new MemoryStream())
            {
                ICryptoTransform transform = provider.CreateEncryptor(KeyData, IV);
                CryptoStream cryptoStream = new CryptoStream(ms, transform, CryptoStreamMode.Write);
                cryptoStream.Write(byteInput, 0, byteInput.Length);
                cryptoStream.FlushFinalBlock();
                Result = Convert.ToBase64String(ms.ToArray());
            }
            return Result;
        }

        //
        // Helper function to decrypt based on base
        //
        private string decryptString(string inputString)
        {
            string Result;
            try
            {
                if (inputString.StartsWith(_cEncrypted))
                    inputString = inputString.Substring(3);

                if (!string.IsNullOrWhiteSpace(_cryptPre) && inputString.StartsWith(_cryptPre))
                    inputString = inputString.Substring(_cryptPre.Length);


                if (_provider == Provider.AES)
                {
                    Result = decryptAES(inputString);
                }
                else
                {
                    Result = decryptDes(inputString);
                }

            }
            catch
            {
                Result = null;
            }

            return Result;
        }

        /// <summary>
        /// Helper function to decrypt aes
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        private string decryptAES(string inputString)
        {
            string Result = string.Empty;
            try
            {
                AesManaged aes = new AesManaged();
                byte[] Key = ReadBytes("gCjK+DZ/GCYbKIGiAt1qCA==");
                byte[] IV = ReadBytes("47l5QsSe1POo31ad");

                aes.Key = ReadBytes("gCjK+DZ/GCYbKIGiAt1qCA==");
                aes.IV = ReadBytes("47l5QsSe1POo31ad");

                byte[] byteInput = Convert.FromBase64String(inputString);

                Result = decryptMemoryStream(aes, IV, Key, byteInput);
            }
            catch (Exception)
            {

            }

            return Result;
        }

        //
        // Helper function for decrypt for des ...
        //
        private string decryptDes(string inputString)
        {
            string Result;

            byte[] IV = _IVData;
            string Key = _storeKey;

            byte[] KeyData = ReadBytes(Key);

            byte[] byteInput = new byte[inputString.Length];
            byteInput = Convert.FromBase64String(inputString);
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();

            Result = decryptMemoryStream(provider, IV, KeyData, byteInput);
            return Result;
        }

       
        protected byte[] ReadBytes(string Value)
        {
            return new ASCIIEncoding().GetBytes(Value);
        }


        //
        // Helper function to decrypt a memory stream
        //
        private string decryptMemoryStream(SymmetricAlgorithm provider, byte[] IV, byte[] KeyData, byte[] byteInput)
        {
            string Result;
            using (MemoryStream ms = new MemoryStream())
            {
                ICryptoTransform transform = provider.CreateDecryptor(KeyData, IV);
                CryptoStream cryptoStream = new CryptoStream(ms, transform, CryptoStreamMode.Write);
                cryptoStream.Write(byteInput, 0, byteInput.Length);
                cryptoStream.FlushFinalBlock();
                Encoding encoding1 = _charecterEnc == null ? Encoding.UTF8 : _charecterEnc;
                Result = encoding1.GetString(ms.ToArray());
            }
            return Result;
        }
    }
}
