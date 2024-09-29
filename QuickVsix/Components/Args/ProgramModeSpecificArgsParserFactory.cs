using System;

public class ProgramModeSpecificArgsParserFactory
{
   public virtual ProgramModeSpecificArgsParser New(ProgramMode programMode)
   {
      switch (programMode)
      {
         case ProgramMode.InstallVsix: return new InstallVsixArgsParser();
         case ProgramMode.UninstallVsix: return new UninstallVsixArgsParser();
         case ProgramMode.ReinstallVsix: return new ReinstallVsixArgsParser();
         default: throw new ArgumentException($"Invalid ProgramMode: {programMode}");
      }
   }
}
