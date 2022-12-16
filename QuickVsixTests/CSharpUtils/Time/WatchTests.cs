using CSharpUtils;
using NUnit.Framework;
using System;
using System.Text.RegularExpressions;

[TestFixture]
public class WatchTests
{
   Watch _watch;

   [SetUp]
   public void SetUp()
   {
      _watch = new Watch();
   }

   [Test]
   public void Now_ReturnsNowDateTime()
   {
      DateTime dateTimeNow = _watch.DateTimeNow();
      Assert.AreNotEqual(DateTime.MinValue, dateTimeNow);
   }

   [Test]
   public void DateTime_ReturnsTodayDateTime()
   {
      DateTime dateToday = _watch.DateToday();
      Assert.AreEqual(DateTime.Today, dateToday);
   }

   [Test]
   public void DateTodayString_ReturnsDateTodayStringInISO8601Format()
   {
      string dateToday = _watch.DateTodayString();
      Assert.IsTrue(Regex.IsMatch(dateToday, @"^\d\d\d\d-\d\d-\d\d$"));
   }

   [Test]
   public void DateTimeNowString_ReturnsDateTimeDotNowInExpectedFormat()
   {
      string dateTimeNow = _watch.DateTimeNowString();
      Assert.IsTrue(Regex.IsMatch(dateTimeNow, @"^\d\d\d\d-\d\d-\d\d \d\d:\d\d:\d\d (AM|PM)$"));
   }
}
