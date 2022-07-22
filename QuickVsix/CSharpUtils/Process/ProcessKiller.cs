using CSharpUtils;
using System.Collections.ObjectModel;
using System.Diagnostics;

public class ProcessKiller
{
   private readonly ConsoleWriter _consoleWriter = new ConsoleWriter("QuickVsix");
   private readonly LinqHelper _linqHelper = new LinqHelper();
   private readonly ProcessHelper _processHelper = new ProcessHelper();
   private readonly ReadOnlyCollectionMaker _readOnlyCollectionMaker = new ReadOnlyCollectionMaker();

   public virtual void KillProcessByName(string processName)
   {
      Process[] mutableProcessesWithName = _processHelper.GetProcessesByName(processName);
      ReadOnlyCollection<Process> readOnlyProcessesWithName = _readOnlyCollectionMaker.MakeReadOnlyCollection(mutableProcessesWithName);
      _linqHelper.ForEach(readOnlyProcessesWithName, KillProcess);
   }

   public void KillProcess(Process process)
   {
      string processName = _processHelper.GetProcessName(process);
      _consoleWriter.WriteProgramNameTimestampedLine($"Killing process {processName}");
      _processHelper.KillProcess(process);
      _consoleWriter.WriteProgramNameTimestampedLine($"Killed process {processName}");
   }
}
