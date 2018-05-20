using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using KeyLocker.Console;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KeyLocker.ConsoleTests
{
	[TestClass]
	public class ProgramTests
	{
		public TestContext TestContext { get; set; }

		[TestMethod]
		public void CreateFile_Success()
		{
			//Arrange
			Exception exception = null;
			string expectedFilePath = Path.Combine(TestContext.DeploymentDirectory, "newlocker.bin");
			CreateArguments arguments = new CreateArguments
			{
				LockerPath = expectedFilePath,
				Password = "password"
			};

			//Act
			try
			{
				Program.CreateFile(arguments);
			}
			catch (Exception e)
			{
				exception = e;
			}

			//Assert
			Assert.IsNull(exception, $"Was not expecting an exception [{exception}]");

			bool fileExists = File.Exists(expectedFilePath);
			Assert.IsTrue(fileExists, "Was expecting a locker file to exist");
		}

		[TestMethod]
		[DeploymentItem(@"..\..\TestFiles\lockerforadding.bin")]
		public void AddKey_Success()
		{
			//Arrange
			Exception exception = null;
			Locker locker = null;
			string lockerFilePath = Path.Combine(TestContext.DeploymentDirectory, "lockerforadding.bin");
			AddArguments arguments = new AddArguments
			{
				LockerPath = lockerFilePath,
				Password = "password",
				Key = "somekey",
				Value = "somevalue"
			};

			//Act
			try
			{
				Program.AddKey(arguments);
				locker = Program.GetLocker(arguments);
			}
			catch (Exception e)
			{
				exception = e;
			}

			//Assert
			Assert.IsNull(exception, $"Was not expecting an exception [{exception}]");

			Assert.IsNotNull(locker, "Was expecting to have a locker");
			Assert.AreEqual(1, locker.Keys.Count, "Was expecting to have 1 key");
			Assert.AreEqual("somekey", locker.Keys.First().Key, "key name mismatch");
			Assert.AreEqual("somevalue", locker.Keys.First().Value, "value name mismatch");
		}

		[TestMethod]
		[DeploymentItem(@"..\..\TestFiles\lockerforupdates.bin")]
		public void UpdateKey_Success()
		{
			//Arrange
			Exception exception = null;
			Locker locker = null;
			string lockerFilePath = Path.Combine(TestContext.DeploymentDirectory, "lockerforupdates.bin");
			UpdateArguments arguments = new UpdateArguments
			{
				LockerPath = lockerFilePath,
				Password = "password",
				Key = "somekey",
				Value = "anothervalue"
			};

			//Act
			try
			{
				Program.UpdateKey(arguments);
				locker = Program.GetLocker(arguments);
			}
			catch (Exception e)
			{
				exception = e;
			}

			//Assert
			Assert.IsNull(exception, $"Was not expecting an exception [{exception}]");

			Assert.IsNotNull(locker, "Was expecting to have a locker");
			Assert.AreEqual(1, locker.Keys.Count, "Was expecting to have 1 key");
			Assert.AreEqual("somekey", locker.Keys.First().Key, "key name mismatch");
			Assert.AreEqual("anothervalue", locker.Keys.First().Value, "value name mismatch");
		}

		[TestMethod]
		[DeploymentItem(@"..\..\TestFiles\lockerforremoving.bin")]
		public void RemoveKey_Success()
		{
			//Arrange
			Exception exception = null;
			Locker locker = null;
			string lockerFilePath = Path.Combine(TestContext.DeploymentDirectory, "lockerforremoving.bin");
			RemoveArguments arguments = new RemoveArguments
			{
				LockerPath = lockerFilePath,
				Password = "password",
				Key = "somekey"
			};

			//Act
			try
			{
				Program.RemoveKey(arguments);
				locker = Program.GetLocker(arguments);
			}
			catch (Exception e)
			{
				exception = e;
			}

			//Assert
			Assert.IsNull(exception, $"Was not expecting an exception [{exception}]");

			Assert.IsNotNull(locker, "Was expecting to have a locker");
			Assert.AreEqual(0, locker.Keys.Count, "Was expecting to have 1 key");
		}

		[TestMethod]
		[DeploymentItem(@"..\..\TestFiles\lockerfordisplay.bin")]
		public void DisplayKeys_Success()
		{
			//Arrange
			Exception exception = null;
			string lockerFilePath = Path.Combine(TestContext.DeploymentDirectory, "lockerfordisplay.bin");
			DisplayArguments arguments = new DisplayArguments
			{
				LockerPath = lockerFilePath,
				Password = "password"
			};

			//Act
			try
			{
				Program.DisplayKeys(arguments);
			}
			catch (Exception e)
			{
				exception = e;
			}

			//Assert
			Assert.IsNull(exception, $"Was not expecting an exception [{exception}]");
			
			//Check test output for proof that keys are displayed. Should show:
			//List of Keys and values
			//----------------------------------------
			//somekey somevalue
		}
	}
}