using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace Ostium.BeforeIDie.Domain.Extensions
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
    }
}
