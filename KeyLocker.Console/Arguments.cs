using CommandLine;

namespace KeyLocker.Console
{
	public class GlobalArguments
	{
		[Option('f', "filepath", Required = true, HelpText = "The path to the locker file")]
		public string LockerPath { get; set; }

		[Option('p', "password", Required = true, HelpText = "The password to unencrypt the locker file")]
		public string Password { get; set; }

		[Option('s', "salt", Required = false, HelpText = "The salt used for encryption operations")]
		public string Salt { get; set; } = "12345678";

		[Option('i', "iterations", Required = false, HelpText = "The number of iterations used to generate encryption key")]
		public int Iterations { get; set; } = 1024;
	}

	[Verb("create", HelpText = "Create a new locker with optional first key")]
	public class CreateArguments : GlobalArguments
	{
		[Option('k', "keyname", Required = false, HelpText = "The name of the key to be added to the file")]
		public string Key { get; set; }

		[Option('v', "keyvalue", Required = false, HelpText = "The value of the key to be added to the file")]
		public string Value { get; set; }
	}

	[Verb("add", HelpText = "Add a key to the locker")]
	public class AddArguments : GlobalArguments
	{
		[Option('k', "keyname", Required = true, HelpText = "The name of the key to be added to the file")]
		public string Key { get; set; }

		[Option('v', "keyvalue", Required = true, HelpText = "The value of the key to be added to the file")]
		public string Value { get; set; }
	}

	[Verb("update", HelpText = "Update a key's value in the locker")]
	public class UpdateArguments : GlobalArguments
	{
		[Option('k', "keyname", Required = true, HelpText = "The name of the key to be added to the file")]
		public string Key { get; set; }

		[Option('v', "keyvalue", Required = true, HelpText = "The value of the key to be updated in the file")]
		public string Value { get; set; }
	}

	[Verb("remove", HelpText = "Remove a key from the locker")]
	public class RemoveArguments : GlobalArguments
	{
		[Option('k', "keyname", Required = true, HelpText = "The name of the key to be added to the file")]
		public string Key { get; set; }
	}

	[Verb("display", HelpText = "Display keys and values stored in the locker")]
	public class DisplayArguments : GlobalArguments
	{
	}
}
