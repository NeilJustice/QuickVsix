param($doInstall = "true")

WindowsCSharpBuilder.exe build-csharp-solution `
   --solution=QuickVsix `
   --configuration=Release `
   --install=$doInstall
exit $LastExitCode
