using System.Diagnostics.CodeAnalysis;
using System.Security.Principal;

namespace CSharpUtils
{
   public class Environmentalist
   {
      private readonly MethodCaller _methodCaller = new MethodCaller();

      [ExcludeFromCodeCoverage]
      public virtual string CurrentDirectoryPath()
      {
         string currentDirectoryPath = Environment.CurrentDirectory;
         return currentDirectoryPath;
      }

      public virtual string GetUserEnvironmentVariable(string environmentVariableName)
      {
         string userEnvironmentVariable = _methodCaller.CallFunction(
            Environment.GetEnvironmentVariable, environmentVariableName, EnvironmentVariableTarget.User);
         if (userEnvironmentVariable == null)
         {
            throw new ArgumentException($"User environment variable {environmentVariableName} is not defined");
         }
         return userEnvironmentVariable;
      }

      public virtual void Exit(int exitCode)
      {
         _methodCaller.CallAction(Environment.Exit, exitCode);
      }

      [ExcludeFromCodeCoverage]
      public virtual string MachineName()
      {
         string machineName = Environment.MachineName;
         return machineName;
      }

      [ExcludeFromCodeCoverage]
      public virtual string UserName()
      {
         string userName = Environment.UserName;
         return userName;
      }

      [ExcludeFromCodeCoverage]
      public virtual void ThrowIfNotRunningAsAdministrator(string exceptionMessageIfNotAdministrator)
      {
         WindowsIdentity windowsIdentity = WindowsIdentity.GetCurrent();
         WindowsPrincipal windowsPrincipal = new WindowsPrincipal(windowsIdentity);
         bool areRunningAsAdministrator = windowsPrincipal.IsInRole(WindowsBuiltInRole.Administrator);
         if (!areRunningAsAdministrator)
         {
            throw new InvalidOperationException(exceptionMessageIfNotAdministrator);
         }
      }
   }
}
