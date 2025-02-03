param($doInstall = "true")

WindowsCSharpBuilder.exe build-csharp-program `
   --solution-name=QuickVsix `
   --configuration=Release `
   --install=$doInstall
exit $LastExitCode
