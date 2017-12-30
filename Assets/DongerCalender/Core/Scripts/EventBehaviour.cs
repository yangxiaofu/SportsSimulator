using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Donger.BuckeyeEngine{
	public abstract class EventBehaviour : MonoBehaviour {

		protected Date _date;

		///<summary>
		///Initializes the event behaviour
		///</summary>
		public virtual void Initialize(Date date)
		{
			_date = date;
		}

		public abstract void Simulate();
	}

}
