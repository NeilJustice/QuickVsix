param([bool]$doInstall = $True)

BuildAnalyzeTestDeploy.exe csharp-program `
   --solution-name=QuickVsix `
   --configuration=Release `
   --tests-project-name=QuickVsixTests `
   --ilrepack-DLLs="DocoptPlus.dll nunit.framework.dll" `
   --install-folder="C:\bin" `
   --do-install=$doInstall
exit $LastExitCode
