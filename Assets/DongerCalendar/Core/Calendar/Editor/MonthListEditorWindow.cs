using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace Donger.BuckeyeEngine{

	public class MonthListEditorWindow : ListEditorWindow
	{
		public string[] Months = {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sept", "Oct", "Nov", "Dec"};

		void OnGUI()
        {
            DrawMonthButtons(_numberOfColumns, _buttonWidth);
        }

		///<summary>Draw out the month buttons</summary>
        private void DrawMonthButtons(int columns, float buttonWidth)
        {
            EditorGUILayout.LabelField("Select the month", EditorStyles.boldLabel);
            int i = 0;
            while (i < Months.Length)
            {
                EditorGUILayout.BeginHorizontal();

                for (int j = 0; j < columns; j++)
                {
                    if (GUILayout.Button(Months[i], GUILayout.Width(buttonWidth)))
                    {
                        CloseWindow(i + 1);
                    }
                    i++;

                    //If no more months exists, then break. 
                    if (i >= Months.Length) 
                    {
                        break;
                    }
                }
                
                EditorGUILayout.EndHorizontal();
            }
        }

        protected override void OnDisable()
		{
            if (_cal == null) return;
			_cal.MonthWindowOpen = false;
		}

        protected override void CloseWindow(int index)
        {
            _cal.MonthWindowOpen = false;
            _cal.Month = index;
            this.Close();
            
        }
    }

}