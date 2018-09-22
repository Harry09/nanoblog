using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using Nanoblog.Api.Extensions;

namespace Nanoblog.Api.Encryption
{
    public static class Hasher
    {
		private const int DeriveBytesIterationsCount = 10_000;
		private const int SaltSize = 40;

		public static string GetSalt()
		{
			var saltBytes = new byte[SaltSize];
			var rng = RandomNumberGenerator.Create();
			rng.GetBytes(saltBytes);

			return Convert.ToBase64String(saltBytes);
		}

		public static string GetHash(string value, string salt)
		{
			if (value.Empty())
			{
				throw new ArgumentException("Can not generate hash from an empty value.", nameof(value));
			}
			if (salt.Empty())
			{
				throw new ArgumentException("Can not use an empty salt from hashing value.", nameof(value));
			}

			var pbkdf2 = new Rfc2898DeriveBytes(value, GetBytes(salt), DeriveBytesIterationsCount);

			return Convert.ToBase64String(pbkdf2.GetBytes(SaltSize));
		}

		private static byte[] GetBytes(string value)
		{
			var bytes = new byte[value.Length * sizeof(char)];
			Buffer.BlockCopy(value.ToCharArray(), 0, bytes, 0, bytes.Length);

			return bytes;
		}
	}
}
