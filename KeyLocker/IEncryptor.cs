namespace KeyLocker
{
	public interface IEncryptor
	{
		byte[] Encrypt(byte[] source, string password);
		byte[] Decrypt(byte[] source, string password);
	}
}