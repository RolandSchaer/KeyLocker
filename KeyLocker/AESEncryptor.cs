using System;
using System.IO;
using System.Security.Cryptography;

namespace KeyLocker
{
	/// <summary>
	/// Reference Encryptor class that implements Iencryptor for AES
	/// </summary>
	/// <seealso cref="KeyLocker.IEncryptor" />
	public class AESEncryptor : IEncryptor
	{
		/// <summary>
		/// The <see cref="CipherMode" /> used to perform encryption/decryption operations
		/// </summary>
		public CipherMode CipherMode { get; set; } = CipherMode.CBC;

		/// <summary>
		/// The <see cref="PaddingMode"/> used to perform encryption/devryption operations
		/// </summary>
		public PaddingMode PaddingMode { get; set; } = PaddingMode.PKCS7;

		private readonly int _Iterations;
		private readonly byte[] _Salt;

		/// <summary>
		/// Initializes a new instance of the <see cref="AESEncryptor" /> class.
		/// </summary>
		/// <param name="salt">The byte value of at least 8 bytes used to generate encryption key</param>
		/// <param name="iterations">The number of iterations used to generate key</param>
		public AESEncryptor(byte[] salt, int iterations)
		{
			//only assign if input parameters not null
			_Iterations = iterations;
			_Salt = salt;
		}

		#region IEncryptor Implementation

		/// <summary>
		/// Encrypts a byte[] into an encrypted byte[] with non-encrypted IV embedded at beginning 
		/// </summary>
		/// <param name="source">The data to be encrypted</param>
		/// <param name="password">The password used to generate encryption key</param>
		/// <returns>
		/// An encrypted byte[] with non-encrypted iv embedded at beginning
		/// </returns>
		public byte[] Encrypt(byte[] source, string password)
		{
			byte[] result;
			if (string.IsNullOrWhiteSpace(password))
			{
				throw new ArgumentNullException("password");
			}

			// Instantiate the encryption process
			using (var aes = CreateAes())
			{
				// Generate the key and iv from the password
				(byte[] key, byte[] iv) = GetKeyandIv(aes, password);
				aes.Key = key;
				aes.IV = iv;

				byte[] cipherText;
				using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
				{
					//create streams that facilitate the decryption
					using (MemoryStream cypherStream = new MemoryStream())
					{
						using (CryptoStream cryptoStream = new CryptoStream(cypherStream, encryptor, CryptoStreamMode.Write))
						{
							cryptoStream.Write(source, 0, source.Length);
							cryptoStream.FlushFinalBlock();
							cipherText = cypherStream.ToArray();
						}
					}
				}

				//assemble encrypted source including IV prepended
				using (var encryptedStream = new MemoryStream())
				{
					using (var writer = new BinaryWriter(encryptedStream))
					{
						writer.Write(aes.IV);
						writer.Write(cipherText);
					}
					result = encryptedStream.ToArray();
				}
			}

			return result;
		}

		/// <summary>
		/// Decrypts an encrypted byte[] with non-encrypted IV embedded at beginning
		/// </summary>
		/// <param name="source">An encrypted byte[] with non-encrypted iv embedded at beginning</param>
		/// <param name="password">The password used to generate encryption key</param>
		/// <returns>
		/// A decrypted byte[]
		/// </returns>
		public byte[] Decrypt(byte[] source, string password)
		{
			byte[] result;
			if (string.IsNullOrWhiteSpace(password))
			{
				throw new ArgumentNullException("password");
			}
			using (var aes = CreateAes())
			{
				// Generate the key from password. IV is embedded in at beginning of data stream.
				(byte[] key, byte[] iv) keys = GetKeyandIv(aes, password);
				aes.Key = keys.key;

				// Extract IV header from the encrypted source
				int ivLength = aes.BlockSize / 8;
				byte[] iv = new byte[ivLength];
				Array.Copy(source, 0, iv, 0, iv.Length);
				aes.IV = iv;

				// Remove the IV from the byte array
				byte[] data = new byte[source.Length - iv.Length];
				Array.Copy(source, iv.Length, data, 0, source.Length - iv.Length);

				// Create the decryptor that will perform decryption operations
				using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
				{
					// Create memoryStream to hold decrypted data
					using (MemoryStream decrypted = new MemoryStream())
					{
						//create stream with encrypted data
						using (MemoryStream encrypted = new MemoryStream(data))
						{
							//Create crypto stream that will decrypt the data
							using (CryptoStream cryptoStream = new CryptoStream(encrypted, decryptor, CryptoStreamMode.Read))
							{
								// Decrypt data in block size chunks
								byte[] buffer = new byte[aes.BlockSize];
								int count;
								while ((count = cryptoStream.Read(buffer, 0, buffer.Length)) > 0)
								{
									decrypted.Write(buffer, 0, count);
								}
								result = decrypted.ToArray();
							}
						}
					}
				}
			}
			return result;
		}

		#endregion

		/// <summary>
		/// Use to get standardized AES for all encryption operations
		/// </summary>
		/// <returns><see cref="AesManaged"/></returns>
		private AesManaged CreateAes()
		{
			return new AesManaged
			{
				Mode = CipherMode,
				Padding = PaddingMode,
				KeySize = 256,
				BlockSize = 128
			};
		}

		private (byte[] key, byte[] iv) GetKeyandIv(AesManaged aes, string password)
		{
			int keySize = aes.KeySize / 8;
			int ivSize = aes.BlockSize / 8;

			// Using some number other that default of 1000 for iteration count
			Rfc2898DeriveBytes derivedBytes = new Rfc2898DeriveBytes(password, _Salt, _Iterations);
			var key = derivedBytes.GetBytes(keySize);
			var iv = derivedBytes.GetBytes(ivSize);

			return (key: key, iv: iv);
		}
	}
}
