using RestSharp;
using System;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace LibertyConsignmentAPI.Auth
{
    /// <summary>
    /// Used to input all authentication information for restricted Liberty API calls.
    /// </summary>
    public class Credentials
    {
        public string ServerName { get; set; }
        public string ListenPortNumber { get; set; }
        public string ApplicationID { get; set; }
        public string ApiVersion { get; set; }
        public string ApiKey { get; set; }
        internal int CallTime { get; private set; }
        internal CookieContainer Cookie { get; set; }
        public bool KeepSessionOpen { get; set; }
        public User User { get; set; }

        internal void GetCallTime()
        {
            CallTime = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }

        /// <summary>
        /// Main credential object for making calls to the Liberty server
        /// </summary>
        /// <param name="applicationID">Application ID given to you by ResaleWorld</param>
        /// <param name="apiVersion">API Version being used</param>
        /// <param name="apiKey">API Key given to you by ResaleWorld</param>
        /// <param name="serverName">Name of Liberty server</param>
        /// <param name="listenPortNumber">Listen port (8080 for HTTP, 8081 for HTTPS)</param>
        public Credentials(string applicationID, string apiVersion, string apiKey, string serverName, string listenPortNumber = "8080")
        {
            this.Cookie = new CookieContainer();
            this.ApplicationID = applicationID;
            this.ApiVersion = apiVersion;
            this.ApiKey = apiKey;
            this.ServerName = serverName;
            this.ListenPortNumber = listenPortNumber;

            var timer = new System.Threading.Timer(e => KeepSessionAlive(), null, TimeSpan.Zero, TimeSpan.FromMinutes(4));
        }

        private async void KeepSessionAlive()
        {
            if (KeepSessionOpen)
            {
                if (User.Username == null || User.Password.Length == 0)
                {
                    Console.WriteLine("Auth object must have nested user object to keep alive");
                }
                else
                {
                    AsyncLoginCall asyncLoginCall = new AsyncLoginCall(User.Username, User.Password);
                    await asyncLoginCall.ExecuteAsync(this, false);
                    Console.WriteLine("Session kept open");
                }
            }
        }

        internal string AuthString(int callTime)
        {
            return LibertyConsignmentAPI.Hashes.MD5.Convert(ApplicationID + ":" + ApiKey + ":" + callTime);
        }

        public void PingTest()
        {
            var client = new RestClient("http://" + ServerName + ":" + ListenPortNumber + "/ping");
            var request = new RestRequest("", Method.GET);
            IRestResponse response = client.Execute(request);
            var content = response.Content;
            Console.WriteLine(content);
        }

        public void EchoTest(string echoString = "echothistome")
        {
            var client = new RestClient("http://" + ServerName + ":" + ListenPortNumber + "/test?" + echoString);
            var request = new RestRequest("", Method.GET);
            IRestResponse response = client.Execute(request);
            var content = response.Content;
            Console.WriteLine(content);
        }
    }

    public static class Cipher
    {
        internal static String SecureStringToString(SecureString value)
        {
            IntPtr valuePtr = IntPtr.Zero;
            try
            {
                valuePtr = Marshal.SecureStringToGlobalAllocUnicode(value);
                return Marshal.PtrToStringUni(valuePtr);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
            }
        }

        /// <summary>
        /// Encrypt a string.
        /// </summary>
        /// <param name="plainText">String to be encrypted</param>
        /// <param name="password">Password</param>
        public static string Encrypt(string plainText, string password)
        {
            if (plainText == null)
            {
                return null;
            }

            if (password == null)
            {
                password = String.Empty;
            }

            // Get the bytes of the string
            var bytesToBeEncrypted = Encoding.UTF8.GetBytes(plainText);
            var passwordBytes = Encoding.UTF8.GetBytes(password);

            // Hash the password with SHA256
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

            var bytesEncrypted = Cipher.Encrypt(bytesToBeEncrypted, passwordBytes);

            return Convert.ToBase64String(bytesEncrypted);
        }

        /// <summary>
        /// Decrypt a string.
        /// </summary>
        /// <param name="encryptedText">String to be decrypted</param>
        /// <param name="password">Password used during encryption</param>
        /// <exception cref="FormatException"></exception>
        public static SecureString Decrypt(string encryptedText, string password)
        {
            if (encryptedText == null)
            {
                return null;
            }

            if (password == null)
            {
                password = String.Empty;
            }

            // Get the bytes of the string
            var bytesToBeDecrypted = Convert.FromBase64String(encryptedText);
            var passwordBytes = Encoding.UTF8.GetBytes(password);

            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

            var bytesDecrypted = Cipher.Decrypt(bytesToBeDecrypted, passwordBytes);

            var secure = new SecureString();
            foreach (char c in Encoding.UTF8.GetString(bytesDecrypted))
            {
                secure.AppendChar(c);
            }
            return secure;
        }

        public static string Decrypt(string encryptedText, string password, bool stringOutput)
        {
            if (encryptedText == null)
            {
                return null;
            }

            if (password == null)
            {
                password = String.Empty;
            }

            // Get the bytes of the string
            var bytesToBeDecrypted = Convert.FromBase64String(encryptedText);
            var passwordBytes = Encoding.UTF8.GetBytes(password);

            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

            var bytesDecrypted = Cipher.Decrypt(bytesToBeDecrypted, passwordBytes);

            return Encoding.UTF8.GetString(bytesDecrypted);
        }

        private static byte[] Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
        {
            byte[] encryptedBytes = null;

            // Set your salt here, change it to meet your flavor:
            // The salt bytes must be at least 8 bytes.
            var saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);

                    AES.KeySize = 256;
                    AES.BlockSize = 128;
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                        cs.Close();
                    }

                    encryptedBytes = ms.ToArray();
                }
            }

            return encryptedBytes;
        }

        private static byte[] Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
        {
            byte[] decryptedBytes = null;

            // Set your salt here, change it to meet your flavor:
            // The salt bytes must be at least 8 bytes.
            var saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);

                    AES.KeySize = 256;
                    AES.BlockSize = 128;
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);
                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                        cs.Close();
                    }

                    decryptedBytes = ms.ToArray();
                }
            }

            return decryptedBytes;
        }
    }
}
