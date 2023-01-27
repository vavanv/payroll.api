using System;
using System.Security.Cryptography;
using System.Text;

namespace Payroll2.Api.Services.Common
{
    internal static class HashPassword
    {
        public static string GetHashPassword(string plainPassword)
        {
            var crypt = new SHA256Managed();
            var crypto = crypt.ComputeHash(Encoding.ASCII.GetBytes(plainPassword), 0,
                Encoding.ASCII.GetByteCount(plainPassword));
            return BytesToString(crypto);
        }

        private static string BytesToString(byte[] crypto)
        {
            var sb = new StringBuilder();
            foreach (var b in crypto) sb.Append(b.ToString("x2"));

            return sb.ToString();
        }
    }
}