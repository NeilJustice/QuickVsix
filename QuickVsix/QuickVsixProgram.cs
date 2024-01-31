using CSharpUtils;

public class QuickVsixProgram
{
   public readonly ConsoleWriter _consoleWriter = new ConsoleWriter("QuickVsix");
   private readonly QuickVsixArgsParser _acceptanceTestRunnerArgsParser = new QuickVsixArgsParser();
   private readonly QuickVsixSubProgramFactory _acceptanceTestRunnerSubProgramFactory = new QuickVsixSubProgramFactory();
   private readonly OneArgTryCatchCaller _oneArgTryCatchCaller = new OneArgTryCatchCaller();
   private readonly PreamblePrinter _preamblePrinter = new PreamblePrinter();
   private readonly ResultAndExitCodePrinter _resultAndElapsedTimeAndExitCodePrinter = new ResultAndExitCodePrinter();
   private readonly Stopwatcher _stopwatcher = new Stopwatcher();

   public int Main(string[] stringArgs)
   {
      int exitCode = _oneArgTryCatchCaller.TryCatchCall(Run, stringArgs, ExceptionHandler);
      return exitCode;
   }

   public int Run(string[] stringArgs)
   {
      _stopwatcher.Start();
      QuickVsixArgs args = _acceptanceTestRunnerArgsParser.ParseStringArgs(stringArgs);
      QuickVsixSubProgram subProgram = _acceptanceTestRunnerSubProgramFactory.New(args.programMode);
      _preamblePrinter.PrintPreamble(args);
      int exitCode = subProgram.Run(args);
      string elapsedSecondsAndMilliseconds = _stopwatcher.StopAndGetSeconds();
      _resultAndElapsedTimeAndExitCodePrinter.PrintResultAndElapsedTimeAndExitCode(elapsedSecondsAndMilliseconds, exitCode);
      _consoleWriter.OptionallyWaitForAnyKeyToContinue(args.waitForAnyKey);
      return exitCode;
   }

   public int ExceptionHandler(Exception ex, string[] stringArgs)
   {
      string exceptionMessage = $@"Error: Exception thrown:
{ex}";
      _consoleWriter.WriteProgramNameTimestampedLine(exceptionMessage);
      string elapsedSecondsAndMilliseconds = _stopwatcher.StopAndGetSeconds();
      _resultAndElapsedTimeAndExitCodePrinter.PrintResultAndElapsedTimeAndExitCode(elapsedSecondsAndMilliseconds, 1);
      _consoleWriter.OptionallyWaitForAnyKeyToContinue(true);
      return 1;
   }
}
