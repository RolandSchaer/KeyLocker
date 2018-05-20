using System;
using System.Collections.Generic;

namespace KeyLocker
{
	/// <summary>
	/// Used to hold keys in place of KeyValuePair which are readonly and can cause problems
	/// in some usage scenarios such as winforms gridviews
	/// </summary>
	[Serializable]
	public class LockerKey
    {
		public string Key { get; set; }
		public string Value { get; set; }
    }
}
