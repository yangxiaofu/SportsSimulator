using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Donger.BuckeyeEngine{
	[CustomEditor(typeof(EventManager))]
	public class EventManagerEditor : DongerEditor{

		/// <summary>
		/// This function is called when the object becomes enabled and active.
		/// </summary>
		protected override void OnEnable()
		{
			base.OnEnable();
		}

		public override void OnInspectorGUI()
        {
            serializedObject.Update();
            DrawHelpBox();
            DrawDefaultInspector();

			if(GUILayout.Button("Generate Events"))
			{
				(_target as EventManager).GenerateEvents();
			}

            serializedObject.ApplyModifiedProperties();
        }


    }

}
