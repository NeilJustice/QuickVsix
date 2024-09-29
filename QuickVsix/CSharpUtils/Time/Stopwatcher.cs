using System;
using System.Diagnostics;

namespace CSharpUtils
{
   public class Stopwatcher
   {
      private readonly Stopwatch _stopwatch = new Stopwatch();

      public virtual void Start()
      {
         if (_stopwatch.IsRunning)
         {
            throw new InvalidOperationException("Stopwatcher.Start() called on a running Stopwatcher.");
         }
         _stopwatch.Start();
      }

      private void Stop(string methodName)
      {
         if (!_stopwatch.IsRunning)
         {
            throw new InvalidOperationException($"Stopwatcher.{methodName}() called on a stopped Stopwatcher.");
         }
         _stopwatch.Stop();
      }

      public virtual string StopAndGetSeconds()
      {
         Stop("StopAndGetSeconds");
         double elapsedSeconds = _stopwatch.Elapsed.TotalSeconds;
         _stopwatch.Reset();
         double elapsedSecondsRoundedToThreeDecimalPlaces = Math.Round(elapsedSeconds, 3);
         string elapsedSecondsString = elapsedSecondsRoundedToThreeDecimalPlaces.ToString("0.000");
         return elapsedSecondsString;
      }

      public virtual string StopAndGetMilliseconds()
      {
         Stop("StopAndGetMilliseconds");
         double elapsedMilliseconds = Math.Round(_stopwatch.Elapsed.TotalMilliseconds, 0);
         _stopwatch.Reset();
         string elapsedMillisecondsString = elapsedMilliseconds.ToString();
         return elapsedMillisecondsString;
      }

      public virtual string StopAndGetMinutesAndSeconds()
      {
         Stop("StopAndGetMinutesAndSeconds");
         double elapsedMinutes = _stopwatch.Elapsed.TotalMinutes;
         double elapsedSeconds = _stopwatch.Elapsed.TotalSeconds;
         _stopwatch.Reset();
         double elapsedMinutesRoundedToTwoDecimalPlaces = Math.Round(elapsedMinutes, 2);
         double elapsedSecondsRoundedToOneDecimalPlace = Math.Round(elapsedSeconds, 0);
         string elapsedMinutesString = $"{elapsedMinutesRoundedToTwoDecimalPlaces:0.00} minutes ({elapsedSecondsRoundedToOneDecimalPlace:0.0} seconds)";
         return elapsedMinutesString;
      }
   }
}
