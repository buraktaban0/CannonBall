using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityCommon.Logging;
using UnityEngine;

namespace UnityCommon.Singletons
{
	public static class SingletonBehaviour
	{

		public static T GetInstance<T>() where T : Behaviour
		{
			var instance = GameObject.FindObjectOfType<T>();

			if (instance == null)
			{
				var obj = new GameObject(typeof(T) + "_Instance");
				GameObject.DontDestroyOnLoad(obj);
				instance = obj.AddComponent<T>();
			}

			return instance;
		}

	}

	public abstract class SingletonBehaviour<T> : MonoBehaviour where T : SingletonBehaviour<T>
	{

		private class Logger : Logging.ILogger
		{
			public string GetLoggerIdentity()
			{
				return "SingletonBehaviour<" + typeof(T) + ">";
			}
		}

		private static Logger logger = new Logger();

		private static T instance;
		public static T Instance
		{
			get
			{
				if (instance == null)
				{
					instance = FindObjectOfType<T>();

					if (instance == null)
					{
						Log.WriteLine("Instance of type " + typeof(T) + " was not existent, creating new", logger);

						var obj = new GameObject(typeof(T) + "_Instance");
						DontDestroyOnLoad(obj);
						instance = obj.AddComponent<T>();
					}
				}

				return instance;
			}

			protected set
			{
				instance = value;
			}
		}

	}
}
