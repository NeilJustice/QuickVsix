using System;

public class QuickVsixSubProgramFactory
{
   public virtual QuickVsixSubProgram New(ProgramMode programMode)
   {
      switch (programMode)
      {
         case ProgramMode.InstallVsix: return new InstallVsixSubProgram();
         case ProgramMode.UninstallVsix: return new UninstallVsixSubProgram();
         case ProgramMode.ReinstallVsix: return new ReinstallVsixSubProgram();
         default: throw new ArgumentException($"Invalid ProgramMode: {programMode}");
      }
   }
}
