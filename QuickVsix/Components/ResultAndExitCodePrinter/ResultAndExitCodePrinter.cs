using CSharpUtils;
using System;

public class ResultAndExitCodePrinter
{
   private readonly ConsoleWriter _consoleWriter = new ConsoleWriter("QuickVsix");

   public virtual void PrintResultAndElapsedTimeAndExitCode(string elapsedSecondsAndMilliseconds, int exitCode)
   {
      if (exitCode == 0)
      {
         _consoleWriter.WriteProgramNameTimestampedLineWithColor("     Result: Success", ConsoleColor.Green);
         _consoleWriter.WriteProgramNameTimestampedLine($"ElapsedTime: {elapsedSecondsAndMilliseconds} seconds");
         _consoleWriter.WriteProgramNameTimestampedLine("   ExitCode: 0");
      }
      else
      {
         _consoleWriter.WriteProgramNameTimestampedLineWithColor("     Result: Failure", ConsoleColor.Red);
         _consoleWriter.WriteProgramNameTimestampedLine($"ElapsedTime: {elapsedSecondsAndMilliseconds} seconds");
         _consoleWriter.WriteProgramNameTimestampedLine($"   ExitCode: {exitCode}");
      }
   }
}
