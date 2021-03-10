using Ostium.BeforeIDie.API.Model.Settings;
using Ostium.BeforeIDie.API.Model.ValueObjects;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace Ostium.BeforeIDie.API.Model.Extensions
{
    public static class CrypExtension
    {
        public static string Encrypt(this string Texto)
        {
            byte[] Hash;

            using (HashAlgorithm Algoritmo = SHA256.Create())
            {
                Hash = Algoritmo.ComputeHash(Encoding.Unicode.GetBytes(Texto));
            }

            StringBuilder Retorno = new StringBuilder();

            foreach (byte B in Hash)
            {
                Retorno.AppendFormat(CultureInfo.InvariantCulture, "{0:X2}", B);
            }

            return Retorno.ToString();
        }

        public static string Encrypt(this string Texto, string chave, CryptValueObject provider)
        {
            CryptographySettings crip = new CryptographySettings(provider);
            crip.Key = chave;
            return crip.Encrypt(Texto);
        }

        public static string Decrypt(this string Texto, string chave, CryptValueObject provider)
        {
            CryptographySettings crip = new CryptographySettings(provider);
            crip.Key = chave;
            return crip.Decrypt(Texto);
        }
    }
}
