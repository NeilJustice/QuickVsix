using CSharpUtils;

public class PreamblePrinter
{
   private readonly ConsoleWriter _consoleWriter = new ConsoleWriter("QuickVsix");
   private readonly Environmentalist _environmentalist = new Environmentalist();

   public virtual void PrintPreamble(QuickVsixArgs args)
   {
      string machineName = _environmentalist.MachineName();
      string userName = _environmentalist.UserName();
      string currentDirectoryPath = _environmentalist.CurrentDirectoryPath();
      _consoleWriter.WriteProgramNameTimestampedLine($"MachineName: {machineName}");
      _consoleWriter.WriteProgramNameTimestampedLine($"   UserName: {userName}");
      _consoleWriter.WriteProgramNameTimestampedLine($"  Directory: {currentDirectoryPath}");
      _consoleWriter.WriteProgramNameTimestampedLine("");
   }
}
