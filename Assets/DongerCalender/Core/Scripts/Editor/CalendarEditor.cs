using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace DongerCalendar.Core{
	[CustomEditor(typeof(Calendar))]
	public class CalendarEditor : DongerEditor{
		
		protected override void OnEnable()
		{
			_target = (Calendar)target;
		}

		public override void OnInspectorGUI()
		{
			serializedObject.Update();
			DrawHelpBox();
			DrawDefaultInspector();
			serializedObject.ApplyModifiedProperties();
		}
	}

}
