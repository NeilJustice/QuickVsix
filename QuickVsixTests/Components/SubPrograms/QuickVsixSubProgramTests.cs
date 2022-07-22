using CSharpUtils;
using NUnit.Framework;

[TestFixture]
public static class QuickVsixSubProgramTests
{
   [Test]
   public static void DefaultConstructor_NewsProtectedConsoleWriter()
   {
      var quickVsixSubProgram = new QuickVsixSubProgram();
      Assert2.AssertConsoleWriterHasProgramName("QuickVsix", quickVsixSubProgram, "p_consoleWriter");
      Assert2.AssertProcessRunnerHasProgramName("QuickVsix", quickVsixSubProgram, "p_processRunner");
      Assert2.FieldIsNonNullAndExactType<ProcessKiller>(quickVsixSubProgram, "p_processKiller");
      Assert2.FieldIsNonNullAndExactType<QuickVsixLogFilePathPrinter>(quickVsixSubProgram, "p_quickVsixLogFilePathPrinter");
      Assert2.FieldIsNonNullAndExactType<VsixZipFileReader>(quickVsixSubProgram, "p_vsixZipFileReader");
   }

   [Test]
   public static void Run_Returns0()
   {
      var quickVsixSubProgram = new QuickVsixSubProgram();
      var args = QuickVsixTestRandom.Args();
      //
      int exitCode = quickVsixSubProgram.Run(args);
      //
      Assert.Zero(exitCode);
   }
}
