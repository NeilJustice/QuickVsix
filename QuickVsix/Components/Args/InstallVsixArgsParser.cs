using DocoptPlus;
using System.Collections.ObjectModel;

public class InstallVsixArgsParser : ProgramModeSpecificArgsParser
{
   public override QuickVsixArgs ParseDocoptDictionary(ReadOnlyDictionary<string, DocoptValueObject> docoptDictionary)
   {
      var args = new QuickVsixArgs();
      args.programMode = ProgramMode.InstallVsix;
      args.vsixFilePath = p_docoptParser.GetRequiredFilePathWhichMustExist(docoptDictionary, "--vsix-file");
      args.waitForAnyKey = p_docoptParser.GetOptionalBool(docoptDictionary, "--wait-for-any-key");
      return args;
   }
}
