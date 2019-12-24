using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace UnityCommon.Variables
{

	[Serializable]
	public abstract class Reference
	{
		[SerializeField] protected bool useConstant = true;

		public abstract object GetValue();

	}

	[Serializable]
	public abstract class Reference<T, T1> : Reference where T1 : Variable<T>
	{
		[SerializeField] protected T1 variable;

		[SerializeField] protected T constantValue;

		public T Value
		{
			get
			{
				if (useConstant)
					return constantValue;
				else
				{
					if (variable != null)
						return variable.value;
					else
					{
						Debug.Log($"Value reference of type {typeof(T)} is set to use a variable which is null. Returning constant.");
						return constantValue;
					}
				}
			}
			set { if (useConstant) { constantValue = value; } else { variable.value = value; } }
		}

		public override object GetValue()
		{
			return Value;
		}

	}
}
