﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityCommon.ResourceManagement
{
	public abstract class ObjectFactory<T>
	{
		public abstract T Produce();

	}
}
