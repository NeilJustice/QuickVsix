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
   ProcessHelper _processHelperMock;
   ReadOnlyCollectionMaker _readOnlyCollectionMakerMock;

   [SetUp]
   public void SetUp()
   {
      _processKiller = new ProcessKiller();
      Assert2.AssertConsoleWriterHasProgramName("QuickVsix", _processKiller, "_consoleWriter");
      _consoleWriterMock = Mock.Component<ConsoleWriter>(_processKiller, "_consoleWriter");
      _linqHelperMock = Mock.Component<LinqHelper>(_processKiller, "_linqHelper");
      _processHelperMock = Mock.Component<ProcessHelper>(_processKiller, "_processHelper");
      _readOnlyCollectionMakerMock = Mock.Component<ReadOnlyCollectionMaker>(_processKiller, "_readOnlyCollectionMaker");
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

      string processName = TestRandom.String();
      //
      _processKiller.KillProcess(processName);
      //
      Called.NumberOfTimes(3, () => _consoleWriterMock.WriteProgramNameTimestampedLine(null));
      Called.Once(() => _processHelperMock.GetProcessesByName(processName)).Then(
      Called.Once(() => _readOnlyCollectionMakerMock.MakeReadOnlyCollection(mutableProcessesWithName))).Then(
      Called.WasCalled(() => _consoleWriterMock.WriteProgramNameTimestampedLine("Killing all mspdbsrv.exe processes"))).Then(
      Called.Once(() => _linqHelperMock.ForEach(readOnlyProcessesWithName, _processKiller.DoKillProcess))).Then(
      Called.WasCalled(() => _consoleWriterMock.WriteProgramNameTimestampedLine($"Killed {readOnlyProcessesWithName.Count} mspdbsrv.exe processes"))).Then(
      Called.WasCalled(() => _consoleWriterMock.WriteProgramNameTimestampedLine("")));
   }

   [Test]
   public void DoKillProcess_WritesKillingProcessMessage_KillsProcess_WritesKilledProcessMessage()
   {
      string processName = Mock.ReturnRandomString(() => _processHelperMock.GetProcessName(null));

      Mock.Expect(() => _consoleWriterMock.WriteProgramNameTimestampedLine(null));
      Mock.Expect(() => _processHelperMock.KillProcess(null));

      var process = new Process();
      //
      _processKiller.DoKillProcess(process);
      //
      Called.Once(() => _processHelperMock.GetProcessName(process)).Then(
      Called.WasCalled(() => _consoleWriterMock.WriteProgramNameTimestampedLine($"Killing process {processName}"))).Then(
      Called.Once(() => _processHelperMock.KillProcess(process))).Then(
      Called.WasCalled(() => _consoleWriterMock.WriteProgramNameTimestampedLine($"Killed process {processName}")));
   }
}
