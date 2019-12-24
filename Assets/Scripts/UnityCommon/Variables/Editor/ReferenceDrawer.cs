using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace UnityCommon.Variables.Editor
{

	[CustomPropertyDrawer(typeof(Reference), true)]
	public class ReferenceDrawer : PropertyDrawer
	{
		public readonly string[] options = { "Use Constant", "Use Variable" };


		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, property);

			position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

			var indent = EditorGUI.indentLevel;
			EditorGUI.indentLevel = 0;

			var ddRect = new Rect(position.x, position.y, 16, position.height);
			var valueRect = new Rect(position.x + 20, position.y, position.width - 20, position.height);


			var index = property.FindPropertyRelative("useConstant").boolValue ? 0 : 1;
			index = EditorGUI.Popup(ddRect, index, options);

			var useConstant = index == 0;

			property.FindPropertyRelative("useConstant").boolValue = useConstant;

			var labelWidth = EditorGUIUtility.labelWidth;
			if (useConstant)
			{
				EditorGUIUtility.labelWidth = 16;
				EditorGUI.PropertyField(valueRect, property.FindPropertyRelative("constantValue"), new GUIContent(" "));
			}
			else
			{
				EditorGUI.PropertyField(valueRect, property.FindPropertyRelative("variable"), GUIContent.none);
			}

			EditorGUIUtility.labelWidth = labelWidth;

			EditorGUI.indentLevel = indent;

			EditorGUI.EndProperty();
		}

	}

}
