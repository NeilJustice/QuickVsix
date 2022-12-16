using CSharpUtils;
using NUnit.Framework;

[TestFixture]
public class ReinstallVsixSubProgramTests
{
   ReinstallVsixSubProgram _reinstallVsixSubProgram;
   UninstallVsixSubProgram _uninstallVsixSubProgramMock;
   InstallVsixSubProgram _installVsixSubProgramMock;

   [SetUp]
   public void SetUp()
   {
      _reinstallVsixSubProgram = new ReinstallVsixSubProgram();
      _uninstallVsixSubProgramMock = Mock.Component<UninstallVsixSubProgram>(_reinstallVsixSubProgram, "_uninstallVsixSubProgram");
      _installVsixSubProgramMock = Mock.Component<InstallVsixSubProgram>(_reinstallVsixSubProgram, "_installVsixSubProgram");
   }

   [Test]
   public void Run_RunsUninstallSubProgramWhichReturnsNon0_ReturnsUninstallSubProgramExitCode()
   {
      int uninstallExitCode = TestRandom.Non0Int();
      Mock.Return(() => _uninstallVsixSubProgramMock.Run(null), uninstallExitCode);
      var args = QuickVsixTestRandom.Args();
      //
      int exitCode = _reinstallVsixSubProgram.Run(args);
      //
      Called.Once(() => _uninstallVsixSubProgramMock.Run(args));
      Assert.AreEqual(uninstallExitCode, exitCode);
   }

   [Test]
   public void Run_RunsUninstallSubProgramWhichReturns0_RunsInstallSubProgram_ReturnsInstallExitCode()
   {
      Mock.Return(() => _uninstallVsixSubProgramMock.Run(null), 0);

      int installExitCode = TestRandom.Int();
      Mock.Return(() => _installVsixSubProgramMock.Run(null), installExitCode);

      QuickVsixArgs args = QuickVsixTestRandom.Args();
      //
      int exitCode = _reinstallVsixSubProgram.Run(args);
      //
      Called.Once(() => _uninstallVsixSubProgramMock.Run(args)).Then(
      Called.Once(() => _installVsixSubProgramMock.Run(args)));
      Assert.AreEqual(installExitCode, exitCode);
   }
}
