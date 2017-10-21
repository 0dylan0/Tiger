using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Security
{
    public interface IEncryptionService
    {
        string CreateHash(string data, string hashAlgorithm = "SHA1");

        string CreateHash(byte[] data, string hashAlgorithm = "SHA1");

        string CreateSaltKey(int size);

        string CreatePasswordHash(string password, string saltkey, string passwordFormat = "SHA1");

        string CreateBase64Password(string password, string passwordFormat = "MD5");

        string EncryptText(string plainText);

        string DecryptText(string cipherText);
    }
}
