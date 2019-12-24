using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityCommon.ResourceManagement
{
	public abstract class PoolObjectFactory<T> : ObjectFactory<T> where T : IPoolObject
	{
		public abstract override T Produce();
	}
}
