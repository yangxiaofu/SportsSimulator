using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Donger.BuckeyeEngine{
	public class CoreEventEditorWindow : EditorWindow {
		EventManager _eventManager;
		EventManagerEditor _eventManagerEditor;
		CoreEvent _coreEvent;
		public void Setup(EventManagerEditor eventManagerEditor, EventManager eventManager, CoreEvent coreEvent)
		{
			_coreEvent = coreEvent;
			_eventManager = eventManager;
			_eventManagerEditor = eventManagerEditor;
		}

		void OnGUI()
		{
			EditorGUILayout.LabelField("Event Name " + _coreEvent.Name);
			EditorGUILayout.LabelField("Date" + _coreEvent.Month + "/" + _coreEvent.Day + "/" + _coreEvent.Year);

			//Display the delete button
			if (GUILayout.Button("Delete Event"))
			{
				_eventManager.RemoveEvent(_coreEvent.ID);
				_eventManagerEditor.RefreshCurrentDateEvents();
				CloseWindow();	
			}
		}
		
		///<summary>Closes the window</summary>
		protected void CloseWindow()
		{
			_eventManagerEditor.CoreEventEditorWindowOpen = false;
			this.Close();
		}

		void OnDisable()
        {
			_eventManagerEditor.CoreEventEditorWindowOpen = false;
        }
    }
}

