using System;
using System.Diagnostics.CodeAnalysis;

namespace CSharpUtils
{
   public class ConsoleWriter
   {
      private string _programName;
      private readonly Asserter _asserter = new Asserter();
      private readonly MethodCaller _methodCaller = new MethodCaller();
      private readonly Watch _watch = new Watch();

      public ConsoleWriter()
      {
      }

      public ConsoleWriter(string programName)
      {
         _programName = programName;
      }

      public virtual void OptionallyPressAnyKeyToContinue(bool waitForAnyKey)
      {
         if (waitForAnyKey)
         {
            _methodCaller.CallAction(WriteProgramNameTimestampedLine, "Press any key to continue . . .");
            _methodCaller.CallFunction(Console.ReadLine);
         }
      }

      [ExcludeFromCodeCoverage]
      public virtual ConsoleColor SetForegroundColor(ConsoleColor textColor)
      {
         ConsoleColor initialForegroundColor = Console.ForegroundColor;
         Console.ForegroundColor = textColor;
         return initialForegroundColor;
      }

      public virtual void SetProgramName(string programName)
      {
         _programName = programName;
      }

      public virtual void Write(string message)
      {
         _methodCaller.CallAction(Console.Write, message);
      }

      public virtual void WriteLine(string message)
      {
         _methodCaller.CallAction(Console.WriteLine, message);
      }

      public virtual void WriteLineWithColor(string message, ConsoleColor textColor)
      {
         ConsoleColor initialForegroundColor = _methodCaller.CallFunction(SetForegroundColor, textColor);
         _methodCaller.CallAction(Console.WriteLine, message);
         _methodCaller.CallFunction(SetForegroundColor, initialForegroundColor);
      }

      public virtual void WriteNewLine()
      {
         _methodCaller.CallAction(Console.WriteLine);
      }

      public virtual void WriteProgramNameTimestampedLine(string message)
      {
         string dateTimeNow = _watch.DateTimeNowString();
         _asserter.ThrowIfNull(_programName, nameof(_programName));
         string programNameTimestampedMessage = $"[{_programName} {dateTimeNow}] {message}";
         _methodCaller.CallAction(Console.WriteLine, programNameTimestampedMessage);
      }

      public virtual void WriteProgramNameTimestampedLineWithColor(string message, ConsoleColor textColor)
      {
         ConsoleColor initialForegroundColor = _methodCaller.CallFunction(SetForegroundColor, textColor);
         _methodCaller.CallAction(WriteProgramNameTimestampedLine, message);
         _methodCaller.CallFunction(SetForegroundColor, initialForegroundColor);
      }
   }
}
