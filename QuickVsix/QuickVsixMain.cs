using System.Diagnostics.CodeAnalysis;

public static class QuickVsixMain
{
   // install-vsix --vsix-file=C:\Code\OneStrokeStudio\OneStrokeStudio\bin\Debug\OneStrokeStudio.vsix
   // uninstall-vsix --vsix-file=C:\Code\OneStrokeStudio\OneStrokeStudio\bin\Debug\OneStrokeStudio.vsix
   // reinstall-vsix --vsix-file=C:\Code\OneStrokeStudio\OneStrokeStudio\bin\Debug\OneStrokeStudio.vsix --wait-for-any-key

   [ExcludeFromCodeCoverage]
   public static int Main(string[] stringArgs)
   {
      var quickVsixProgram = new QuickVsixProgram();
      int exitCode = quickVsixProgram.Main(stringArgs);
      return exitCode;
   }
}
