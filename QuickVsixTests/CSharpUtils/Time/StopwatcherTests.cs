using CSharpUtils;
using NUnit.Framework;
using System;
using System.Text.RegularExpressions;

[TestFixture]
public class StopwatcherTests
{
   Stopwatcher _stopwatcher;

   [SetUp]
   public void SetUp()
   {
      _stopwatcher = new Stopwatcher();
   }

   [Test]
   public void StartThenStart_ThrowsInvalidOperationException()
   {
      _stopwatcher.Start();
      Assert2.Throws<InvalidOperationException>(() => _stopwatcher.Start(),
         "Stopwatcher.Start() called on a running Stopwatcher.");
   }

   [Test]
   public void StopAndGetSecondsBeforeStart_ThrowsInvalidOperationException()
   {
      Assert2.Throws<InvalidOperationException>(() => _stopwatcher.StopAndGetSeconds(),
         "Stopwatcher.StopAndGetSeconds() called on a stopped Stopwatcher.");
   }

   [Test]
   public void StartThenStopThenStartThenStopAndGetSeconds_ReturnsElapsedSecondsWithThreeDecimalPlaces()
   {
      _stopwatcher.Start();
      string elapsedSecondsA = _stopwatcher.StopAndGetSeconds();
      Assert.IsTrue(Regex.IsMatch(elapsedSecondsA, @"^\d+\.\d\d\d$"));

      _stopwatcher.Start();
      string elapsedSecondsB = _stopwatcher.StopAndGetSeconds();
      Assert.IsTrue(Regex.IsMatch(elapsedSecondsB, @"^\d+\.\d\d\d$"));
   }

   [Test]
   public void StopAndGetMillisecondsBeforeStart_ThrowsInvalidOperationException()
   {
      Assert2.Throws<InvalidOperationException>(() => _stopwatcher.StopAndGetMilliseconds(),
         "Stopwatcher.StopAndGetMilliseconds() called on a stopped Stopwatcher.");
   }

   [Test]
   public void StopAndGetMillisecondsBeforeStart_()
   {
      _stopwatcher.Start();
      string elapsedMillisecondsA = _stopwatcher.StopAndGetMilliseconds();
      Assert.IsTrue(Regex.IsMatch(elapsedMillisecondsA, @"^\d+$"));

      _stopwatcher.Start();
      string elapsedMillisecondsB = _stopwatcher.StopAndGetMilliseconds();
      Assert.IsTrue(Regex.IsMatch(elapsedMillisecondsB, @"^\d+$"));
   }

   [Test]
   public void StopAndGetMinutesAndSecondsBeforeStart_ThrowsInvalidOperationException()
   {
      Assert2.Throws<InvalidOperationException>(() => _stopwatcher.StopAndGetMinutesAndSeconds(),
         "Stopwatcher.StopAndGetMinutesAndSeconds() called on a stopped Stopwatcher.");
   }

   [Test]
   public void StartThenStopThenStartThenStopAndGetMinutes_ReturnsElapsedMinutesWithTwoDecimalPlaces()
   {
      _stopwatcher.Start();
      string elapsedMinutesAndSecondsA = _stopwatcher.StopAndGetMinutesAndSeconds();
      Assert.IsTrue(Regex.IsMatch(elapsedMinutesAndSecondsA, @"^\d+\.\d\d minutes \(\d+.\d seconds\)$"));

      _stopwatcher.Start();
      string elapsedMinutesAndSecondsB = _stopwatcher.StopAndGetMinutesAndSeconds();
      Assert.IsTrue(Regex.IsMatch(elapsedMinutesAndSecondsB, @"^\d+\.\d\d minutes \(\d+\.\d seconds\)$"));
   }
}
