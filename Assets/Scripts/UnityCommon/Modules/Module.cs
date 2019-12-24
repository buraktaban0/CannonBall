using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityCommon.Singletons;
using UnityEngine;

namespace UnityCommon.Modules
{

	public interface IModule
	{
		int ExecutionOrder { get; }

		void Start();
		void Stop();

		void Update();
		string GetName();
	}


	public abstract class Module : ScriptableObject, IModule
	{
		[Range(-5, 5)] [SerializeField] private int executionOrder;
		public int ExecutionOrder => executionOrder;

		public string GetName() => this.GetType().Name;

		public abstract void Start();
		public abstract void Stop();

		public abstract void Update();

	}

}
