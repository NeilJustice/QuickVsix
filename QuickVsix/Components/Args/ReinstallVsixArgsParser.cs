using DocoptPlus;
using System.Collections.ObjectModel;

public class ReinstallVsixArgsParser : ProgramModeSpecificArgsParser
{
   public override QuickVsixArgs ParseDocoptDictionary(ReadOnlyDictionary<string, DocoptValueObject> docoptDictionary)
   {
      var args = new QuickVsixArgs();
      args.programMode = ProgramMode.ReinstallVsix;
      args.vsixFilePath = p_docoptParser.GetRequiredString(docoptDictionary, "--vsix-file");
      args.waitForAnyKey = p_docoptParser.GetOptionalBool(docoptDictionary, "--wait-for-any-key");
      return args;
   }
}
