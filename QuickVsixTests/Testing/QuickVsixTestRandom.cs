using CSharpUtils;

public static class QuickVsixTestRandom
{
   public static QuickVsixArgs Args()
   {
      return TestableArgs(RandomGenerator.Instance);
   }

   public static QuickVsixArgs TestableArgs(RandomGenerator randomGenerator)
   {
      var randomArgs = new QuickVsixArgs();
      randomArgs.CommandLine = randomGenerator.String();
      randomArgs.programMode = randomGenerator.Enum<ProgramMode>();
      randomArgs.vsixFilePath = randomGenerator.String();
      randomArgs.waitForAnyKey = randomGenerator.Bool();
      return randomArgs;
   }
}
