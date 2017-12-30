using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Donger.BuckeyeEngine{
	[CustomEditor(typeof(EventManager))]
	public class EventManagerEditor : Editor{
		EventManager _eventManager;
		void OnEnable()
		{
			_eventManager = (EventManager)target;
		}
		public override void OnInspectorGUI(){
			serializedObject.Update();
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("Selected Date", GUILayout.Width(100));

			var day = _eventManager.selectedDate.Day;
			var month = _eventManager.selectedDate.Month;
			var year = _eventManager.selectedDate.Year;

			var selectedDate = month + "/" + day + "/" + year;
			_eventManager.EventDate = selectedDate;

			EditorGUILayout.EndHorizontal();

			DrawDefaultInspector();
			serializedObject.ApplyModifiedProperties();
		}
    }

}
