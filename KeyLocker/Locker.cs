using System.IO;

namespace KeyLocker
{
	public class Locker<T> where T : new()
	{
		public Locker(IEncryptor encryptor, ISerializer<T> serializer, string lockerKey = null, string filePath = null)
		{
			LockerKey = lockerKey;
			FilePath = filePath;
			Encryptor = encryptor;
			Serializer = serializer;
		}

		public string LockerKey { get; set; }
		public string FilePath { get; private set; }
		public IEncryptor Encryptor { get; }
		public ISerializer<T> Serializer { get; }
		public T Keys { get; private set; } = new T();

		public void Open(string filePath = null)
		{
			FilePath = filePath ?? FilePath;

			if(File.Exists(FilePath))
			{
				byte[] encryptedBytes;

				// Read the file
				using(FileStream fileStream = File.OpenRead(FilePath))
				{
					byte[] readBytes = new byte[fileStream.Length];
					fileStream.Read(readBytes, 0, readBytes.Length);
					encryptedBytes = readBytes;
				}

				// Decrypt data
				byte[] decryptedBytes = Encryptor.Decrypt(encryptedBytes, LockerKey);

				// deserialize data

				Keys = Serializer.DeSerialize(decryptedBytes);
			}
			else
			{
				throw new FileNotFoundException("File not found", filePath);
			}
		}

		public void Save(string filePath = null)
		{
			FilePath = filePath ?? FilePath;

			// Serialize the data
			byte[] serializedKeys = Serializer.Serialize(Keys);

			// Encrypt the data
			byte[] encryptedBytes = Encryptor.Encrypt(serializedKeys, LockerKey);

			// Write the file
			using(BinaryWriter writer = new BinaryWriter(File.Open(FilePath, FileMode.Create)))
			{
				writer.Write(encryptedBytes);
				writer.Flush();
				writer.Close();
			}
		}
	}
}
