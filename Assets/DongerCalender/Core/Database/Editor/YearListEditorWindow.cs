using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Donger.BuckeyeEngine{
	public class YearListEditorWindow : ListEditorWindow 
	{
		public int Year;
		void OnGUI()
		{
			EditorGUILayout.LabelField("Enter the year");

			//The input text area for the year.
			Year = Convert.ToInt16(EditorGUILayout.TextField(Year.ToString()));

			//Save the year and update the event inspector.
			if (GUILayout.Button("Close and Save"))
			{
				CloseWindow(Year);
			}
		}

		protected override void OnDisable()
		{
			if (_database == null) return;
			_database.YearWindowOpen = false;
		}

        protected override void CloseWindow(int index)
        {
            _database.YearWindowOpen = false;
			_database.Year = index;
			this.Close();
        }
    }
}

