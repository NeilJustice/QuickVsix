using DocoptPlus;
using System.Collections.ObjectModel;

public class InstallVsixArgsParser : ProgramModeSpecificArgsParser
{
   public override QuickVsixArgs ParseDocoptDictionary(ReadOnlyDictionary<string, DocoptValueObject> docoptDictionary, ProgramMode programMode)
   {
      var args = new QuickVsixArgs();
      args.programMode = programMode;
      args.vsixFilePath = p_docoptParser.GetRequiredFilePathWhichMustExist(docoptDictionary, "--vsix-file");
      args.waitForAnyKey = p_docoptParser.GetOptionalBool(docoptDictionary, "--wait-for-any-key");
      return args;
   }
}
