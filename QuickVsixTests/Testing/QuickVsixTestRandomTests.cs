using CSharpUtils;
using NUnit.Framework;

[TestFixture]
public static class QuickVsixTestRandomTests
{
   [Test]
   public static void TestableArgs_ReturnsRandomArgs()
   {
      var randomGeneratorMock = Mock.Strict<RandomGenerator>();

      string commandLine = TestRandom.String();
      string vsixFilePath = TestRandom.String();
      Mock.ReturnValues(() => randomGeneratorMock.String(),
         commandLine,
         vsixFilePath);

      bool waitForAnyKey = Mock.ReturnRandomBool(() => randomGeneratorMock.Bool());

      ProgramMode programMode = Mock.ReturnRandomEnum(() => randomGeneratorMock.Enum<ProgramMode>());
      //
      var randomArgs = QuickVsixTestRandom.TestableArgs(randomGeneratorMock);
      //
      var expectedRandomArgs = new QuickVsixArgs
      {
         CommandLine = commandLine,
         programMode = programMode,
         vsixFilePath = vsixFilePath,
         waitForAnyKey = waitForAnyKey
      };
      Assert.AreEqual(expectedRandomArgs, randomArgs);
   }
}
