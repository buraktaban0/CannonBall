using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEditor;
using UnityEngine;

namespace UnityCommon.Events.Editor
{
	[CustomEditor(typeof(GameEvent))]
	public class GameEventEditor : UnityEditor.Editor
	{

		GameEvent gameEvent;

		private void OnEnable()
		{
			gameEvent = (GameEvent)target;
		}


		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			GUILayout.Space(20);

			if (GUILayout.Button("Raise"))
			{
				gameEvent.Raise(this);
			}

		}

	}
}
