using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
namespace Donger.BuckeyeEngine
{
	[CustomEditor(typeof(EventsDatabase))]
	public class EventsDatabaseEditor : Editor 
    {
		private EventsDatabase _database;

		// void OnEnable()
		// {
		// 	_database = (EventsDatabase)target;
		// }

		// public override void OnInspectorGUI()
        // {
        //     EditorGUILayout.HelpBox(_database.HelpBox(), MessageType.Info);

        //     serializedObject.Update();


        //     //Display the header
        //     EditorGUILayout.BeginHorizontal(GUILayout.Width(100));
        //     GUILayout.Label(new GUIContent("List of events"), EditorStyles.boldLabel);
        //     EditorGUILayout.EndHorizontal();

        //     //Begin Main Buttons for the database
        //     EditorGUILayout.BeginHorizontal();
        //     if (GUILayout.Button("Sort By Date"))
        //     {
        //         Debug.Log("Sorts the database"); //TODO: Create a database sorter.
        //     }

        //     if (GUILayout.Button("Clear Database"))
        //     {
        //         _database.Clear();
        //     }
        //     EditorGUILayout.EndHorizontal();

        //     DisplayListOfCoreEvents();

        //     serializedObject.ApplyModifiedProperties();
        // }

        // ///<summary>Display the list of events based on the current date in the inspector.</summary>
        // private void DisplayListOfCoreEvents()
        // {
        //     float _nameColumnWidth = 100f;
        //     float _dateColumnWidth = 80f;
        //     float _actionColumnWidth = 100f;

        //     EditorGUILayout.BeginHorizontal();
        //     GUILayout.Label("Event Name", EditorStyles.boldLabel, GUILayout.Width(_nameColumnWidth));
        //     GUILayout.Label("Date", EditorStyles.boldLabel, GUILayout.Width(_dateColumnWidth));
        //     GUILayout.Label("Action", EditorStyles.boldLabel, GUILayout.Width(_actionColumnWidth));
        //     EditorGUILayout.EndHorizontal();

        //     var events = _database.Events;
        //     //Shows each event.
        //     for (int i = 0; i < events.Count; i++)
        //     {
        //         EditorGUILayout.BeginHorizontal();
        //         var thisDate = events[i].Month + "/" + events[i].Day + "/" + events[i].Year;

        //         GUILayout.Label(new GUIContent(events[i].Name), GUILayout.Width(_nameColumnWidth));
        //         GUILayout.Label(new GUIContent(thisDate), GUILayout.Width(_dateColumnWidth));

        //         if (GUILayout.Button("Edit", GUILayout.Width(_actionColumnWidth)))
        //         {
        //             _database.Remove(events[i].ID);
        //         }

        //         EditorGUILayout.EndHorizontal();
        //     }
        // }        
	}

}
