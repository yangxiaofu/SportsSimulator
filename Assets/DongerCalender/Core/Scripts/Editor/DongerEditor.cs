using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace DongerCalendar.Core{
	public class DongerEditor : Editor{
		protected DongerCalendarCoreBehaviour _target;

		/// <summary>
		/// This function is called when the object becomes enabled and active.
		/// </summary>
		protected virtual void OnEnable()
		{
			_target = (DongerCalendarCoreBehaviour)target;
		}

		protected void DrawHelpBox()
        {
            EditorGUILayout.HelpBox(_target.HelpBox(), MessageType.Info);
        }
	}
}

