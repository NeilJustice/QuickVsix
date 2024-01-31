using CSharpUtils;
using NUnit.Framework;
using System;

[TestFixture]
public class QuickVsixProgramTests
{
   QuickVsixProgram _quickVsixProgram;
   QuickVsixArgsParser _acceptanceTestRunnerArgsParserMock;
   ConsoleWriter _consoleWriterMock;
   QuickVsixSubProgramFactory _acceptanceTestRunnerSubProgramFactoryMock;
   OneArgTryCatchCaller _oneArgTryCatchCallerMock;
   PreamblePrinter _preamblePrinterMock;
   ResultAndExitCodePrinter _resultAndElapsedTimeAndExitCodePrinterMock;
   Stopwatcher _stopwatcherMock;

   [SetUp]
   public void SetUp()
   {
      _quickVsixProgram = new QuickVsixProgram();
      _acceptanceTestRunnerArgsParserMock = Mock.Component<QuickVsixArgsParser>(_quickVsixProgram, "_acceptanceTestRunnerArgsParser");
      Assert2.AssertConsoleWriterHasProgramName("QuickVsix", _quickVsixProgram, "_consoleWriter");
      _consoleWriterMock = Mock.Component<ConsoleWriter>(_quickVsixProgram, "_consoleWriter");
      _acceptanceTestRunnerSubProgramFactoryMock = Mock.Component<QuickVsixSubProgramFactory>(_quickVsixProgram, "_acceptanceTestRunnerSubProgramFactory");
      _oneArgTryCatchCallerMock = Mock.Component<OneArgTryCatchCaller>(_quickVsixProgram, "_oneArgTryCatchCaller");
      _preamblePrinterMock = Mock.Component<PreamblePrinter>(_quickVsixProgram, "_preamblePrinter");
      _resultAndElapsedTimeAndExitCodePrinterMock = Mock.Component<ResultAndExitCodePrinter>(_quickVsixProgram, "_resultAndElapsedTimeAndExitCodePrinter");
      _stopwatcherMock = Mock.Component<Stopwatcher>(_quickVsixProgram, "_stopwatcher");
   }

   [Test]
   public void Main_ParsesArgs_TryCatchCallsRunProgram_ReturnsRunProgramReturnValues()
   {
      int tryCatchCallReturnValue = Mock.ReturnRandomInt(() => _oneArgTryCatchCallerMock.TryCatchCall(
         default(Func<string[], int>),
         default(string[]),
         default(Func<Exception, string[], int>)));
      string[] stringArgs = TestRandom.StringArray();
      //
      int exitCode = _quickVsixProgram.Main(stringArgs);
      //
      Called.Once(() => _oneArgTryCatchCallerMock.TryCatchCall(_quickVsixProgram.Run, stringArgs, _quickVsixProgram.ExceptionHandler));
      Assert.AreEqual(tryCatchCallReturnValue, exitCode);
   }

   [Test]
   public void Run_NewsProgramModeRunner_PrintsPreamble_RunsProgram_PrintsResultAndElapsedTimeAndExitCode_ReturnsExitCode()
   {
      Mock.Expect(() => _stopwatcherMock.Start());

      QuickVsixArgs args = QuickVsixTestRandom.Args();
      Mock.Return(() => _acceptanceTestRunnerArgsParserMock.ParseStringArgs(null), args);

      QuickVsixSubProgram subProgramMock = Mock.Strict<QuickVsixSubProgram>();
      int runReturnValue = Mock.ReturnRandomInt(() => subProgramMock.Run(null));

      Mock.Return(() => _acceptanceTestRunnerSubProgramFactoryMock.New(default), subProgramMock);

      Mock.Expect(() => _preamblePrinterMock.PrintPreamble(null));

      string elapsedSecondsAndMilliseconds = Mock.ReturnRandomString(() => _stopwatcherMock.StopAndGetSeconds());

      Mock.Expect(() => _resultAndElapsedTimeAndExitCodePrinterMock.PrintResultAndElapsedTimeAndExitCode(null, 0));

      Mock.Expect(() => _consoleWriterMock.OptionallyWaitForAnyKeyToContinue(false));

      var stringArgs = TestRandom.StringArray();
      //
      int exitCode = _quickVsixProgram.Run(stringArgs);
      //
      Called.Once(() => _stopwatcherMock.Start()).Then(
      Called.Once(() => _acceptanceTestRunnerSubProgramFactoryMock.New(args.programMode))).Then(
      Called.Once(() => _preamblePrinterMock.PrintPreamble(args))).Then(
      Called.Once(() => subProgramMock.Run(args))).Then(
      Called.Once(() => _stopwatcherMock.StopAndGetSeconds())).Then(
      Called.Once(() => _resultAndElapsedTimeAndExitCodePrinterMock.PrintResultAndElapsedTimeAndExitCode(elapsedSecondsAndMilliseconds, exitCode))).Then(
      Called.Once(() => _consoleWriterMock.OptionallyWaitForAnyKeyToContinue(args.waitForAnyKey)));
      Assert.AreEqual(runReturnValue, exitCode);
   }

   [Test]
   public void ExceptionHandler_PrintsExceptionMessage_PrintsTestResultElapsedTimeExitCode_Returns1()
   {
      Mock.Expect(() => _consoleWriterMock.WriteProgramNameTimestampedLine(null));

      string elapsedSecondsAndMilliseconds = Mock.ReturnRandomString(() => _stopwatcherMock.StopAndGetSeconds());

      Mock.Expect(() => _resultAndElapsedTimeAndExitCodePrinterMock.PrintResultAndElapsedTimeAndExitCode(null, 0));

      Mock.Expect(() => _consoleWriterMock.OptionallyWaitForAnyKeyToContinue(false));

      var ex = new Exception(TestRandom.String());
      string[] stringArgs = TestRandom.StringArray();
      //
      int exitCode = _quickVsixProgram.ExceptionHandler(ex, stringArgs);
      //
      string expectedExceptionMessage = $@"Error: Exception thrown:
{ex}";
      Called.Once(() => _consoleWriterMock.WriteProgramNameTimestampedLine(expectedExceptionMessage)).Then(
      Called.Once(() => _stopwatcherMock.StopAndGetSeconds())).Then(
      Called.Once(() => _resultAndElapsedTimeAndExitCodePrinterMock.PrintResultAndElapsedTimeAndExitCode(elapsedSecondsAndMilliseconds, 1))).Then(
      Called.Once(() => _consoleWriterMock.OptionallyWaitForAnyKeyToContinue(true)));
      Assert.AreEqual(1, exitCode);
   }
}
