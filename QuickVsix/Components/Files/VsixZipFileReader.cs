using CSharpUtils;
using System.Collections.ObjectModel;

public class VsixZipFileReader
{
   private readonly ConsoleWriter _consoleWriter = new ConsoleWriter("QuickVsix");
   private readonly LinqHelper _linqHelper = new LinqHelper();
   private readonly Regexer _regexer = new Regexer();
   private readonly ZipFileReader _zipFileReader = new ZipFileReader();

   public virtual void PrintFileNamesContainedInVsixFile(string vsixFilePath)
   {
      _consoleWriter.WriteProgramNameTimestampedLine($"Reading {vsixFilePath} for extension file names:");
      ReadOnlyCollection<string> vsixFileNames = _zipFileReader.ReadFileNamesContainedInZipFile(vsixFilePath);
      _linqHelper.ForEach(vsixFileNames, _consoleWriter.WriteProgramNameTimestampedLine);
      _consoleWriter.WriteProgramNameTimestampedLine("");
   }

   public virtual string ReadVsixFileForExtensionGuid(string vsixFilePath)
   {
      _consoleWriter.WriteProgramNameTimestampedLine($"Reading {vsixFilePath} for the extension GUID contained in manifest.json");
      string manifestJsonText = _zipFileReader.ReadFileTextOfFileContainedInZipFile(vsixFilePath, "manifest.json");
      // "vsixId":"9def4441-acfd-4dd6-bffd-4531a57dee37"
      string extensionGuid = _regexer.MatchAndGetGroup(manifestJsonText, $@"(?<=""vsixId"":"")(?<ExtensionGuid>\w+-\w+-\w+-\w+-\w+)(?="")", "ExtensionGuid");
      _consoleWriter.WriteProgramNameTimestampedLine($"Extension GUID: {extensionGuid}");
      _consoleWriter.WriteProgramNameTimestampedLine("");
      return extensionGuid;
   }
}
