namespace KeyLocker
{
	public static class Extensions
    {
		/// <summary>
		/// Compare to another byte array to determin if they are equal 
		/// </summary>
		/// <param name="source">The source.</param>
		/// <param name="target">The target.</param>
		/// <returns>bool</returns>
		public static bool AreEqual(this byte[] source, byte[] target)
		{
			bool result = false;
			if(target != null && source.Length == target.Length)
			{
				int index = 0;
				while(index < source.Length && source[index] == target[index])
				{
					index++;	
				}
				result = index == source.Length;
			}
			return result;
		}

		public static string ToBase64(this string source)
		{
			return System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(source));
		}

		public static string FromBase64(this string source)
		{
			return System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(source));
		}

		public static byte[] ToBytes(this string source)
		{
			return System.Text.Encoding.UTF8.GetBytes(source);
		}

		public static string ValueToString(this byte[] source)
		{
			return System.Text.Encoding.UTF8.GetString(source);
		}
	}
}
