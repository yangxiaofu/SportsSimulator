using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Donger.UI{
	public class StarterUI : MonoBehaviour {
		[SerializeField] GUISkin _skin;
		void OnGUI()
		{
			if (GUI.Button(new Rect(Screen.width/2, Screen.height/2, 200, 50), new GUIContent("Simulate"), _skin.button)){
				print("Button Pressed");
			}
		}
	}

}
