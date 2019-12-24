using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityCommon.RuntimeCollections;
using UnityEngine;

namespace UnityCommon.ResourceManagement
{
	public static class ResourceManager
	{

		private static List<UnityEngine.Object> allResources = new List<UnityEngine.Object>();

		private static Dictionary<Type, Pool> pools = new Dictionary<Type, Pool>();

		private static Dictionary<Type, List<UnityEngine.Object>> loadedResources
			= new Dictionary<Type, List<UnityEngine.Object>>();


		private static Dictionary<string, UnityEngine.Object> preloadedResources = new Dictionary<string, UnityEngine.Object>();
		private static Dictionary<string, List<UnityEngine.Object>> instances = new Dictionary<string, List<UnityEngine.Object>>();

		public static void SetupPool<T>(int size) where T : IPoolObject, new()
		{
			var type = typeof(T);

			if (pools.ContainsKey(type))
			{
				Debug.Log($"Tried to setup pool of type {type} that already exists.");
				return;
			}

			DefaultPoolObjectFactory<T> factory = new DefaultPoolObjectFactory<T>();
			Pool<T> pool = new Pool<T>(size, factory);
			pools[type] = pool;
		}

		public static void SetupPool<T>(int size, PoolObjectFactory<T> factory) where T : IPoolObject
		{
			var type = typeof(T);

			if (pools.ContainsKey(type))
			{
				Debug.Log($"Tried to setup pool of type {type} that already exists.");
				return;
			}

			Pool<T> pool = new Pool<T>(size, factory);
			pools[type] = pool;
		}

		public static T Recycle<T>() where T : IPoolObject, new()
		{
			var type = typeof(T);
			if (pools.ContainsKey(type) == false)
			{
				throw new KeyNotFoundException($"Tried to recycle object of type {type}, but the pool does not exist.");
			}

			return ((Pool<T>)pools[type]).Recycle();
		}

		public static void Return<T>(T obj) where T : IPoolObject, new()
		{
			var type = typeof(T);
			if (pools.ContainsKey(type) == false)
			{
				throw new KeyNotFoundException($"Tried to return object of type {type}, but the pool does not exist.");
			}

			((Pool<T>)pools[type]).Return(obj);
		}


		public static void PreloadResources()
		{
			StringCollection resPaths = Resources.Load<StringCollection>("PreloadResources");

			foreach (var path in resPaths.items)
			{
				var fullPath = "Preload/" + path;


			}

		}

		public static IEnumerator PreloadResourcesAsync()
		{
			StringCollection resPaths = Resources.Load<StringCollection>("PreloadResources");

			yield return null;

			foreach (var path in resPaths.items)
			{
				preloadedResources[path] = Resources.Load("Preload/" + path);

				yield return null;
			}

		}


		public static T Instantiate<T>(string resPath) where T : UnityEngine.Object
		{
			if (!preloadedResources.ContainsKey(resPath))
				return default;

			var res = preloadedResources[resPath];

			var ins = GameObject.Instantiate(res);

			var instances = ResourceManager.instances[resPath];
			instances.Add(ins);

			return (T)ins;

		}


		public static void LoadAllResources()
		{
			allResources = new List<UnityEngine.Object>(Resources.LoadAll(""));
		}




		public static T[] GetResources<T>()
		{
			return allResources.Where(res => res is T).OfType<T>().ToArray();
		}



		public static T GetResource<T>(string name) where T : UnityEngine.Object
		{
			var type = typeof(T);
			bool containsType = loadedResources.ContainsKey(type);
			if (containsType)
			{
				var res = loadedResources[type].Where(r => r.name == name).FirstOrDefault() as T;
				if (res != default)
					return res;
			}

			var loadedTs = Resources.LoadAll<T>("");

			if (containsType)
			{
				foreach (var r in loadedTs)
				{
					var loadedTResources = loadedResources[type];
					if (!loadedTResources.Contains(r))
					{
						loadedTResources.Add(r);
					}
				}
			}
			else
			{
				loadedResources[type] = new List<UnityEngine.Object>(loadedTs);
			}

			return loadedTs.Where(r => r.name == name).FirstOrDefault();
		}


	}
}
