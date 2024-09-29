using CSharpUtils;

public class UninstallVsixSubProgram : QuickVsixSubProgram
{
   public override int Run(QuickVsixArgs args)
   {
      p_consoleWriter.WriteProgramNameTimestampedLine($"Uninstalling Visual Studio extension {args.vsixFilePath}");
      p_consoleWriter.WriteProgramNameTimestampedLine("");

      p_processKiller.KillAllPotentiallyBlockingProcesses();

      string extensionGuid = p_vsixZipFileReader.ReadVsixFileForExtensionGuid(args.vsixFilePath);

      p_quickVsixLogFilePathPrinter.PrintQuickVsixLogFilePath();

      string vsixInstallerArgs = $"/uninstall:{extensionGuid} /quiet /shutdownprocesses /logFile:QuickVsix.log";
      ProcessResult processResult = p_processRunner.RunWithStandardOutputPrinted("VSIXInstaller.exe", vsixInstallerArgs, true);

      if (processResult.exitCode == 1002)
      {
         p_consoleWriter.WriteProgramNameTimestampedLine(
            $"VSIXInstaller.exe exited with code 1002 - meaning the extension is already uninstalled");
         p_consoleWriter.WriteProgramNameTimestampedLine("");
         return 0;
      }
      if (processResult.exitCode != 0)
      {
         p_consoleWriter.WriteProgramNameTimestampedLine($"VSIXInstaller.exe failed with exit code {processResult.exitCode}");
         p_consoleWriter.WriteProgramNameTimestampedLine("");
         return processResult.exitCode;
      }
      p_consoleWriter.WriteProgramNameTimestampedLine(
         $"VSIXInstaller.exe successfully uninstalled Visual Studio extension {args.vsixFilePath}");
      p_consoleWriter.WriteProgramNameTimestampedLine("");
      return 0;
   }
}
