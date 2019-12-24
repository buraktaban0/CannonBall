
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityCommon.RuntimeCollections;
using UnityEditor;
using UnityEngine;

namespace UnityCommon.ResourceManagement.Editor
{
	public static class EditorResourceManager
	{

		[MenuItem("Resources/Cache Resource Paths")]
		public static void CacheResourcePaths()
		{

			var allResources = Resources.LoadAll("Preload");

			var paths = Resources.Load<StringCollection>("PreloadResources");

			if (paths == null)
			{
				paths = ScriptableObject.CreateInstance<StringCollection>();
				AssetDatabase.CreateAsset(paths, "Assets/Resources/PreloadResources.asset");
				AssetDatabase.SaveAssets();
			}
			else
			{
				paths.items = new List<string>();
			}


			foreach (var res in allResources)
			{
				string path = AssetDatabase.GetAssetPath(res);
				string resPath = path.Replace("\\", "/").Replace("Assets/Resources/Preload/", "");

				paths.items.Add(resPath);
			}

			EditorUtility.SetDirty(paths);

			AssetDatabase.SaveAssets();

			Resources.UnloadUnusedAssets();

		}
		


	}
}
