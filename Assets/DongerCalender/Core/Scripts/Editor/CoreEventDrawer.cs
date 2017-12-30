using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Donger.BuckeyeEngine{

	[CustomPropertyDrawer(typeof(CoreEvent))]
	public class CoreEventDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, property);
		
			//Draw Label.
			position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), GUIContent.none);
			
			var indent = EditorGUI.indentLevel;
			EditorGUI.indentLevel = 1;
		
			//Calculate Rects
			var nameRect = new Rect(position.x, position.y, 100, position.height);
			var dayRect = new Rect(position.x + 50, position.y, 50, position.height);
			var monthRect = new Rect(position.x + 100, position.y, 50, position.height);
			var yearRect = new Rect(position.x + 150, position.y, 50, position.height);

			// //Draw Fields
			GUI.enabled = false;
			EditorGUI.PropertyField(nameRect, property.FindPropertyRelative("Name"), GUIContent.none);
			EditorGUI.PropertyField(dayRect, property.FindPropertyRelative("Day"), GUIContent.none);
			EditorGUI.PropertyField(monthRect, property.FindPropertyRelative("Month"), GUIContent.none);
			EditorGUI.PropertyField(yearRect, property.FindPropertyRelative("Year"), GUIContent.none);
			GUI.enabled = true;

			//Set indent back to what it was.
			//EditorGUI.indentLevel = indent;
			
			EditorGUI.EndProperty();
		}
	}
}

