using System;
using System.Collections.Generic;
using CommandLine;
using KeyLocker;

namespace KeyLocker.Console
{
	public static class Program
	{
		static void Main(string[] args)
		{
			Parser.Default.ParseArguments<CreateArguments, AddArguments, UpdateArguments, RemoveArguments, DisplayArguments>(args)
				.MapResult(
				(CreateArguments opts) => CreateFile(opts),
				(AddArguments opts) => AddKey(opts),
				(UpdateArguments opts) => UpdateKey(opts),
				(RemoveArguments opts) => RemoveKey(opts),
				(DisplayArguments opts) => DisplayKeys(opts),
				errs => 1
				);
		}

		public static int CreateFile(CreateArguments arguments)
		{
			int result = 0;
			IEncryptor encryptor = new AESEncryptor(arguments.Salt.ToBytes(), arguments.Iterations);
			GenericBinarySerializer<Dictionary<string, string>> serializer = new GenericBinarySerializer<Dictionary<string, string>>();
			Locker locker = new Locker(encryptor, serializer, arguments.Password, arguments.LockerPath);
			// If ia key name is passed in add the kay to the keys list before saving
			if (String.IsNullOrWhiteSpace(arguments.Key) == false)
			{
				locker.Keys.Add(arguments.Key, arguments.Value);
			}
			locker.Save();
			return result;
		}

		public static int AddKey(AddArguments arguments)
		{
			int result = 0;
			Locker locker = GetLocker(arguments);
			locker.Keys.Add(arguments.Key, arguments.Value);
			locker.Save();
			return result;
		}

		public static int UpdateKey(UpdateArguments arguments)
		{
			int result = 0;
			Locker locker = GetLocker(arguments);
			try
			{
				locker.Keys[arguments.Key] = arguments.Value;
				locker.Save();
			}
			catch (Exception e)
			{
				System.Console.WriteLine(e);
				result = 1;
			}
			return result;
		}

		public static int RemoveKey(RemoveArguments arguments)
		{
			int result = 0;
			Locker locker = GetLocker(arguments);
			try
			{
				locker.Keys.Remove(arguments.Key);
				locker.Save();
			}
			catch (Exception e)
			{
				System.Console.WriteLine(e);
				result = 1;
			}
			return result;
		}

		public static int DisplayKeys(DisplayArguments arguments)
		{
			int result = 0;
			Locker locker = GetLocker(arguments);
			System.Console.WriteLine("List of Keys and values");
			System.Console.WriteLine("----------------------------------------");
			foreach (var pair in locker.Keys)
			{
				System.Console.WriteLine($"{pair.Key}     {pair.Value}");
			}
			return result;
		}

		public static Locker GetLocker(GlobalArguments arguments)
		{
			IEncryptor encryptor = new AESEncryptor(arguments.Salt.ToBytes(), arguments.Iterations);
			GenericBinarySerializer<Dictionary<string, string>> serializer = new GenericBinarySerializer<Dictionary<string, string>>();
			Locker locker = new Locker(encryptor, serializer, arguments.Password, arguments.LockerPath);
			locker.Open();
			return locker;
		}
	}
}
