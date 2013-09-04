using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Encryption
{
    public class DefaultEncryptor: IEncryptor
    {
        public string MakeSha512Hash(string phrase)
        {
            var bytes = Encoding.UTF8.GetBytes(phrase);
            bytes = new SHA512CryptoServiceProvider().ComputeHash(bytes);
            var hashBuilder = new StringBuilder();

            foreach (byte b in bytes)
            {
                hashBuilder.Append(b.ToString("x2").ToLower());
            }

            return hashBuilder.ToString();
        }
    }
}
