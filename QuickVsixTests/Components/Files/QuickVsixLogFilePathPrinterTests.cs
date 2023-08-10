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
      string tempFolderPath = Mock.ReturnRandomString(() => _environmentalistMock.GetUserEnvironmentVariable(null));
      string quickVsixLogFilePath = Mock.ReturnRandomString(() => _fileSystemMock.CombineTwoPaths(null, null));
      Mock.Expect(() => _consoleWriterMock.WriteProgramNameTimestampedLine(null));
      //
      _quickVsixLogFilePathPrinter.PrintQuickVsixLogFilePath();
      //
      Called.Once(() => _environmentalistMock.GetUserEnvironmentVariable("TEMP")).Then(
      Called.Once(() => _fileSystemMock.CombineTwoPaths(tempFolderPath, "QuickVsix.log"))).Then(
      Called.Once(() => _consoleWriterMock.WriteProgramNameTimestampedLine($"VSIXInstaller.exe log file: {quickVsixLogFilePath}")));
   }
}
