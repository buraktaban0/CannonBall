using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace UnityCommon.Core.Editor
{
	public static class GameCreator
	{
		[MenuItem("Common/Create Game Manager")]
		public static void CreateGameManager()
		{
			var managerObj = new GameObject("Game");
			managerObj.AddComponent<Game>();
		}

	}
}
