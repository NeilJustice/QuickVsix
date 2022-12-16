using CSharpUtils;
using NUnit.Framework;

[TestFixture]
public class PreamblePrinterTests
{
   PreamblePrinter _preamblePrinter;
   ConsoleWriter _consoleWriterMock;
   Environmentalist _environmentalistMock;

   [SetUp]
   public void SetUp()
   {
      _preamblePrinter = new PreamblePrinter();
      Assert2.AssertConsoleWriterHasProgramName("QuickVsix", _preamblePrinter, "_consoleWriter");
      _consoleWriterMock = Mock.Component<ConsoleWriter>(_preamblePrinter, "_consoleWriter");
      _environmentalistMock = Mock.Component<Environmentalist>(_preamblePrinter, "_environmentalist");
   }

   [Test]
   public void PrintPreamble_PrintsMachineName_PrintsUserName_PrintsCurrentDirectory_SetsCurrentDirectoryToAcceptanceTestFolderPath_PrintsSetCurrentDirectoryMessage()
   {
      string machineName = Mock.ReturnRandomString(() => _environmentalistMock.MachineName());
      string userName = Mock.ReturnRandomString(() => _environmentalistMock.UserName());
      string currentDirectoryPath = Mock.ReturnRandomString(() => _environmentalistMock.CurrentDirectoryPath());
      Mock.Expect(() => _consoleWriterMock.WriteProgramNameTimestampedLine(null));

      var args = QuickVsixTestRandom.Args();
      //
      _preamblePrinter.PrintPreamble(args);
      //
      Called.NumberOfTimes(4, () => _consoleWriterMock.WriteProgramNameTimestampedLine(null));
      Called.Once(() => _environmentalistMock.MachineName()).Then(
      Called.Once(() => _environmentalistMock.UserName())).Then(
      Called.Once(() => _environmentalistMock.CurrentDirectoryPath()));
      Called.WasCalled(() => _consoleWriterMock.WriteProgramNameTimestampedLine($"MachineName: {machineName}")).Then(
      Called.WasCalled(() => _consoleWriterMock.WriteProgramNameTimestampedLine($"   UserName: {userName}"))).Then(
      Called.WasCalled(() => _consoleWriterMock.WriteProgramNameTimestampedLine($"  Directory: {currentDirectoryPath}"))).Then(
      Called.WasCalled(() => _consoleWriterMock.WriteProgramNameTimestampedLine("")));
   }
}
