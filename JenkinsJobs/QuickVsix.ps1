param($isRunByGitPrecommit = "false", $doInstall = "true")

BuildAnalyzeTestDeploy.exe build-dotnet-csharp-program `
   --solution-name=QuickVsix `
   --configuration=Release `
   --tests-project-name=QuickVsixTests `
   --install-folder="C:\bin\QuickVsix" `
   --do-always-run-tests=$isRunByGitPrecommit `
   --do-install=$doInstall
exit $LastExitCode
