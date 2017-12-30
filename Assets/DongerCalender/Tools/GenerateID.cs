using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Donger.Tools{
	public class UniqueID{

		///<summary>Generates a unique ID</summary>
		public static string Generate()
		{
			return Guid.NewGuid().ToString();
		}
	}

}
