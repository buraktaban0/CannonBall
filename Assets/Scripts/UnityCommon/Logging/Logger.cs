using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityCommon.Logging
{
	public class Logger : ILogger
	{
		private string identity;

		public Logger(string identity)
		{
			this.identity = identity;

			if (identity == null)
				identity = "NotIdentified";
		}

		public string GetLoggerIdentity()
		{
			return identity;
		}
	}
}
