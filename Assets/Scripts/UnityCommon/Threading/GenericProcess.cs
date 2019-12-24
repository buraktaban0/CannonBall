using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityCommon.Threading
{
	public class GenericProcess : Process
	{

		private Action action;

		public GenericProcess(Action action)
		{
			this.action = action;
		}


		public override void Run()
		{
			action?.Invoke();
		}
	}
}
