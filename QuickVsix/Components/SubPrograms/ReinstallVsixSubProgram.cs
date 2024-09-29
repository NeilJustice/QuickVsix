public class ReinstallVsixSubProgram : QuickVsixSubProgram
{
   private readonly UninstallVsixSubProgram _uninstallVsixSubProgram = new UninstallVsixSubProgram();
   private readonly InstallVsixSubProgram _installVsixSubProgram = new InstallVsixSubProgram();

   public override int Run(QuickVsixArgs args)
   {
      int uninstallExitCode = _uninstallVsixSubProgram.Run(args);
      if (uninstallExitCode != 0)
      {
         return uninstallExitCode;
      }
      int installExitCode = _installVsixSubProgram.Run(args);
      return installExitCode;
   }
}
