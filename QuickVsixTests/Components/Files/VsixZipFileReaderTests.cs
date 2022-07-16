using CSharpUtils;
using NUnit.Framework;
using System;
using System.Collections.ObjectModel;

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

      ReadOnlyCollection<string> vsixFileNames = Mock.ReturnRandomReadOnlyStringCollection(
         () => _zipFileReaderMock.ReadFileNamesContainedInZipFile(null));

      Mock.Expect(() => _linqHelperMock.ForEach(default(ReadOnlyCollection<string>), default(Action<string>)));

      string vsixFilePath = TestRandom.String();
      //
      _vsixZipFileReader.PrintFileNamesContainedInVsixFile(vsixFilePath);
      //
      Called.NumberOfTimes(2, () => _consoleWriterMock.WriteProgramNameTimestampedLine(null));
      Called.WasCalled(() => _consoleWriterMock.WriteProgramNameTimestampedLine($"Reading {vsixFilePath} for extension file names:")).Then(
      Called.Once(() => _zipFileReaderMock.ReadFileNamesContainedInZipFile(vsixFilePath))).Then(
      Called.WasCalled(() => _linqHelperMock.ForEach(vsixFileNames, _consoleWriterMock.WriteProgramNameTimestampedLine))).Then(
      Called.WasCalled(() => _consoleWriterMock.WriteProgramNameTimestampedLine("")));
   }

   [Test]
   public void ReadVsixFileForExtensionGuid_ReadsVsixZipFileForManifestJsonExtensionGuid_ReturnsExtensionGuid()
   {
      Mock.Expect(() => _consoleWriterMock.WriteProgramNameTimestampedLine(null));

      string manifestJsonText = Mock.ReturnRandomString(() => _zipFileReaderMock.ReadFileTextOfFileContainedInZipFile(null, null));

      string extensionGuid = Mock.ReturnRandomString(() => _regexerMock.MatchAndGetGroup(null, null, null));

      string vsixFilePath = TestRandom.String();
      //
      string returnedExtensionGuid = _vsixZipFileReader.ReadVsixFileForExtensionGuid(vsixFilePath);
      //
      Called.NumberOfTimes(3, () => _consoleWriterMock.WriteProgramNameTimestampedLine(null));
      Called.WasCalled(() => _consoleWriterMock.WriteProgramNameTimestampedLine($"Reading {vsixFilePath} for the extension GUID contained in manifest.json")).Then(
      Called.Once(() => _zipFileReaderMock.ReadFileTextOfFileContainedInZipFile(vsixFilePath, "manifest.json"))).Then(
      Called.Once(() => _regexerMock.MatchAndGetGroup(manifestJsonText, $@"(?<=""vsixId"":"")(?<ExtensionGuid>\w+-\w+-\w+-\w+-\w+)(?="")", "ExtensionGuid"))).Then(
      Called.WasCalled(() => _consoleWriterMock.WriteProgramNameTimestampedLine($"Extension GUID: {extensionGuid}"))).Then(
      Called.WasCalled(() => _consoleWriterMock.WriteProgramNameTimestampedLine("")));
      Assert.AreEqual(extensionGuid, returnedExtensionGuid);
   }
}
