using CSharpUtils;
using System.Collections.ObjectModel;
using System.Diagnostics;

public class ProcessKiller
{
   private readonly ConsoleWriter _consoleWriter = new ConsoleWriter("QuickVsix");
   private readonly LinqHelper _linqHelper = new LinqHelper();
   private readonly MethodCaller _methodCaller = new MethodCaller();
   private readonly Pluralizer _pluralizer = new Pluralizer();
   private readonly ProcessHelper _processHelper = new ProcessHelper();
   private readonly ReadOnlyCollectionMaker _readOnlyCollectionMaker = new ReadOnlyCollectionMaker();

   public virtual void KillAllPotentiallyBlockingProcesses()
   {
      _consoleWriter.WriteProgramNameTimestampedLine("Killing all processes that can block VSIXInstaller.exe");
      _consoleWriter.WriteProgramNameTimestampedLine("");
      _methodCaller.CallAction(KillProcess, "cl");
      _methodCaller.CallAction(KillProcess, "Microsoft.ServiceHub.Controller");
      _methodCaller.CallAction(KillProcess, "MSBuild");
      _methodCaller.CallAction(KillProcess, "mspdbsrv");
      _methodCaller.CallAction(KillProcess, "node");
      _methodCaller.CallAction(KillProcess, "ServiceHub.Host.dotnet.x64");
      _methodCaller.CallAction(KillProcess, "ServiceHub.Host.netfx.x86");
      _methodCaller.CallAction(KillProcess, "ServiceHub.IdentityHost");
      _methodCaller.CallAction(KillProcess, "ServiceHub.IndexingService");
      _methodCaller.CallAction(KillProcess, "ServiceHub.IntellicodeModelService");
      _methodCaller.CallAction(KillProcess, "ServiceHub.RoslynCodeAnalysisService");
      _methodCaller.CallAction(KillProcess, "ServiceHub.SettingsHost");
      _methodCaller.CallAction(KillProcess, "ServiceHub.ThreadedWaitDialog");
      _methodCaller.CallAction(KillProcess, "ServiceHub.VSDetouredHost");
      _methodCaller.CallAction(KillProcess, "vctip");
      _methodCaller.CallAction(KillProcess, "vstest.console");
   }

   public virtual void KillProcess(string processNameWithoutExeExtension)
   {
      Process[] mutableProcessesWithName = _processHelper.GetProcessesByName(processNameWithoutExeExtension);
      ReadOnlyCollection<Process> readOnlyProcessesWithName = _readOnlyCollectionMaker.MakeReadOnlyCollection(mutableProcessesWithName);
      _consoleWriter.WriteProgramNameTimestampedLine(
         $"Killing all {processNameWithoutExeExtension}.exe processes which delay installs and uninstalls of Visual Studio extensions");
      _linqHelper.ForEach(readOnlyProcessesWithName, DoKillProcess);
      string processOrProcesses = _pluralizer.PluralizeIfNot1(readOnlyProcessesWithName.Count, "process", "processes");
      _consoleWriter.WriteProgramNameTimestampedLine(
         $"Killed {readOnlyProcessesWithName.Count} {processNameWithoutExeExtension}.exe {processOrProcesses}");
      _consoleWriter.WriteProgramNameTimestampedLine("");
   }

   public void DoKillProcess(Process process)
   {
      string processName = _processHelper.GetProcessName(process);
      _processHelper.KillProcess(process);
   }
}
