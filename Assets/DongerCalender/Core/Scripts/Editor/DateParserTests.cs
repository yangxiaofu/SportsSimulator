using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

namespace DongerCalendar.Core.UnitTests{
	public class DateParserTests {

		[Test]
		[TestCase("8-2-1981", 2)]
		[TestCase("8/2/1981", 2)]
		public void DateParserTests_ReturnsCorrectDay(string dateString, int result) 
		{
			var date = new DateParser(dateString);
			Assert.AreEqual(result, date.Day);
		}

		[Test]
		[TestCase("8-2-1981", 8)]
		[TestCase("8/2/1981", 8)]
		public void DateParserTests_ReturnsCorrectMonth(string dateString, int result) 
		{
			var date = new DateParser(dateString);
			Assert.AreEqual(result, date.Month);
		}

		[Test]
		[TestCase("8-2-1981", 1981)]
		[TestCase("8/2/1981", 1981)]
		public void DateParserTests_ReturnsCorrectYear(string dateString, int result) 
		{
			var date = new DateParser(dateString);
			Assert.AreEqual(result, date.Year);
		}
	}

}
