param($doInstall = "true")

WindowsCSharpBuilder.exe build-csharp-solution `
   --solution-name=QuickVsix `
   --configuration=Release `
   --install=$doInstall
exit $LastExitCode
