namespace KeyLocker
{
	public interface ISerializer<T>
	{
		byte[] Serialize(T source);
		T DeSerialize(byte[] source);
	}
}
