param($doInstall = "true")

WindowsCSharpBuilder.exe build-csharp-program `
   --solution-name=QuickVsix `
   --configuration=Release `
   --install=$doInstall `
   --install-with-release-folder-name-suffix
exit $LastExitCode
