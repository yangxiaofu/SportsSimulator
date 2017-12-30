using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace Donger.BuckeyeEngine{
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
