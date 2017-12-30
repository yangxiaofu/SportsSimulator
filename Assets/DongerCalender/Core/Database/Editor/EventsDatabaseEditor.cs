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
		public int Year = 2018;
		public int Month = 2;
		private int day = 1;
        ///<summary>Ensures that the window is only opened once.</summary>
        public bool MonthWindowOpen = false;
        ///<summary>Ensures that the Year Window is only opened once</summary>
        public bool YearWindowOpen = false;
        private string[] _months = {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sept", "Oct", "Nov", "Dec"};
        private string[] _daysInWeek = {"Sun", "Mon", "Tues", "Wed", "Thurs", "Fri", "Sat"};

		List<string> foldoutIds = new List<string>();

		void OnEnable()
		{
			_database = (EventsDatabase)target;
		}

		public override void OnInspectorGUI()
        {
            EditorGUILayout.HelpBox("This is meant to display the events on the selected day.  You can find the events for the day after selecting a day.", MessageType.Info);

            float _nameColumnWidth = 100f;
            float _dateColumnWidth = 80f;
            float _actionColumnWidth = 100f;

            serializedObject.Update();

            //Display the Calendar header
            GUILayout.Label("Calendar", EditorStyles.boldLabel);

            //Display selector for the months.
            DrawYearSelector();
            DrawMonthSelector();
            Seperator();
            DrawCalendar(Year, Month, day, 50f);

            EditorGUILayout.Space();

            //Display the list of events.
            //Display the head.
            EditorGUILayout.BeginHorizontal(GUILayout.Width(100));
            GUILayout.Label(new GUIContent("List of events"), EditorStyles.boldLabel);
            EditorGUILayout.EndHorizontal();

            //Begin Main Buttons for the database
            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Sort By Date"))
            {
                Debug.Log("Sorts the database"); //TODO: Create a database sorter.
            }

            if (GUILayout.Button("Clear Database"))
            {
                _database.Clear();
            }

            EditorGUILayout.EndHorizontal();

            //Display the table header.
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Event Name", EditorStyles.boldLabel, GUILayout.Width(_nameColumnWidth));
            GUILayout.Label("Date", EditorStyles.boldLabel, GUILayout.Width(_dateColumnWidth));
            GUILayout.Label("Action", EditorStyles.boldLabel, GUILayout.Width(_actionColumnWidth));
            EditorGUILayout.EndHorizontal();

            var events = _database.Events;
            //Shows each event.
            for (int i = 0; i < events.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                var thisDate = events[i].Month + "/" + events[i].Day + "/" + events[i].Year;

                GUILayout.Label(new GUIContent(events[i].Name), GUILayout.Width(_nameColumnWidth));
                GUILayout.Label(new GUIContent(thisDate), GUILayout.Width(_dateColumnWidth));

                if (GUILayout.Button("Edit", GUILayout.Width(_actionColumnWidth)))
                {
                    _database.Remove(events[i].ID);
                }

                EditorGUILayout.EndHorizontal();
            }

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


        private void Seperator()
        {
            //Seperator
            EditorGUILayout.Space();
            EditorGUILayout.Separator();
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
                    GUILayout.Button(day.ToString(), GUILayout.Width(buttonWidth));
                    day++;

                    if (day > daysInMonth) break;
                }

                EditorGUILayout.EndHorizontal();
            }
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
	}

}
