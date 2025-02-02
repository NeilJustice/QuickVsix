param($isRunByGitPrecommit = "false", $doInstall = "true")

WindowsCSharpBuilder.exe build-csharp-program `
   --solution-name=QuickVsix `
   --configuration=Release `
   --tests-project-name=QuickVsixTests `
   --install-folder="C:\bin\QuickVsix" `
   --do-always-run-tests=$isRunByGitPrecommit `
   --do-install=$doInstall
exit $LastExitCode
