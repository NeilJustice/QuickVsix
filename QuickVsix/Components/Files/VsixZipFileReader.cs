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
      _consoleWriter.WriteNewLine();
      ReadOnlyCollection<string> vsixFileNames = _zipFileReader.ReadFileNamesContainedInZipFile(vsixFilePath);
      _linqHelper.ForEach(vsixFileNames, _consoleWriter.WriteLine);
      _consoleWriter.WriteNewLine();
   }

   public virtual string ReadVsixFileForExtensionGuid(string vsixFilePath)
   {
      _consoleWriter.WriteProgramNameTimestampedLine($"Reading {vsixFilePath} for extension.vsixmanifest:");
      string extensionDotVsixmanifestFileText = _zipFileReader.ReadFileTextOfFileContainedInZipFile(vsixFilePath, "extension.vsixmanifest");

      _consoleWriter.WriteNewLine();
      _consoleWriter.WriteLine(extensionDotVsixmanifestFileText);
      _consoleWriter.WriteNewLine();

      // Identity Id="9def4441-acfd-4dd6-bfgf-4531a57dee37"
      string extensionGuid = _regexer.MatchAndGetGroup(
         extensionDotVsixmanifestFileText, $@"(?<=Identity Id="")(?<ExtensionGuid>\w+-\w+-\w+-\w+-\w+)(?="")", "ExtensionGuid");

      return extensionGuid;
   }
}
