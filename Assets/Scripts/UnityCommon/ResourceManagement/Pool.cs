using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityCommon.ResourceManagement
{


	public class Pool
	{

	}

	public class Pool<T> : Pool where T : IPoolObject
	{

		private Queue<T> objects;

		private int capacity;

		private int currentIndex = 0;

		private bool autoResize;

		private PoolObjectFactory<T> factory;


		public Pool(int capacity, PoolObjectFactory<T> factory, bool autoResize = true)
		{
			this.capacity = capacity;
			this.factory = factory;
			this.autoResize = autoResize;

			objects = new Queue<T>(capacity);

			Populate(capacity);
		}

		private void Populate(int count)
		{
			for (int i = 0; i < count; i++)
			{
				var obj = factory.Produce();
				obj.OnPooled();
				objects.Enqueue(obj);
			}
		}


		public T Recycle()
		{

			if (objects.Count < 1)
			{
				if (autoResize)
				{
					Populate(capacity);
					capacity *= 2;
				}
				else
				{
					throw new IndexOutOfRangeException($"Pool of type {typeof(T)}: All in use. Auto resize is disabled.");
				}
			}

			var obj = objects.Dequeue();
			obj.OnRecycled();
			return obj;

		}

		public void Return(T obj)
		{
			obj.OnPooled();
			objects.Enqueue(obj);
		}


	}
}
