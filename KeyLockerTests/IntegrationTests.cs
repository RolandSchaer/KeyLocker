using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using KeyLocker;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KeyLockerTests
{
	[TestClass]
	public class IntegrationTests
	{
		public TestContext TestContext { get; set; }

		#region Serialization

		[TestMethod]
		public void Serialize_Encrypt_RoundTrip()
		{
			List<KeyValuePair<string, string>> dictionary = new List<KeyValuePair<string, string>>();
			dictionary.Add(new KeyValuePair<string, string>("first", "one"));
			string key = "12345678901234567890123456789012";
			byte[] salt = { 1, 2, 3, 4, 5, 6, 7, 8 };
			GenericBinarySerializer<List<KeyValuePair<string, string>>> serializer = new GenericBinarySerializer<List<KeyValuePair<string, string>>>();
			AESEncryptor encryptor = new AESEncryptor(salt, 1234);

			//Act
			byte[] serializedData = serializer.Serialize(dictionary);

			byte[] encryptedData = encryptor.Encrypt(serializedData, key);

			byte[] decryptedData = encryptor.Decrypt(encryptedData, key);

			List<KeyValuePair<string, string>> deserializedData = serializer.DeSerialize(decryptedData);

			Assert.IsTrue(serializedData.AreEqual(decryptedData), "Was expecting serialized and decrypted data to be equal");
			Assert.AreEqual(dictionary.Count, deserializedData.Count, "Was expecting dictionaries to have 1 entry");
		}

		#endregion

		#region Locker

		/// <summary>
		/// Lockers the round trip.
		/// </summary>
		[TestMethod]
		public void Locker_RoundTrip()
		{
			//Arrange
			string expectedFilePath = Path.Combine(TestContext.DeploymentDirectory, "rttestlocker.bin");
			string passowrd = "password";
			Exception exception = null;
			byte[] salt = { 1, 2, 3, 4, 5, 6, 7, 8 };
			AESEncryptor encryptor = new AESEncryptor(salt, 1234);
			GenericBinarySerializer<List<KeyValuePair<string, string>>> serializer = new GenericBinarySerializer<List<KeyValuePair<string, string>>>();
			Locker<List<KeyValuePair<string, string>>> openLocker = null;

			//Act
			Locker<List<KeyValuePair<string, string>>> saveLocker = new Locker<List<KeyValuePair<string, string>>>(encryptor, serializer, passowrd);
			try
			{

				saveLocker.Keys.Add(new KeyValuePair<string, string>("first", "one"));
				saveLocker.Save(expectedFilePath);

				openLocker = new Locker<List<KeyValuePair<string, string>>>(encryptor, serializer, passowrd);
				openLocker.Open(expectedFilePath);
			}
			catch (Exception e)
			{
				exception = e;
			}

			//Assert
			Assert.IsNull(exception, $"Was not expecting an exception [{exception?.Message}]");
			bool fileExists = File.Exists(expectedFilePath);
			Assert.IsTrue(fileExists, "Was expecting a locker file to exist");
			Assert.IsNotNull(openLocker, "Was expecting openlocker to have a value");
			Assert.IsNotNull(openLocker.Keys);
			Assert.AreEqual(1, openLocker.Keys.Count, "was expecting 1 key");
			var key = openLocker.Keys.FirstOrDefault(k => k.Key == "first");
			Assert.AreEqual("one", key.Value, "Mismatched first value");
		}

		#endregion
	}
}
