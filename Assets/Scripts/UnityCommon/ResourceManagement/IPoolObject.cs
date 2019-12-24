using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityCommon.ResourceManagement
{
	public interface IPoolObject
	{


		void OnPooled();
		void OnRecycled();


	}
}
