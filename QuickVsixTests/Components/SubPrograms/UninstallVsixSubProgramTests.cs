using CSharpUtils;
using NUnit.Framework;

[TestFixture]
public class UninstallVsixSubProgramTests
{
   UninstallVsixSubProgram _uninstallVsixSubProgram;
   ConsoleWriter p_consoleWriterMock;
   ProcessKiller p_processKillerMock;
   ProcessRunner p_processRunnerMock;
   QuickVsixLogFilePathPrinter p_quickVsixLogFilePathPrinterMock;
   VsixZipFileReader p_vsixZipFileReaderMock;

   [SetUp]
   public void SetUp()
   {
      _uninstallVsixSubProgram = new UninstallVsixSubProgram();
      p_consoleWriterMock = Mock.Component<ConsoleWriter>(_uninstallVsixSubProgram, "p_consoleWriter");
      p_processKillerMock = Mock.Component<ProcessKiller>(_uninstallVsixSubProgram, "p_processKiller");
      p_processRunnerMock = Mock.Component<ProcessRunner>(_uninstallVsixSubProgram, "p_processRunner");
      p_quickVsixLogFilePathPrinterMock = Mock.Component<QuickVsixLogFilePathPrinter>(_uninstallVsixSubProgram, "p_quickVsixLogFilePathPrinter");
      p_vsixZipFileReaderMock = Mock.Component<VsixZipFileReader>(_uninstallVsixSubProgram, "p_vsixZipFileReader");
   }

   [Test]
   public void Run_RunsVSIXInstallerWithUninstallArgsWhichReturns1002_WritesAlreadyUninstalledMessageReturns0()
   {
      Mock.Expect(() => p_consoleWriterMock.WriteProgramNameTimestampedLine(null));

      Mock.Expect(() => p_processKillerMock.KillAllPotentiallyBlockingProcesses());

      string extensionGuid = Mock.ReturnRandomString(() => p_vsixZipFileReaderMock.ReadVsixFileForExtensionGuid(null));

      Mock.Expect(() => p_quickVsixLogFilePathPrinterMock.PrintQuickVsixLogFilePath());

      var processResult = TestRandom.ProcessResultWithExitCode(1002);
      Mock.Return(() => p_processRunnerMock.RunWithStandardOutputPrinted(null, null, false), processResult);

      var args = QuickVsixTestRandom.Args();
      //
      int exitCode = _uninstallVsixSubProgram.Run(args);
      //
      string expectedVsixInstallerArgs = $"/uninstall:{extensionGuid} /quiet /shutdownprocesses /logFile:QuickVsix.log";
      Called.NumberOfTimes(4, () => p_consoleWriterMock.WriteProgramNameTimestampedLine(null));
      Called.WasCalled(() => p_consoleWriterMock.WriteProgramNameTimestampedLine($"Uninstalling Visual Studio extension {args.vsixFilePath}")).Then(
      Called.WasCalled(() => p_consoleWriterMock.WriteProgramNameTimestampedLine(""))).Then(
      Called.Once(() => p_processKillerMock.KillAllPotentiallyBlockingProcesses())).Then(
      Called.Once(() => p_vsixZipFileReaderMock.ReadVsixFileForExtensionGuid(args.vsixFilePath))).Then(
      Called.Once(() => p_quickVsixLogFilePathPrinterMock.PrintQuickVsixLogFilePath())).Then(
      Called.WasCalled(() => p_processRunnerMock.RunWithStandardOutputPrinted("VSIXInstaller.exe", expectedVsixInstallerArgs, true))).Then(
      Called.WasCalled(() => p_consoleWriterMock.WriteProgramNameTimestampedLine(
         $"VSIXInstaller.exe exited with code 1002 - meaning the extension is already uninstalled"))).Then(
      Called.WasCalled(() => p_consoleWriterMock.WriteProgramNameTimestampedLine("")));
      Assert.Zero(exitCode);
   }

   [TestCase(-1)]
   [TestCase(1)]
   [TestCase(1001)]
   [TestCase(1003)]
   public void Run_RunsVSIXInstallerWithUninstallArgsWhichReturnsNot01002AndNot0_WritesFailedMessage_ReturnsVSIXInstallerExitCode(
      int vsixInstallerExitCode)
   {
      Mock.Expect(() => p_consoleWriterMock.WriteProgramNameTimestampedLine(null));

      Mock.Expect(() => p_quickVsixLogFilePathPrinterMock.PrintQuickVsixLogFilePath());

      string extensionGuid = Mock.ReturnRandomString(() => p_vsixZipFileReaderMock.ReadVsixFileForExtensionGuid(null));

      Mock.Expect(() => p_processKillerMock.KillAllPotentiallyBlockingProcesses());

      var processResult = TestRandom.ProcessResultWithExitCode(vsixInstallerExitCode);
      Mock.Return(() => p_processRunnerMock.RunWithStandardOutputPrinted(null, null, false), processResult);

      var args = QuickVsixTestRandom.Args();
      //
      int exitCode = _uninstallVsixSubProgram.Run(args);
      //
      string expectedVsixInstallerArgs = $"/uninstall:{extensionGuid} /quiet /shutdownprocesses /logFile:QuickVsix.log";
      Called.NumberOfTimes(4, () => p_consoleWriterMock.WriteProgramNameTimestampedLine(null));
      Called.WasCalled(() => p_consoleWriterMock.WriteProgramNameTimestampedLine($"Uninstalling Visual Studio extension {args.vsixFilePath}")).Then(
      Called.WasCalled(() => p_consoleWriterMock.WriteProgramNameTimestampedLine(""))).Then(
      Called.Once(() => p_processKillerMock.KillAllPotentiallyBlockingProcesses())).Then(
      Called.Once(() => p_vsixZipFileReaderMock.ReadVsixFileForExtensionGuid(args.vsixFilePath))).Then(
      Called.Once(() => p_quickVsixLogFilePathPrinterMock.PrintQuickVsixLogFilePath())).Then(
      Called.WasCalled(() => p_processRunnerMock.RunWithStandardOutputPrinted("VSIXInstaller.exe", expectedVsixInstallerArgs, true))).Then(
      Called.WasCalled(() => p_consoleWriterMock.WriteProgramNameTimestampedLine(
         $"VSIXInstaller.exe failed with exit code {processResult.exitCode}"))).Then(
      Called.WasCalled(() => p_consoleWriterMock.WriteProgramNameTimestampedLine("")));
      Assert.AreEqual(vsixInstallerExitCode, exitCode);
   }

   [Test]
   public void Run_RunsVSIXInstallerWithUninstallArgsWhichReturns0_WritesSuccessfullyUninstalledMessage_Returns0()
   {
      Mock.Expect(() => p_consoleWriterMock.WriteProgramNameTimestampedLine(null));

      Mock.Expect(() => p_quickVsixLogFilePathPrinterMock.PrintQuickVsixLogFilePath());

      string extensionGuid = Mock.ReturnRandomString(() => p_vsixZipFileReaderMock.ReadVsixFileForExtensionGuid(null));

      Mock.Expect(() => p_processKillerMock.KillAllPotentiallyBlockingProcesses());

      var processResult = TestRandom.ProcessResultWithExitCode(0);
      Mock.Return(() => p_processRunnerMock.RunWithStandardOutputPrinted(null, null, false), processResult);

      var args = QuickVsixTestRandom.Args();
      //
      int exitCode = _uninstallVsixSubProgram.Run(args);
      //
      string expectedVsixInstallerArgs = $"/uninstall:{extensionGuid} /quiet /shutdownprocesses /logFile:QuickVsix.log";
      Called.NumberOfTimes(4, () => p_consoleWriterMock.WriteProgramNameTimestampedLine(null));
      Called.WasCalled(() => p_consoleWriterMock.WriteProgramNameTimestampedLine($"Uninstalling Visual Studio extension {args.vsixFilePath}")).Then(
      Called.WasCalled(() => p_consoleWriterMock.WriteProgramNameTimestampedLine(""))).Then(
      Called.Once(() => p_processKillerMock.KillAllPotentiallyBlockingProcesses())).Then(
      Called.Once(() => p_vsixZipFileReaderMock.ReadVsixFileForExtensionGuid(args.vsixFilePath))).Then(
      Called.Once(() => p_quickVsixLogFilePathPrinterMock.PrintQuickVsixLogFilePath())).Then(
      Called.Once(() => p_processRunnerMock.RunWithStandardOutputPrinted("VSIXInstaller.exe", expectedVsixInstallerArgs, true))).Then(
      Called.WasCalled(() => p_consoleWriterMock.WriteProgramNameTimestampedLine(
         $"VSIXInstaller.exe successfully uninstalled Visual Studio extension {args.vsixFilePath}"))).Then(
      Called.WasCalled(() => p_consoleWriterMock.WriteProgramNameTimestampedLine("")));
      Assert.Zero(exitCode);
   }
}
