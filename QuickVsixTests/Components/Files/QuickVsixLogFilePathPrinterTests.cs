using CSharpUtils;
using NUnit.Framework;

[TestFixture]
public class QuickVsixLogFilePathPrinterTests
{
   QuickVsixLogFilePathPrinter _quickVsixLogFilePathPrinter;
   ConsoleWriter _consoleWriterMock;
   Environmentalist _environmentalistMock;
   FileSystem _fileSystemMock;

   [SetUp]
   public void SetUp()
   {
      _quickVsixLogFilePathPrinter = new QuickVsixLogFilePathPrinter();
      Assert2.AssertConsoleWriterHasProgramName("QuickVsix", _quickVsixLogFilePathPrinter, "_consoleWriter");
      _consoleWriterMock = Mock.Component<ConsoleWriter>(_quickVsixLogFilePathPrinter, "_consoleWriter");
      _environmentalistMock = Mock.Component<Environmentalist>(_quickVsixLogFilePathPrinter, "_environmentalist");
      _fileSystemMock = Mock.Component<FileSystem>(_quickVsixLogFilePathPrinter, "_fileSystem");
   }

   [Test]
   public void GetQuickVsixLogFilePath_GetsTEMPMachineEnvironmentVariable_ReturnsTEMPBackslashQuickVsixDotLog()
   {
      string tempFolderPath = Mock.ReturnRandomString(() => _environmentalistMock.GetMachineEnvironmentVariable(null));
      string quickVsixLogFilePath = Mock.ReturnRandomString(() => _fileSystemMock.CombineTwoPaths(null, null));
      Mock.Expect(() => _consoleWriterMock.WriteProgramNameTimestampedLine(null));
      //
      _quickVsixLogFilePathPrinter.PrintQuickVsixLogFilePath();
      //
      Called.NumberOfTimes(2, () => _consoleWriterMock.WriteProgramNameTimestampedLine(null));
      Called.Once(() => _environmentalistMock.GetMachineEnvironmentVariable("TEMP")).Then(
      Called.Once(() => _fileSystemMock.CombineTwoPaths(tempFolderPath, "QuickVsix.log"))).Then(
      Called.WasCalled(() => _consoleWriterMock.WriteProgramNameTimestampedLine($"VSIXInstaller.exe log file: {quickVsixLogFilePath}"))).Then(
      Called.WasCalled(() => _consoleWriterMock.WriteProgramNameTimestampedLine("")));
   }
}
