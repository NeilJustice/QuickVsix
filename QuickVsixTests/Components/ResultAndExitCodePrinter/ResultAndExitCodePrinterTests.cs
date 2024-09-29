using CSharpUtils;
using NUnit.Framework;

[TestFixture]
public class ResultAndExitCodePrinterTests
{
   ResultAndExitCodePrinter _resultAndElapsedTimeAndExitCodePrinter;
   ConsoleWriter _consoleWriterMock;

   [SetUp]
   public void SetUp()
   {
      _resultAndElapsedTimeAndExitCodePrinter = new ResultAndExitCodePrinter();
      Assert2.AssertConsoleWriterHasProgramName("QuickVsix", _resultAndElapsedTimeAndExitCodePrinter, "_consoleWriter");
      _consoleWriterMock = Mock.Component<ConsoleWriter>(_resultAndElapsedTimeAndExitCodePrinter, "_consoleWriter");
   }

   [Test]
   public void PrintResultAndElapsedTimeAndExitCode_ExitCodeIs0_PrintsSuccessAndExitCode0()
   {
      Mock.Expect(() => _consoleWriterMock.WriteProgramNameTimestampedLineWithColor(null, default));
      Mock.Expect(() => _consoleWriterMock.WriteProgramNameTimestampedLine(null));
      string elapsedSecondsAndMilliseconds = TestRandom.String();
      //
      _resultAndElapsedTimeAndExitCodePrinter.PrintResultAndElapsedTimeAndExitCode(elapsedSecondsAndMilliseconds, 0);
      //
      Called.NumberOfTimes(2, () => _consoleWriterMock.WriteProgramNameTimestampedLine(null));
      Called.Once(() => _consoleWriterMock.WriteProgramNameTimestampedLineWithColor("     Result: Success", ConsoleColor.Green)).Then(
      Called.WasCalled(() => _consoleWriterMock.WriteProgramNameTimestampedLine($"ElapsedTime: {elapsedSecondsAndMilliseconds} seconds"))).Then(
      Called.WasCalled(() => _consoleWriterMock.WriteProgramNameTimestampedLine("   ExitCode: 0")));
   }

   [TestCase(-1)]
   [TestCase(1)]
   [TestCase(123)]
   public void PrintResultAndElapsedTimeAndExitCode_ExitCodeIsNot00_PrintsFailureInRedAndExitCode(int exitCode)
   {
      Mock.Expect(() => _consoleWriterMock.WriteProgramNameTimestampedLineWithColor(null, default));
      Mock.Expect(() => _consoleWriterMock.WriteProgramNameTimestampedLine(null));
      string elapsedSecondsAndMilliseconds = TestRandom.String();
      //
      _resultAndElapsedTimeAndExitCodePrinter.PrintResultAndElapsedTimeAndExitCode(elapsedSecondsAndMilliseconds, exitCode);
      //
      Called.NumberOfTimes(2, () => _consoleWriterMock.WriteProgramNameTimestampedLine(null));
      Called.Once(() => _consoleWriterMock.WriteProgramNameTimestampedLineWithColor("     Result: Failure", ConsoleColor.Red)).Then(
      Called.WasCalled(() => _consoleWriterMock.WriteProgramNameTimestampedLine($"ElapsedTime: {elapsedSecondsAndMilliseconds} seconds"))).Then(
      Called.WasCalled(() => _consoleWriterMock.WriteProgramNameTimestampedLine($"   ExitCode: {exitCode}")));
   }
}
