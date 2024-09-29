using CSharpUtils;

public class QuickVsixSubProgram
{
   protected readonly ConsoleWriter p_consoleWriter = new ConsoleWriter("QuickVsix");
   protected readonly ProcessKiller p_processKiller = new ProcessKiller();
   protected readonly ProcessRunner p_processRunner = new ProcessRunner("QuickVsix");
   protected readonly QuickVsixLogFilePathPrinter p_quickVsixLogFilePathPrinter = new QuickVsixLogFilePathPrinter();
   protected readonly VsixZipFileReader p_vsixZipFileReader = new VsixZipFileReader();

   public virtual int Run(QuickVsixArgs args)
   {
      return 0;
   }
}
