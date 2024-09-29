using CSharpUtils;
using DocoptPlus;
using System.Collections.ObjectModel;

public class QuickVsixArgs : NUnitEquatable, IDocoptArgs
{
   public string ProgramName => "QuickVsix";
   public string Version => "1.1.0";
   public string Usage => @"QuickVsix - Quickly and non-interactively installs, uninstalls, or reinstalls Visual Studio extensions.

Usage:
   QuickVsix.exe install-vsix --vsix-file=<FilePath> [--wait-for-any-key]
   QuickVsix.exe uninstall-vsix --vsix-file=<FilePath> [--wait-for-any-key]
   QuickVsix.exe reinstall-vsix --vsix-file=<FilePath> [--wait-for-any-key]
   QuickVsix.exe --help
   QuickVsix.exe --version";

   public string CommandLine { get; set; }
   public ProgramMode programMode;
   public string vsixFilePath;
   public bool waitForAnyKey;

   public static readonly ReadOnlyDictionary<string, ProgramMode> programModesDictionary = new Dictionary<string, ProgramMode>
   {
      { "install-vsix", ProgramMode.InstallVsix },
      { "uninstall-vsix", ProgramMode.UninstallVsix },
      { "reinstall-vsix", ProgramMode.ReinstallVsix }
   }.ToReadOnlyDictionary();

   public void Populate(ReadOnlyDictionary<string, DocoptValueObject> docoptArguments)
   {
      throw new NotSupportedException();
   }

   public override bool Equals(object actualObject)
   {
      var actual = (QuickVsixArgs)actualObject;
      _nunitAsserter.AreEqual(CommandLine, actual.CommandLine);
      _nunitAsserter.AreEqual(programMode, actual.programMode);
      _nunitAsserter.AreEqual(vsixFilePath, actual.vsixFilePath);
      _nunitAsserter.AreEqual(waitForAnyKey, actual.waitForAnyKey);
      return true;
   }

   public override int GetHashCode()
   {
      throw new NotSupportedException();
   }
}
