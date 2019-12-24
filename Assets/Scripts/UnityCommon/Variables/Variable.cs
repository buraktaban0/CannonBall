using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityCommon.Singletons;
using UnityEngine;
using UnityEngine.Events;

namespace UnityCommon.Variables
{

	public abstract class Variable : ScriptableObject
	{


		public abstract object GetValueAsObject();

		public abstract void SetValueAsObject(object obj);

	}

	public abstract class Variable<T> : Variable
	{

		public T value;

		public override object GetValueAsObject()
		{
			return value;
		}

		public override void SetValueAsObject(object obj)
		{
			Debug.Log(typeof(T) + "  " + obj.GetType());
			value = (T)obj;
		}


	}
}
