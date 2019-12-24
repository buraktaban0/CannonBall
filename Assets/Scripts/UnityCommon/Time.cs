using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityCommon
{
	public static class Time
	{
		public static long UnixTimestamp => DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

	}
}
