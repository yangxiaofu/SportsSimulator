﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Donger.BuckeyeEngine{
	[CustomEditor(typeof(EventManager))]
	public class EventManagerEditor : Editor{
		EventManager _eventManager;
		string _path;
		GUISkin _skin;
		CoreEventEditorWindow _coreEventEditorWindow;
		public bool CoreEventEditorWindowOpen = false;

		protected virtual void OnEnable()
		{
			_eventManager = (EventManager)target;
			_path = "Assets/DongerCalendar/Core/GUISkin/GUISkin.guiskin";
		}
		public override void OnInspectorGUI(){

			serializedObject.Update();

			DrawDefaultInspector();
			//Setup the skin
			_skin = (GUISkin)(AssetDatabase.LoadAssetAtPath(_path, typeof(GUISkin)));
			
			//Show the number of events on this date. 
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("Events On this Date", EditorStyles.boldLabel);
			EditorGUILayout.LabelField(_eventManager.CoreEvents.Count.ToString());
			EditorGUILayout.EndHorizontal();

			//Generate Events Button
			if (GUILayout.Button("Generate Event"))
			{
				_eventManager.GenerateEvents();
				return;
			}

			if (GUILayout.Button("Clear Event Transform")){
				_eventManager.ClearEventTransform();
				return;
			}

			//Shows the current events Button.
			if (GUILayout.Button("Show Events for Selected Date"))
			{
				RefreshCurrentDateEvents();
				return;
			}

			EditorGUILayout.LabelField("Events on this day");

			for(int i = 0; i < _eventManager.CoreEvents.Count; i++)
			{
				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.LabelField(_eventManager.CoreEvents[i].Name, GUILayout.Width(100));
				
				if (GUILayout.Button("Edit", GUI.skin.button, GUILayout.Width(100)))
				{
					//If the editor window for core event has not been open, then open a new one. 
					if (CoreEventEditorWindowOpen == false)
					{
						//TODO: Open up some window for this particular event.
						_coreEventEditorWindow = ScriptableObject.CreateInstance<CoreEventEditorWindow>();
						_coreEventEditorWindow.position = new Rect(Screen.width / 2, Screen.height / 2, 500, 125);
						_coreEventEditorWindow.Setup(this, _eventManager, _eventManager.CoreEvents[i]);
						_coreEventEditorWindow.Show();
						_coreEventEditorWindow.titleContent = new GUIContent("Core Event");
						CoreEventEditorWindowOpen = true;
					} 
					//otherwise, refresh the editor window with the new information.
					else {
						_coreEventEditorWindow.Setup(this, _eventManager, _eventManager.CoreEvents[i]);
					}
					
					return;
				}

				EditorGUILayout.EndHorizontal();
			}

			serializedObject.ApplyModifiedProperties();
		}

		///<summary>Refreshes the current date events</summary>
		public virtual void RefreshCurrentDateEvents()
		{
			var date = _eventManager.selectedDateTime;
			var selectedDate = new Date(date.Year, date.Month, date.Day);
			_eventManager.RefreshCoreEvents(selectedDate);
		}
    }

}
