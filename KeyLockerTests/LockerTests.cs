using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KeyLocker;

namespace KeyLockerTests
{
	[TestClass]
	public class LockerTests
	{
		public TestContext TestContext { get; set; }

		private Locker<List<LockerKey>> CreateAndSaveLockerWithOneKey(string lockerPath, string password, byte[] salt, string key, string value)
		{
			AESEncryptor encryptor = new AESEncryptor(salt, 1234);
			GenericBinarySerializer<List<LockerKey>> serializer = new GenericBinarySerializer<List<LockerKey>>();

			var keyLocker = new Locker<List<LockerKey>>(encryptor, serializer, password);
			keyLocker.Keys.Add(new LockerKey { Key = key, Value = value });
			keyLocker.Save(lockerPath);
			return keyLocker;
		}

		[TestMethod]
		public void Locker_Create()
		{
			//Arrange
			string expectedFilePath = Path.Combine(TestContext.DeploymentDirectory, "newtestlocker.bin");
			Locker<List<LockerKey>> keyLocker = null;
			Exception exception = null;
			byte[] salt = { 1, 2, 3, 4, 5, 6, 7, 8 };

			//Act
			try
			{
				keyLocker = CreateAndSaveLockerWithOneKey(expectedFilePath, "password", salt, "first", "one");
			}
			catch(Exception e)
			{
				exception = e;
			}

			//Assert
			Assert.IsNull(exception, "Was not expecting an exception");
			Assert.IsNotNull(keyLocker.FilePath, "Was expecting filepath to have a value");
			Assert.IsNotNull(keyLocker.LockerKey, "Was expecting to have a key");
			Assert.AreEqual(expectedFilePath, keyLocker.FilePath, "Was expecting file paths to be the same");

			bool fileExists = File.Exists(expectedFilePath);
			Assert.IsTrue(fileExists, "Was expecting a locker file to exist");
		}

		/// <summary>
		/// Lockers the read.
		/// </summary>
		[TestMethod]
		public void Locker_Read()
		{
			//Arrange
			string testFilePath = Path.Combine(TestContext.DeploymentDirectory, "existingtestlocker.bin");
			Locker<List<LockerKey>> keyLocker = null;
			Exception exception = null;
			byte[] salt = { 1, 2, 3, 4, 5, 6, 7, 8 };
			AESEncryptor encryptor = new AESEncryptor(salt, 1234);
			GenericBinarySerializer<List<LockerKey>> serializer = new GenericBinarySerializer<List<LockerKey>>();

			//Act
			try
			{
				CreateAndSaveLockerWithOneKey(testFilePath, "password", salt, "first", "one");
				keyLocker = new Locker<List<LockerKey>>(encryptor, serializer, "password");
				keyLocker.Open(testFilePath);
			}
			catch(Exception e)
			{
				exception = e;
			}

			//Assert
			Assert.IsNull(exception, $"Was not expecting an exception [{exception?.Message}]");

			Assert.IsNotNull(keyLocker.Keys);
			Assert.AreEqual(1, keyLocker.Keys.Count, "was expecting 1 key");
			var key = keyLocker.Keys.FirstOrDefault(k => k.Key == "first");
			Assert.AreEqual("one", key.Value, "Mismatched first value");
		}

		/// <summary>
		/// Lockers the update.
		/// </summary>
		[TestMethod]
		public void Locker_Update()
		{
			//Arrange
			string testFilePath = Path.Combine(TestContext.DeploymentDirectory, "updatetestlocker.bin");
			Locker<List<LockerKey>> keyLocker = null;
			byte[] salt = { 1, 2, 3, 4, 5, 6, 7, 8 };
			AESEncryptor encryptor = new AESEncryptor(salt, 1234);
			GenericBinarySerializer<List<LockerKey>> serializer = new GenericBinarySerializer<List<LockerKey>>();
			Exception exception = null;

			//Act
			try
			{
				CreateAndSaveLockerWithOneKey(testFilePath, "password", salt, "first", "one");
				keyLocker = new Locker<List<LockerKey>>(encryptor, serializer, "password", testFilePath);
				keyLocker.Open();
				keyLocker.Keys.Add(new LockerKey { Key = "second", Value = "two" });
				keyLocker.Save();

				keyLocker = new Locker<List<LockerKey>>(encryptor, serializer, "password", testFilePath);
				keyLocker.Open();
			}
			catch(Exception e)
			{
				exception = e;
			}

			//Assert
			Assert.IsNull(exception, $"Was not expecting an exception [{exception?.Message}]");

			Assert.IsNotNull(keyLocker.Keys);
			Assert.AreEqual(2, keyLocker.Keys.Count, "was expecting 2 keys");
			var key = keyLocker.Keys.FirstOrDefault(k => k.Key == "first");
			Assert.AreEqual("one", key.Value, "Mismatched first value");
			key = keyLocker.Keys.FirstOrDefault(k => k.Key == "second");
			Assert.AreEqual("two", key.Value, "Mismatched second value");
		}
	}
}
