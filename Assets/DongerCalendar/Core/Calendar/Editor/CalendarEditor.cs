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
		protected string[] _months = {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sept", "Oct", "Nov", "Dec"};
        protected string[] _daysInWeek = {"Sun", "Mon", "Tues", "Wed", "Thurs", "Fri", "Sat"};
        GUISkin _skin;
        string _path = "Assets/DongerCalendar/Core/GUISkin/GUISkin.guiskin";
        

		protected virtual void DrawHelpBox()
        {
            EditorGUILayout.HelpBox(_calendar.HelpBox(), MessageType.Info);
        }

		protected virtual void OnEnable()
		{
			_calendar = (Calendar)target;
			Year = serializedObject.FindProperty("StartingYear").intValue;
			Month = serializedObject.FindProperty("StartingMonth").intValue;
			_day = serializedObject.FindProperty("StartingDay").intValue;
            _skin = (GUISkin)(AssetDatabase.LoadAssetAtPath(_path, typeof(GUISkin)));
		}

		public override void OnInspectorGUI()
        {
            serializedObject.Update();

            //Draw the help box
            DrawHelpBox();

            //Display the Calendar header
            GUILayout.Label("Calendar", EditorStyles.boldLabel);

            if (GUILayout.Button("Reset To Default"))
            {
                Calendar calender = (Calendar)_calendar;
                Year = calender.StartingYear;
                Month = calender.StartingMonth;
                _day = calender.StartingDay;
                _calendar.ResetToDefault();
                return;
            }

            //Draw the selectors
            DrawYearSelector();
            DrawMonthSelector();

            Seperator();

            //Draw the selected date
            DrawSelectedDate();

            //Draw the calendar
            float buttonWidth = 50f;

            DrawCalendar(Year, Month, _day, buttonWidth);

            EditorGUILayout.Space();

            serializedObject.ApplyModifiedProperties();
        }

        ///<summary>Draws the selected date to notify the user.!--</summary>
        protected virtual void DrawSelectedDate()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Selected Date", EditorStyles.boldLabel, GUILayout.Width(100));
            var selectedDate = _calendar.SelectedMonth + "/" + _calendar.SelectedDay + "/" + _calendar.SelectedYear;
            EditorGUILayout.LabelField(selectedDate);
            EditorGUILayout.EndHorizontal();
        }

        protected virtual void DrawYearSelector()
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

		protected virtual void DrawMonthSelector()
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
        protected virtual void DrawCalendar(int year, int month, int day, float buttonWidth)
        {
            float columnWidth = buttonWidth * 1.05f;
            //Get days in the month
            var daysInMonth = DateTime.DaysInMonth(year, month);

            //List out the days of the week.
            EditorGUILayout.BeginHorizontal();
            for (int i = 0; i < _daysInWeek.Length; i++)
            {
                EditorGUILayout.LabelField(_daysInWeek[i], GUILayout.Width(buttonWidth));   
            }
            EditorGUILayout.EndHorizontal();

            //While there are still days left in the month.
            while (day <= daysInMonth)
            {
                //Draw the empty slots where days to not exist.
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
                    EditorGUILayout.BeginVertical(GUILayout.Width(columnWidth));

                    //If the calendar contains an EventManager as a Component.
                    if (_calendar.EventManager)
                    {
                        //Refresh the search date.
                        var searchDate = new DateTime(year, month, day);
                        var coreEvents = _calendar.EventManager.GetEvents(searchDate);
                        //Help the user visually determine if there are events on a particular day.
                        //If there are events, have a custom look to it. 
                        if (coreEvents.Count > 0){
                            GUILayout.Box(coreEvents.Count.ToString(), _skin.box, GUILayout.Width(buttonWidth));
                        } 
                        //otherwise, just go with the default.  
                        else {
                            GUILayout.Box(coreEvents.Count.ToString(), GUILayout.Width(buttonWidth));    
                        }
                        
                    }
                    
					//If a day is selected, then update the calendar with the date. 
                    if(GUILayout.Button(day.ToString(), GUILayout.Width(buttonWidth)))
					{
                        //Update the calender monobehaviour
                        _calendar.UpdateCalendar(year, month, day);
					}

                    EditorGUILayout.EndVertical();

                    day++;

                    //If not more days exist in the month, then do not continue to add days.
                    if (day > daysInMonth) break;
                }

                EditorGUILayout.EndHorizontal();
            }
        }

		///<summary> Separator</summary>
		protected virtual void Seperator()
        {
            EditorGUILayout.Space();
            EditorGUILayout.Separator();
        }

		///<summary>Returns the integer representation for the day of the week.  Returns the index related to the day.</summary>
        protected virtual int GetDayOfWeekIndex(DayOfWeek dayOfWeek)
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

        protected virtual void DecreaseMonth()
        {
            Month -= 1;
            if (Month == 0){
                Month = 12;
            }
        }

        protected virtual void IncreaseMonth()
        {
            Month += 1;
            if (Month == 13){
                Month = 1;
            }
        }

        protected virtual void IncreaseYear()
        {
            Year += 1;
        }

        protected virtual void DecreaseYear()
        {
            Year -= 1;
        }
	}

}
