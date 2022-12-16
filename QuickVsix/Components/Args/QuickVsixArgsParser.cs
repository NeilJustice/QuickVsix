using CSharpUtils;
using DocoptPlus;
using System.Collections.Generic;
using System.Collections.ObjectModel;

public class QuickVsixArgsParser
{
   private readonly DocoptParser _docoptParser = new DocoptParser();
   private readonly ProgramModeSpecificArgsParserFactory _programModeSpecificArgsParserFactory = new ProgramModeSpecificArgsParserFactory();

   public static readonly ReadOnlyDictionary<string, ProgramMode> s_programModesDictionary = new Dictionary<string, ProgramMode>
   {
      { "install-vsix", ProgramMode.InstallVsix },
      { "uninstall-vsix", ProgramMode.UninstallVsix },
      { "reinstall-vsix", ProgramMode.ReinstallVsix }
   }.ToReadOnlyDictionary();

   public virtual QuickVsixArgs ParseStringArgs(string[] stringArgs)
   {
      ReadOnlyDictionary<string, DocoptValueObject> docoptDictionary = _docoptParser.ParseStringArgsForDocoptDictionary<QuickVsixArgs>(stringArgs);
      ProgramMode programMode = _docoptParser.GetProgramMode(docoptDictionary, QuickVsixArgs.programModesDictionary);
      ProgramModeSpecificArgsParser programModeSpecificArgsParser = _programModeSpecificArgsParserFactory.New(programMode);
      QuickVsixArgs args = programModeSpecificArgsParser.ParseDocoptDictionary(docoptDictionary, programMode);
      return args;
   }
}
