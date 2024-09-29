using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class ProcessHelper
{
   public virtual Process[] GetProcessesByName(string processName)
   {
      Process[] processes = Process.GetProcessesByName(processName);
      return processes;
   }

   public virtual string GetProcessName(Process process)
   {
      string processName = process.ProcessName;
      return processName;
   }

   public virtual void KillProcess(Process process)
   {
      process.Kill();
   }
}
