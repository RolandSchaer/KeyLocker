using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace KeyLocker
{
	/// <summary>
	/// Generic Binary serializer to serialize or deserialize any serializable type
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <seealso cref="KeyLocker.ISerializer{T}" />
	public class GenericBinarySerializer<T> : ISerializer<T>
	{
		/// <summary>
		/// Serializes the specified type instance into a byte[]
		/// </summary>
		/// <param name="source">A instance of the defined type</param>
		/// <returns>byte[]</returns>
		public byte[] Serialize(T source)
		{
			byte[] result = null;
			if (source != null)
			{
				using (MemoryStream memoryStream = new MemoryStream())
				{
					BinaryFormatter binaryFormatter = new BinaryFormatter();
					binaryFormatter.Serialize(memoryStream, source);
					result = memoryStream.ToArray();
				}
			}
			return result;
		}

		/// <summary>
		/// Deserializes byte[] into an instance of the specified type
		/// </summary>
		/// <param name="source">A byte[]</param>
		/// <returns>An instance of the defined type</returns>
		public T DeSerialize(byte[] source)
		{
			T result;
			if (source == null) throw new ArgumentNullException("source");
			using (MemoryStream memoryStream = new MemoryStream())
			{
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				memoryStream.Write(source, 0, source.Length);
				memoryStream.Seek(0, SeekOrigin.Begin);
				result = (T) binaryFormatter.Deserialize(memoryStream);
			}
			return result;
		}
	}
}
