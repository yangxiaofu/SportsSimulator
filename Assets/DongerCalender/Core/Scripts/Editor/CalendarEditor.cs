using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Donger.BuckeyeEngine{
	[CustomEditor(typeof(Calendar))]
	public class CalendarEditor : Editor{
		Calendar _calendar;
		public int Year;
		public int Month;
		private int _day;
		 ///<summary>Ensures that the window is only opened once.</summary>
        public bool MonthWindowOpen = false;
        ///<summary>Ensures that the Year Window is only opened once</summary>
        public bool YearWindowOpen = false;
		private string[] _months = {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sept", "Oct", "Nov", "Dec"};
        private string[] _daysInWeek = {"Sun", "Mon", "Tues", "Wed", "Thurs", "Fri", "Sat"};

		void DrawHelpBox()
        {
            EditorGUILayout.HelpBox(_calendar.HelpBox(), MessageType.Info);
        }

		void OnEnable()
		{
			_calendar = (Calendar)target;
			Year = serializedObject.FindProperty("StartingYear").intValue;
			Month = serializedObject.FindProperty("StartingMonth").intValue;
			_day = serializedObject.FindProperty("StartingDay").intValue;
		}

		public override void OnInspectorGUI()
		{
			serializedObject.Update();

			DrawHelpBox();

			//Display the Calendar header
            GUILayout.Label("Calendar", EditorStyles.boldLabel);
		
			if (GUILayout.Button("Reset To Default")){
				Calendar calender = (Calendar)_calendar;
				Year = calender.StartingYear;
				Month = calender.StartingMonth;				
				_day = calender.StartingDay;
				return;
			}

			 //Display selector for the months.
            DrawYearSelector();
            DrawMonthSelector();
            Seperator();

            float buttonWidth = 50f;
            DrawCalendar(Year, Month, _day, buttonWidth);

			EditorGUILayout.Space();

			serializedObject.ApplyModifiedProperties();
		}

		private void DrawYearSelector()
        {
            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("<<"))
            {
                DecreaseYear();
            }

            if (GUILayout.Button(Year.ToString(), GUILayout.Width(300)))
            {
                if (!YearWindowOpen)
                {
                    YearWindowOpen = true;
                    YearListEditorWindow window = ScriptableObject.CreateInstance<YearListEditorWindow>();
                    window.position = new Rect(Screen.width / 2, Screen.height / 2, 250, 500);
                    window.Show();
                    var columns = 5;
                    var buttonWidth = 50;
                    window.Setup(this, columns, buttonWidth);
                    window.Year = this.Year;
                }
            }

            if (GUILayout.Button(">>"))
            {
                IncreaseYear();
            }
            EditorGUILayout.EndHorizontal();
        }

		private void DrawMonthSelector()
        {
            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("<<"))
            {
                DecreaseMonth();
            }

            if (GUILayout.Button(_months[Month - 1].ToString(), GUILayout.Width(300)))
            {
                if (!MonthWindowOpen)
                {
                    MonthWindowOpen = true;

                    //Open Window
                    MonthListEditorWindow window = ScriptableObject.CreateInstance<MonthListEditorWindow>();
                    window.position = new Rect(Screen.width / 2, Screen.height / 2, 500, 125);
                    window.Show();
                    var columns = 5;
                    var buttonWidth = 50;
                    window.Setup(this, columns, buttonWidth);
                }
            }

            if (GUILayout.Button(">>"))
            {
                IncreaseMonth();
            }
            EditorGUILayout.EndHorizontal();
        }

		///<summary>Draw the calendar GUI</summary>
        private void DrawCalendar(int year, int month, int day, float buttonWidth)
        {
            //Get days in the month
            var daysInMonth = DateTime.DaysInMonth(year, month);

            EditorGUILayout.BeginHorizontal();
            for (int i = 0; i < _daysInWeek.Length; i++)
            {
                EditorGUILayout.LabelField(_daysInWeek[i], GUILayout.Width(buttonWidth));   
            }
            EditorGUILayout.EndHorizontal();

            //While there are still days left in the month.
            while (day <= daysInMonth)
            {
                //Draw Empty Slots
                var date = new DateTime(year, month, day);
                var dayOfWeek = date.DayOfWeek;
                var dayOfWeekIndex = GetDayOfWeekIndex(dayOfWeek);

				//Begin a new row.
                EditorGUILayout.BeginHorizontal();

                for (int j = 0; j < dayOfWeekIndex; j++)
                {
                    GUILayout.Box("", GUILayout.Width(buttonWidth));
                }

                //Draw actual days
                for (int j = dayOfWeekIndex; j < 7; j++)
                {
					//If a day is selected, then update the calendar with the date. 
                    if(GUILayout.Button(day.ToString(), GUILayout.Width(buttonWidth)))
					{
                        //Update the calender monobehaviour
						_calendar.SelectedDay = day;
						_calendar.SelectedMonth = month;
						_calendar.SelectedYear = year;
                        Debug.Log(month + "/" + day + "/" + year);
					}

                    day++;

                    if (day > daysInMonth) break;
                }

                EditorGUILayout.EndHorizontal();
            }
        }

		///<summary> Separator</summary>
		private void Seperator()
        {
            
            EditorGUILayout.Space();
            EditorGUILayout.Separator();
        }

		///<summary>Returns the integer representation for the day of the week.<para>Returns something</para></summary>

        private int GetDayOfWeekIndex(DayOfWeek dayOfWeek)
        {
			if (dayOfWeek == DayOfWeek.Sunday){
				return 0;
			} else if (dayOfWeek == DayOfWeek.Monday){
				return 1; 
			} else if (dayOfWeek == DayOfWeek.Tuesday){
				return 2;
			} else if (dayOfWeek == DayOfWeek.Wednesday){
				return 3;	
			} else if (dayOfWeek == DayOfWeek.Thursday){
				return 4;
			} else if (dayOfWeek == DayOfWeek.Friday){
				return 5;
			} else {
				return 6;
			}
		}

        private void DecreaseMonth()
        {
            Month -= 1;
            if (Month == 0){
                Month = 12;
            }
        }

        private void IncreaseMonth()
        {
            Month += 1;
            if (Month == 13){
                Month = 1;
            }
        }

        private void IncreaseYear()
        {
            Year += 1;
        }

        private void DecreaseYear()
        {
            Year -= 1;
        }
	}

}
