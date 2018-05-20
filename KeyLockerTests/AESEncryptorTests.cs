using System;
using System.Security.Cryptography;
using System.Text;
using KeyLocker;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KeyLockerTests
{
	[TestClass]
	public class AESEncryptorTests
	{
		#region Encryption

		/// <summary>
		/// Proves byte[] EncryptToByte(byte[] source);
		/// </summary>
		[TestMethod]
		public void Encrypt_Success()
		{
			//Arrange
			string password = "password";
			string expectedResult = "nQaemyTbqIii/fAy69BLYpo9jl64stTDlRNy4s1d8uM=";
			string testData = "testing";
			byte[] salt = { 1, 2, 3, 4, 5, 6, 7, 8 };

			//Act
			AESEncryptor encryptor = new AESEncryptor(salt, 1234);
			byte[] actualResult = encryptor.Encrypt(testData.ToBytes(), password);

			//Assert
			Assert.AreEqual(CipherMode.CBC, encryptor.CipherMode, "Mismatched CypherMode");
			Assert.AreEqual(PaddingMode.PKCS7, encryptor.PaddingMode, "Mismatched PaddingMode");

			Assert.AreEqual(expectedResult, Convert.ToBase64String(actualResult), "Results mismatch");
		}

		/// <summary>
		/// Proves process will work when Initialization Vector is not passed in
		/// </summary>
		[TestMethod]
		public void Encrypt_MissingKey()
		{
			//Arrange
			string testData = "testing";
			ArgumentNullException exception = null;
			Exception otherException = null;
			byte[] salt = { 1, 2, 3, 4, 5, 6, 7, 8 };

			//Act
			AESEncryptor encryptor = new AESEncryptor(salt, 1234);
			try
			{
				encryptor.Encrypt(testData.ToBytes(), null);
			}
			catch (ArgumentNullException e)
			{
				exception = e;
			}
			catch (Exception e)
			{
				otherException = e;
			}

			//Assert
			Assert.IsNull(otherException, $"Was expecting an ArgumentNullException [{otherException}]");
			Assert.IsNotNull(exception, "Was expecting an ArgumentNullException exception");
			Assert.AreEqual(exception.ParamName, "password", "Was expecting exception to be for password parameter");
		}

		#endregion

		#region Decryption

		/// <summary>
		/// Proves byte[] DecryptToByte(byte[] source, byte[] key);
		/// </summary>
		[TestMethod]
		public void Decrypt_Success()
		{
			string password = "password";
			string encryptedMessage = "nQaemyTbqIii/fAy69BLYpo9jl64stTDlRNy4s1d8uM=";
			string expectedResult = "testing";
			byte[] salt = { 1, 2, 3, 4, 5, 6, 7, 8 };

			//Act
			AESEncryptor encryptor = new AESEncryptor(salt, 1234);
			byte[] actualResult = encryptor.Decrypt(Convert.FromBase64String(encryptedMessage), password);

			//Assert
			Assert.IsTrue(actualResult.AreEqual(expectedResult.ToBytes()), "Expected results to match");
		}

		/// <summary>
		/// Proves byte[] DecryptToByte(byte[] source, byte[] key);
		/// </summary>
		[TestMethod]
		public void Decrypt_Missingkey()
		{
			string encryptedMessage = "njkP6V+HTbQW2a9nmtAtvnN70XEgWTWUypOMIMu1PtU=";
			ArgumentNullException exception = null;
			Exception otherException = null;
			byte[] salt = { 1, 2, 3, 4, 5, 6, 7, 8 };

			//Act
			AESEncryptor encryptor = new AESEncryptor(salt, 1234);
			try
			{
				byte[] actualResult = encryptor.Decrypt(Convert.FromBase64String(encryptedMessage), null);
			}
			catch (ArgumentNullException e)
			{
				exception = e;
			}
			catch (Exception e)
			{
				otherException = e;
			}

			//Assert
			Assert.IsNull(otherException, $"Was expecting an ArgumentNullException [{otherException}]");
			Assert.IsNotNull(exception, "Was expecting an ArgumentNullException exception");
			Assert.AreEqual(exception.ParamName, "password", "Was expecting exception to be for password parameter");
		}

		#endregion

		#region Salt And Iterations

		/// <summary>
		/// Proves process works with salt passed in as a parameter
		/// </summary>
		[TestMethod]
		public void Encrypt_AlternateSalt()
		{
			//Arrange
			string password = "password";
			string testData = "testing";
			string expectedResult = "8ZH453MWfrdkWyiYjscqH1tDSmLwDaq6Uvif1a3j7tg=";
			byte[] salt = { 2, 2, 2, 2, 2, 2, 2, 2 };

			//Act
			AESEncryptor encryptor = new AESEncryptor(salt, 1234);
			byte[] encryptedBytes = encryptor.Encrypt(testData.ToBytes(), password);
			byte[] decryptedBytes = encryptor.Decrypt(encryptedBytes, password);

			//Assert

			Assert.AreEqual(expectedResult, Convert.ToBase64String(encryptedBytes), "Results mismatch");
			Assert.AreEqual(testData, Encoding.UTF8.GetString(decryptedBytes), "Results mismatch");
		}

		/// <summary>
		/// Proves process works with salt passed in as a parameter
		/// </summary>
		[TestMethod]
		public void Encrypt_AlternateIterations()
		{
			//Arrange
			string password = "password";
			string testData = "testing";
			string expectedResult = "p+AAj9R/fsqLKgoefdfbZCqgIUrLZOZvLET4ZOBfLcY=";
			byte[] salt = { 1, 2, 3, 4, 5, 6, 7, 8 };

			//Act
			AESEncryptor encryptor = new AESEncryptor(salt, 2000);
			byte[] encryptedBytes = encryptor.Encrypt(testData.ToBytes(), password);
			byte[] decryptedBytes = encryptor.Decrypt(encryptedBytes, password);

			//Assert

			Assert.AreEqual(expectedResult, Convert.ToBase64String(encryptedBytes), "Results mismatch");
			Assert.AreEqual(testData, Encoding.UTF8.GetString(decryptedBytes), "Results mismatch");
		}

		#endregion
	}
}
