using CSharpUtils;

public class QuickVsixLogFilePathPrinter
{
   private readonly ConsoleWriter _consoleWriter = new ConsoleWriter("QuickVsix");
   private readonly Environmentalist _environmentalist = new Environmentalist();
   private readonly FileSystem _fileSystem = new FileSystem();

   public virtual void PrintQuickVsixLogFilePath()
   {
      string tempFolderPath = _environmentalist.GetUserEnvironmentVariable("TEMP");
      string quickVsixLogFilePath = _fileSystem.CombineTwoPaths(tempFolderPath, "QuickVsix.log");
      _consoleWriter.WriteProgramNameTimestampedLine($"VSIXInstaller.exe log file: {quickVsixLogFilePath}");
   }
}
