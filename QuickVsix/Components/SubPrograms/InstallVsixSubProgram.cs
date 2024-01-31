using CSharpUtils;

public class InstallVsixSubProgram : QuickVsixSubProgram
{
   public override int Run(QuickVsixArgs args)
   {
      p_consoleWriter.WriteProgramNameTimestampedLine($"Installing Visual Studio extension {args.vsixFilePath}");
      p_consoleWriter.WriteProgramNameTimestampedLine("");

      p_processKiller.KillAllPotentiallyBlockingProcesses();

      p_vsixZipFileReader.ReadVsixFileForExtensionGuid(args.vsixFilePath);

      p_vsixZipFileReader.PrintFileNamesContainedInVsixFile(args.vsixFilePath);

      p_quickVsixLogFilePathPrinter.PrintQuickVsixLogFilePath();

      string vsixInstallerArgs = $"{args.vsixFilePath} /quiet /shutdownprocesses /norepair /logFile:QuickVsix.log";
      ProcessResult processResult = p_processRunner.RunWithStandardOutputPrinted("VSIXInstaller.exe", vsixInstallerArgs, true);
      if (processResult.exitCode == 1001)
      {
         p_consoleWriter.WriteProgramNameTimestampedLine(
            $"VSIXInstaller.exe returned exit code 1001 - meaning the extension {args.vsixFilePath} is already installed");
         p_consoleWriter.WriteProgramNameTimestampedLine("");
         return 0;
      }
      if (processResult.exitCode != 0)
      {
         p_consoleWriter.WriteProgramNameTimestampedLine(
            $"VSIXInstaller.exe failed with exit code {processResult.exitCode}");
         p_consoleWriter.WriteProgramNameTimestampedLine("");
         return processResult.exitCode;
      }
      p_consoleWriter.WriteProgramNameTimestampedLine(
         $"VSIXInstaller.exe successfully installed Visual Studio extension {args.vsixFilePath}");
      p_consoleWriter.WriteProgramNameTimestampedLine("");
      return 0;
   }
}
