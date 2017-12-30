using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Donger.BuckeyeEngine{
	public abstract class ListEditorWindow : EditorWindow{

		protected EventsDatabaseEditor _database;
		protected int _numberOfColumns = 1;
		protected float _buttonWidth = 50f;

		///<summary>Sets the the window parameters</summary>
		public void Setup(EventsDatabaseEditor eventsDatabase, int numberOfColumns, float buttonWidth)
		{
			_database = eventsDatabase;
			_numberOfColumns = numberOfColumns;
			_buttonWidth = buttonWidth;
		}

		protected virtual void OnDisable(){
			return;
		}

		protected abstract void CloseWindow(int index);
	}
}

