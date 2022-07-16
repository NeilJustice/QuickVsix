using CSharpUtils;
using DocoptPlus;
using System;
using System.Collections.ObjectModel;

public class ProgramModeSpecificArgsParser
{
   protected readonly ConsoleWriter p_consoleWriter = new ConsoleWriter("QuickVsix");
   protected readonly DocoptParser p_docoptParser = new DocoptParser();
   protected readonly FileSystem p_fileSystem = new FileSystem();

   public virtual QuickVsixArgs ParseDocoptDictionary(ReadOnlyDictionary<string, DocoptValueObject> docoptDictionary, ProgramMode programMode)
   {
      throw new NotSupportedException();
   }
}
