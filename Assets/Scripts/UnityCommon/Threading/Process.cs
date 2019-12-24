using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityCommon.Threading
{
	public abstract class Process
	{
		public bool IsCompleted { get; internal set; }


		public abstract void Run();

	}
}
