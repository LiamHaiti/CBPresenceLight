using Microsoft.AspNetCore.Mvc;

using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CBPresenceLight.Controllers
{
    public class Functions : Controller
    {
        private readonly IConfiguration _config;

        public Functions(IConfiguration config)
        {
            _config = config;
        }

        public String Encrypt(String password)
        {
            string hash = @"foxle@rn";
            byte[] data = UTF8Encoding.UTF8.GetBytes(password);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
                using (TripleDESCryptoServiceProvider tripleDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = tripleDes.CreateEncryptor();
                    byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                    return Convert.ToBase64String(results);
                }
            }


        }

        public String Decrypt(String password)
        {

            if (!string.IsNullOrWhiteSpace(password))
            {

                string hash = @"foxle@rn";
                byte[] data = Convert.FromBase64String(password.Trim());
                using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
                {
                    byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
                    using (TripleDESCryptoServiceProvider tripleDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                    {
                        ICryptoTransform transform = tripleDes.CreateDecryptor();
                        byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                        return UTF8Encoding.UTF8.GetString(results);
                    }
                }

            }
            else
            {

                return "";

            }

        }



        public string GenerateRandomOTP(int OTPLength, string[] allowedCharacters)
        {

            string sOTP = String.Empty;
            string sTempChars = String.Empty;
            Random rand = new Random();

            for (int i = 0; i < OTPLength; i++)
            {

                int p = rand.Next(0, allowedCharacters.Length);
                sTempChars = allowedCharacters[rand.Next(0, allowedCharacters.Length)];
                sOTP += sTempChars;
            }
            return sOTP;
        }

        public double Distance(double latitude1, double latitude2, double longitude1, double longitude2)
        {
            double terreRayonMoyenKm = 6371.0;
            double dLatEnRadian = ConvertDegreToRadian(latitude1 - latitude2);
            double dLonEnRadian = ConvertDegreToRadian(longitude1 - longitude2);
            double distance = Math.Sin(dLatEnRadian / 2) * Math.Sin(dLatEnRadian / 2) +
                  Math.Cos(ConvertDegreToRadian(latitude1)) * Math.Cos(ConvertDegreToRadian(latitude2)) *
                  Math.Sin(dLonEnRadian / 2) * Math.Sin(dLonEnRadian / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(distance), Math.Sqrt(1 - distance));
            return terreRayonMoyenKm * c;


        }
        private static double ConvertDegreToRadian(double degree)
        {
            return degree * (Math.PI / 180.0);
        }
    }
}
