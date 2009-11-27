using System;
using System.Security.Cryptography;
using System.Text;
using Membrane.Core.Services.Interfaces;

namespace Membrane.Core.Services
{
	public class EncryptionService : IEncryptionService
	{
		/// <summary>
		/// Method to Hash any given value.  
		/// </summary>
		/// <param name="valueToHash">What needs hashing?</param>
		/// <returns>Hashed value (string)</returns>
		public string Encrypt(string valueToHash)
		{
			SHA512 sha = new SHA512Managed();
			var hashed = string.Empty;
			if (valueToHash != null)
			{
				byte[] hash = sha.ComputeHash(Encoding.ASCII.GetBytes(valueToHash));
				hashed = BitConverter.ToString(hash);
			}
			return hashed;
		}
	}
}