using System.Collections.ObjectModel;
using CSharpUtils;
using NUnit.Framework;

[TestFixture]
public class VsixZipFileReaderTests
{
   VsixZipFileReader _vsixZipFileReader;
   ConsoleWriter _consoleWriterMock;
   LinqHelper _linqHelperMock;
   Regexer _regexerMock;
   ZipFileReader _zipFileReaderMock;

   [SetUp]
   public void SetUp()
   {
      _vsixZipFileReader = new VsixZipFileReader();
      Assert2.AssertConsoleWriterHasProgramName("QuickVsix", _vsixZipFileReader, "_consoleWriter");
      _consoleWriterMock = Mock.Component<ConsoleWriter>(_vsixZipFileReader, "_consoleWriter");
      _linqHelperMock = Mock.Component<LinqHelper>(_vsixZipFileReader, "_linqHelper");
      _regexerMock = Mock.Component<Regexer>(_vsixZipFileReader, "_regexer");
      _zipFileReaderMock = Mock.Component<ZipFileReader>(_vsixZipFileReader, "_zipFileReader");
   }

   [Test]
   public void PrintFileNamesContainedInVsixFile_PrintsFileNamesContainedInVsixFile()
   {
      Mock.Expect(() => _consoleWriterMock.WriteProgramNameTimestampedLine(null));
      Mock.Expect(() => _consoleWriterMock.WriteNewLine());

      ReadOnlyCollection<string> vsixFileNames = Mock.ReturnRandomReadOnlyStringCollection(
         () => _zipFileReaderMock.ReadFileNamesContainedInZipFile(null));

      Mock.Expect(() => _linqHelperMock.ForEach(default(ReadOnlyCollection<string>), default(Action<string>)));

      string vsixFilePath = TestRandom.String();
      //
      _vsixZipFileReader.PrintFileNamesContainedInVsixFile(vsixFilePath);
      //
      Called.NumberOfTimes(2, () => _consoleWriterMock.WriteNewLine());
      Called.Once(() => _consoleWriterMock.WriteProgramNameTimestampedLine($"Reading {vsixFilePath} for extension file names:")).Then(
      Called.WasCalled(() => _consoleWriterMock.WriteNewLine())).Then(
      Called.Once(() => _zipFileReaderMock.ReadFileNamesContainedInZipFile(vsixFilePath))).Then(
      Called.WasCalled(() => _linqHelperMock.ForEach(vsixFileNames, _consoleWriterMock.WriteLine))).Then(
      Called.WasCalled(() => _consoleWriterMock.WriteNewLine()));
   }

   [Test]
   public void ReadVsixFileForExtensionGuid_ReadsVsixZipFileForManifestJsonExtensionGuid_ReturnsExtensionGuid()
   {
      Mock.Expect(() => _consoleWriterMock.WriteProgramNameTimestampedLine(null));
      Mock.Expect(() => _consoleWriterMock.WriteNewLine());
      Mock.Expect(() => _consoleWriterMock.WriteLine(null));

      string extensionDotVsixmanifestFileText =
         Mock.ReturnRandomString(() => _zipFileReaderMock.ReadFileTextOfFileContainedInZipFile(null, null));

      string extensionGuid = Mock.ReturnRandomString(() => _regexerMock.MatchAndGetGroup(null, null, null));

      string vsixFilePath = TestRandom.String();
      //
      string returnedExtensionGuid = _vsixZipFileReader.ReadVsixFileForExtensionGuid(vsixFilePath);
      //
      Called.NumberOfTimes(2, () => _consoleWriterMock.WriteNewLine());

      Called.Once(() => _consoleWriterMock.WriteProgramNameTimestampedLine(
         $"Reading {vsixFilePath} for extension.vsixmanifest:")).Then(
      Called.Once(() => _zipFileReaderMock.ReadFileTextOfFileContainedInZipFile(vsixFilePath, "extension.vsixmanifest"))).Then(

      Called.WasCalled(() => _consoleWriterMock.WriteNewLine())).Then(
      Called.Once(() => _consoleWriterMock.WriteLine(extensionDotVsixmanifestFileText))).Then(
      Called.WasCalled(() => _consoleWriterMock.WriteNewLine())).Then(

      Called.Once(() => _regexerMock.MatchAndGetGroup(
         extensionDotVsixmanifestFileText, $@"(?<=Identity Id="")(?<ExtensionGuid>\w+-\w+-\w+-\w+-\w+)(?="")", "ExtensionGuid")));

      Assert.AreEqual(extensionGuid, returnedExtensionGuid);
   }
}
