using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace UnityCommon.Procedural
{
	public abstract class Noise3D : ScriptableObject
	{

		public abstract float Sample(Vector3 v);

	}
}
