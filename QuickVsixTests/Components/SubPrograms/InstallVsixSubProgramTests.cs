using CSharpUtils;
using NUnit.Framework;

[TestFixture]
public class InstallVsixSubProgramTests
{
   InstallVsixSubProgram _installVsixSubProgram;
   ConsoleWriter p_consoleWriterMock;
   ProcessRunner p_processRunnerMock;
   ProcessKiller p_processKillerMock;
   QuickVsixLogFilePathPrinter p_quickVsixLogFilePathPrinterMock;
   VsixZipFileReader p_vsixZipFileReaderMock;

   [SetUp]
   public void SetUp()
   {
      _installVsixSubProgram = new InstallVsixSubProgram();
      p_consoleWriterMock = Mock.Component<ConsoleWriter>(_installVsixSubProgram, "p_consoleWriter");
      p_processRunnerMock = Mock.Component<ProcessRunner>(_installVsixSubProgram, "p_processRunner");
      p_processKillerMock = Mock.Component<ProcessKiller>(_installVsixSubProgram, "p_processKiller");
      p_quickVsixLogFilePathPrinterMock = Mock.Component<QuickVsixLogFilePathPrinter>(_installVsixSubProgram, "p_quickVsixLogFilePathPrinter");
      p_vsixZipFileReaderMock = Mock.Component<VsixZipFileReader>(_installVsixSubProgram, "p_vsixZipFileReader");
   }

   [Test]
   public void Run_RunsVSIXInstallerToInstallsVsixWhichExitsWithCode1001_WritesAlreadyInstalledMessage_Returns0()
   {
      Mock.Expect(() => p_consoleWriterMock.WriteProgramNameTimestampedLine(null));

      Mock.Expect(() => p_processKillerMock.KillAllPotentiallyBlockingProcesses());

      Mock.Expect(() => p_vsixZipFileReaderMock.ReadVsixFileForExtensionGuid(null));

      Mock.Expect(() => p_vsixZipFileReaderMock.PrintFileNamesContainedInVsixFile(null));

      Mock.Expect(() => p_quickVsixLogFilePathPrinterMock.PrintQuickVsixLogFilePath());

      ProcessResult processResult = TestRandom.ProcessResultWithExitCode(1001);
      Mock.Return(() => p_processRunnerMock.RunWithStandardOutputPrinted(null, null, false), processResult);

      var args = QuickVsixTestRandom.Args();
      //
      int exitCode = _installVsixSubProgram.Run(args);
      //
      string expectedVSIXInstallerArgs = $"{args.vsixFilePath} /quiet /shutdownprocesses /norepair /logFile:QuickVsix.log";
      Called.NumberOfTimes(4, () => p_consoleWriterMock.WriteProgramNameTimestampedLine(null));
      Called.WasCalled(() => p_consoleWriterMock.WriteProgramNameTimestampedLine($"Installing Visual Studio extension {args.vsixFilePath}")).Then(
      Called.WasCalled(() => p_consoleWriterMock.WriteProgramNameTimestampedLine(""))).Then(
      Called.Once(() => p_processKillerMock.KillAllPotentiallyBlockingProcesses())).Then(
      Called.Once(() => p_vsixZipFileReaderMock.ReadVsixFileForExtensionGuid(args.vsixFilePath))).Then(
      Called.Once(() => p_vsixZipFileReaderMock.PrintFileNamesContainedInVsixFile(args.vsixFilePath))).Then(
      Called.Once(() => p_quickVsixLogFilePathPrinterMock.PrintQuickVsixLogFilePath())).Then(
      Called.Once(() => p_processRunnerMock.RunWithStandardOutputPrinted("VSIXInstaller.exe", expectedVSIXInstallerArgs, true))).Then(
      Called.WasCalled(() => p_consoleWriterMock.WriteProgramNameTimestampedLine(
         $"VSIXInstaller.exe returned exit code 1001 - meaning the extension {args.vsixFilePath} is already installed"))).Then(
      Called.WasCalled(() => p_consoleWriterMock.WriteProgramNameTimestampedLine("")));
      Assert.Zero(exitCode);
   }

   [TestCase(-1)]
   [TestCase(1)]
   [TestCase(1000)]
   [TestCase(1002)]
   public void Run_RunsVSIXInstallerToInstallsVsixWhichExitsWithCodeNot1001AndNot0_WritesErrorMessage_ReturnsVSIXInstallerExitCode(
      int vsixInstallerExitCode)
   {
      Mock.Expect(() => p_consoleWriterMock.WriteProgramNameTimestampedLine(null));

      Mock.Expect(() => p_processKillerMock.KillAllPotentiallyBlockingProcesses());

      Mock.Expect(() => p_vsixZipFileReaderMock.ReadVsixFileForExtensionGuid(null));

      Mock.Expect(() => p_vsixZipFileReaderMock.PrintFileNamesContainedInVsixFile(null));

      Mock.Expect(() => p_quickVsixLogFilePathPrinterMock.PrintQuickVsixLogFilePath());

      ProcessResult processResult = TestRandom.ProcessResultWithExitCode(vsixInstallerExitCode);
      Mock.Return(() => p_processRunnerMock.RunWithStandardOutputPrinted(null, null, false), processResult);

      var args = QuickVsixTestRandom.Args();
      //
      int exitCode = _installVsixSubProgram.Run(args);
      //
      string expectedVSIXInstallerArgs = $"{args.vsixFilePath} /quiet /shutdownprocesses /norepair /logFile:QuickVsix.log";
      Called.NumberOfTimes(4, () => p_consoleWriterMock.WriteProgramNameTimestampedLine(null));
      Called.WasCalled(() => p_consoleWriterMock.WriteProgramNameTimestampedLine($"Installing Visual Studio extension {args.vsixFilePath}")).Then(
      Called.WasCalled(() => p_consoleWriterMock.WriteProgramNameTimestampedLine(""))).Then(
      Called.Once(() => p_processKillerMock.KillAllPotentiallyBlockingProcesses())).Then(
      Called.Once(() => p_vsixZipFileReaderMock.ReadVsixFileForExtensionGuid(args.vsixFilePath))).Then(
      Called.Once(() => p_vsixZipFileReaderMock.PrintFileNamesContainedInVsixFile(args.vsixFilePath))).Then(
      Called.Once(() => p_quickVsixLogFilePathPrinterMock.PrintQuickVsixLogFilePath())).Then(
      Called.Once(() => p_processRunnerMock.RunWithStandardOutputPrinted("VSIXInstaller.exe", expectedVSIXInstallerArgs, true))).Then(
      Called.WasCalled(() => p_consoleWriterMock.WriteProgramNameTimestampedLine($"VSIXInstaller.exe failed with exit code {processResult.exitCode}"))).Then(
      Called.WasCalled(() => p_consoleWriterMock.WriteProgramNameTimestampedLine("")));
      Assert.AreEqual(vsixInstallerExitCode, exitCode);
   }

   [Test]
   public void Run_RunsVSIXInstallerToInstallsVsixWhichExitsWithCode0_WritesSuccessMessage_Returns0()
   {
      Mock.Expect(() => p_consoleWriterMock.WriteProgramNameTimestampedLine(null));

      Mock.Expect(() => p_processKillerMock.KillAllPotentiallyBlockingProcesses());

      Mock.Expect(() => p_vsixZipFileReaderMock.ReadVsixFileForExtensionGuid(null));

      Mock.Expect(() => p_vsixZipFileReaderMock.PrintFileNamesContainedInVsixFile(null));

      Mock.Expect(() => p_quickVsixLogFilePathPrinterMock.PrintQuickVsixLogFilePath());

      ProcessResult processResult = TestRandom.ProcessResultWithExitCode(0);
      Mock.Return(() => p_processRunnerMock.RunWithStandardOutputPrinted(null, null, false), processResult);

      var args = QuickVsixTestRandom.Args();
      //
      int exitCode = _installVsixSubProgram.Run(args);
      //
      string expectedVSIXInstallerArgs = $"{args.vsixFilePath} /quiet /shutdownprocesses /norepair /logFile:QuickVsix.log";
      Called.NumberOfTimes(4, () => p_consoleWriterMock.WriteProgramNameTimestampedLine(null));
      Called.WasCalled(() => p_consoleWriterMock.WriteProgramNameTimestampedLine($"Installing Visual Studio extension {args.vsixFilePath}")).Then(
      Called.WasCalled(() => p_consoleWriterMock.WriteProgramNameTimestampedLine(""))).Then(
      Called.Once(() => p_processKillerMock.KillAllPotentiallyBlockingProcesses())).Then(
      Called.Once(() => p_vsixZipFileReaderMock.ReadVsixFileForExtensionGuid(args.vsixFilePath))).Then(
      Called.Once(() => p_vsixZipFileReaderMock.PrintFileNamesContainedInVsixFile(args.vsixFilePath))).Then(
      Called.Once(() => p_quickVsixLogFilePathPrinterMock.PrintQuickVsixLogFilePath())).Then(
      Called.Once(() => p_processRunnerMock.RunWithStandardOutputPrinted("VSIXInstaller.exe", expectedVSIXInstallerArgs, true))).Then(
      Called.WasCalled(() => p_consoleWriterMock.WriteProgramNameTimestampedLine(
         $"VSIXInstaller.exe successfully installed Visual Studio extension {args.vsixFilePath}"))).Then(
      Called.WasCalled(() => p_consoleWriterMock.WriteProgramNameTimestampedLine("")));
      Assert.Zero(exitCode);
   }
}
