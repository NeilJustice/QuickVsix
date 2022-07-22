using CSharpUtils;
using System.Collections.ObjectModel;
using System.Diagnostics;

public class ProcessKiller
{
   private readonly ConsoleWriter _consoleWriter = new ConsoleWriter("QuickVsix");
   private readonly LinqHelper _linqHelper = new LinqHelper();
   private readonly Pluralizer _pluralizer = new Pluralizer();
   private readonly ProcessHelper _processHelper = new ProcessHelper();
   private readonly ReadOnlyCollectionMaker _readOnlyCollectionMaker = new ReadOnlyCollectionMaker();

   public virtual void KillProcess(string processName)
   {
      Process[] mutableProcessesWithName = _processHelper.GetProcessesByName(processName);
      ReadOnlyCollection<Process> readOnlyProcessesWithName = _readOnlyCollectionMaker.MakeReadOnlyCollection(mutableProcessesWithName);
      _consoleWriter.WriteProgramNameTimestampedLine("Killing all mspdbsrv.exe processes which delay installs and uninstalls of .vsix extensions");
      _linqHelper.ForEach(readOnlyProcessesWithName, DoKillProcess);
      string processOrProcesses = _pluralizer.PluralizeIfNot1(readOnlyProcessesWithName.Count, "process", "processes");
      _consoleWriter.WriteProgramNameTimestampedLine($"Killed {readOnlyProcessesWithName.Count} mspdbsrv.exe {processOrProcesses}");
      _consoleWriter.WriteProgramNameTimestampedLine("");
   }

   public void DoKillProcess(Process process)
   {
      string processName = _processHelper.GetProcessName(process);
      _processHelper.KillProcess(process);
   }
}
