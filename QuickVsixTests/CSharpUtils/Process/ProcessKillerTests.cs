using CSharpUtils;
using NUnit.Framework;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;

[TestFixture]
public class ProcessKillerTests
{
   ProcessKiller _processKiller;
   ConsoleWriter _consoleWriterMock;
   LinqHelper _linqHelperMock;
   MethodCaller _methodCallerMock;
   Pluralizer _pluralizerMock;
   ProcessHelper _processHelperMock;
   ReadOnlyCollectionMaker _readOnlyCollectionMakerMock;

   [SetUp]
   public void SetUp()
   {
      _processKiller = new ProcessKiller();
      Assert2.AssertConsoleWriterHasProgramName("QuickVsix", _processKiller, "_consoleWriter");
      _consoleWriterMock = Mock.Component<ConsoleWriter>(_processKiller, "_consoleWriter");
      _linqHelperMock = Mock.Component<LinqHelper>(_processKiller, "_linqHelper");
      _methodCallerMock = Mock.Component<MethodCaller>(_processKiller, "_methodCaller");
      _pluralizerMock = Mock.Component<Pluralizer>(_processKiller, "_pluralizer");
      _processHelperMock = Mock.Component<ProcessHelper>(_processKiller, "_processHelper");
      _readOnlyCollectionMakerMock = Mock.Component<ReadOnlyCollectionMaker>(_processKiller, "_readOnlyCollectionMaker");
   }

   [Test]
   public void KillAllPotentiallyBlockingProcesses_KillsAllPotentiallyBlockingProcesses()
   {
      Mock.Expect(() => _consoleWriterMock.WriteProgramNameTimestampedLine(null));
      Mock.Expect(() => _methodCallerMock.CallAction(default(Action<string>), null));
      //
      _processKiller.KillAllPotentiallyBlockingProcesses();
      //
      Called.NumberOfTimes(16, () => _methodCallerMock.CallAction(default(Action<string>), null));
      Called.NumberOfTimes(2, () => _consoleWriterMock.WriteProgramNameTimestampedLine(null));
      Called.WasCalled(() => _consoleWriterMock.WriteProgramNameTimestampedLine("Killing all processes that can block VSIXInstaller.exe"));
      Called.WasCalled(() => _consoleWriterMock.WriteProgramNameTimestampedLine(""));
      Called.WasCalled(() => _methodCallerMock.CallAction(_processKiller.KillProcess, "cl")).Then(
      Called.WasCalled(() => _methodCallerMock.CallAction(_processKiller.KillProcess, "Microsoft.ServiceHub.Controller"))).Then(
      Called.WasCalled(() => _methodCallerMock.CallAction(_processKiller.KillProcess, "MSBuild"))).Then(
      Called.WasCalled(() => _methodCallerMock.CallAction(_processKiller.KillProcess, "mspdbsrv"))).Then(
      Called.WasCalled(() => _methodCallerMock.CallAction(_processKiller.KillProcess, "node"))).Then(
      Called.WasCalled(() => _methodCallerMock.CallAction(_processKiller.KillProcess, "ServiceHub.Host.dotnet.x64"))).Then(
      Called.WasCalled(() => _methodCallerMock.CallAction(_processKiller.KillProcess, "ServiceHub.Host.netfx.x86"))).Then(
      Called.WasCalled(() => _methodCallerMock.CallAction(_processKiller.KillProcess, "ServiceHub.IdentityHost"))).Then(
      Called.WasCalled(() => _methodCallerMock.CallAction(_processKiller.KillProcess, "ServiceHub.IndexingService"))).Then(
      Called.WasCalled(() => _methodCallerMock.CallAction(_processKiller.KillProcess, "ServiceHub.IntellicodeModelService"))).Then(
      Called.WasCalled(() => _methodCallerMock.CallAction(_processKiller.KillProcess, "ServiceHub.RoslynCodeAnalysisService"))).Then(
      Called.WasCalled(() => _methodCallerMock.CallAction(_processKiller.KillProcess, "ServiceHub.SettingsHost"))).Then(
      Called.WasCalled(() => _methodCallerMock.CallAction(_processKiller.KillProcess, "ServiceHub.ThreadedWaitDialog"))).Then(
      Called.WasCalled(() => _methodCallerMock.CallAction(_processKiller.KillProcess, "ServiceHub.VSDetouredHost"))).Then(
      Called.WasCalled(() => _methodCallerMock.CallAction(_processKiller.KillProcess, "vctip"))).Then(
      Called.WasCalled(() => _methodCallerMock.CallAction(_processKiller.KillProcess, "vstest.console")));
   }

   [Test]
   public void KillProcess_GetsProcessWithName_KillsEachProcess()
   {
      Process[] mutableProcessesWithName = TestRandom.Array<Process>();
      Mock.Return(() => _processHelperMock.GetProcessesByName(null), mutableProcessesWithName);

      ReadOnlyCollection<Process> readOnlyProcessesWithName = TestRandom.Array<Process>().ToReadOnlyCollection();
      Mock.Return(() => _readOnlyCollectionMakerMock.MakeReadOnlyCollection(default(Process[])), readOnlyProcessesWithName);

      Mock.Expect(() => _consoleWriterMock.WriteProgramNameTimestampedLine(null));

      Mock.Expect(() => _linqHelperMock.ForEach(default(ReadOnlyCollection<Process>), default(Action<Process>)));

      string processOrProcesses = Mock.ReturnRandomString(() => _pluralizerMock.PluralizeIfNot1(0, null, null));

      string processNameWithoutExeExtension = TestRandom.String();
      //
      _processKiller.KillProcess(processNameWithoutExeExtension);
      //
      Called.NumberOfTimes(3, () => _consoleWriterMock.WriteProgramNameTimestampedLine(null));
      Called.Once(() => _processHelperMock.GetProcessesByName(processNameWithoutExeExtension)).Then(
      Called.Once(() => _readOnlyCollectionMakerMock.MakeReadOnlyCollection(mutableProcessesWithName))).Then(
      Called.WasCalled(() => _consoleWriterMock.WriteProgramNameTimestampedLine(
         $"Killing all {processNameWithoutExeExtension}.exe processes which delay installs and uninstalls of Visual Studio extensions"))).Then(
      Called.Once(() => _linqHelperMock.ForEach(readOnlyProcessesWithName, _processKiller.DoKillProcess))).Then(
      Called.Once(() => _pluralizerMock.PluralizeIfNot1(readOnlyProcessesWithName.Count, "process", "processes"))).Then(
      Called.WasCalled(() => _consoleWriterMock.WriteProgramNameTimestampedLine(
         $"Killed {readOnlyProcessesWithName.Count} {processNameWithoutExeExtension}.exe {processOrProcesses}"))).Then(
      Called.WasCalled(() => _consoleWriterMock.WriteProgramNameTimestampedLine("")));
   }

   [Test]
   public void DoKillProcess_WritesKillingProcessMessage_KillsProcess_WritesKilledProcessMessage()
   {
      string processName = Mock.ReturnRandomString(() => _processHelperMock.GetProcessName(null));
      Mock.Expect(() => _processHelperMock.KillProcess(null));
      var process = new Process();
      //
      _processKiller.DoKillProcess(process);
      //
      Called.Once(() => _processHelperMock.GetProcessName(process)).Then(
      Called.Once(() => _processHelperMock.KillProcess(process)));
   }
}
