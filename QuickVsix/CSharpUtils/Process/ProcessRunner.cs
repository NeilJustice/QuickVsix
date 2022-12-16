using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace CSharpUtils
{
   [ExcludeFromCodeCoverage]
   public class ProcessRunner
   {
      private readonly Watch _watch = new Watch();
      private ConsoleWriter _consoleWriter;
      private string _programName;

      public ProcessRunner(string programName)
      {
         _consoleWriter = new ConsoleWriter(programName);
         _programName = programName;
      }

      public virtual ProcessResult Run(string fileName, string arguments)
      {
         using (var process = new Process())
         using (var doneReceivingOutputData = new ManualResetEvent(false))
         using (var doneReceivingErrorData = new ManualResetEvent(false))
         {
            string workingDirectory = Directory.GetCurrentDirectory();
            process.StartInfo = new ProcessStartInfo
            {
               FileName = fileName,
               Arguments = arguments,
               RedirectStandardOutput = true,
               RedirectStandardError = true,
               UseShellExecute = false,
               WorkingDirectory = workingDirectory
            };
            string currentDirectoryPath = Directory.GetCurrentDirectory();
            string argumentsPart = string.IsNullOrEmpty(arguments) ? "" : " " + arguments;
            string dateTimeNow = _watch.DateTimeNowString();
            Console.WriteLine($"[{_programName} {dateTimeNow}] Running process \"{fileName}{argumentsPart}\" from folder {currentDirectoryPath}");
            bool didStartNewProcess = process.Start();
            if (!didStartNewProcess)
            {
               throw new InvalidOperationException($"process.Start() returned false. File name and arguments: {fileName} {arguments}");
            }
            var standardOutputBuilder = new StringBuilder();
            var standardErrorBuilder = new StringBuilder();
            process.OutputDataReceived += (object sender, DataReceivedEventArgs e) =>
            {
               if (e.Data == null)
               {
                  doneReceivingOutputData.Set();
               }
               else
               {
                  standardOutputBuilder.AppendLine(e.Data);
               }
            };
            process.ErrorDataReceived += (object sender, DataReceivedEventArgs e) =>
            {
               if (e.Data == null)
               {
                  doneReceivingErrorData.Set();
               }
               else
               {
                  standardErrorBuilder.AppendLine(e.Data);
               }
            };
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.WaitForExit();
            doneReceivingOutputData.WaitOne();
            doneReceivingErrorData.WaitOne();
            string standardOutput = standardOutputBuilder.ToString().RemoveCarriageReturns();
            string standardError = standardErrorBuilder.ToString().RemoveCarriageReturns();
            DateTime endTime = DateTime.Now;
            int exitCode = process.ExitCode;
            var processResult = new ProcessResult
            {
               processStartInfo = process.StartInfo,
               exitCode = exitCode,
               standardOutput = standardOutput,
               standardError = standardError == "\n" ? "" : standardError,
               startTime = process.StartTime,
               endTime = endTime
            };
            return processResult;
         }
      }

      public virtual int RunAndGetExitCode(string fileName, string arguments, bool printRunningMessage)
      {
         using (var process = new Process())
         {
            string currentDirectoryPath = Directory.GetCurrentDirectory();
            string argumentsPart = string.IsNullOrEmpty(arguments) ? "" : " " + arguments;
            var stopwatcher = new Stopwatcher();
            if (printRunningMessage)
            {
               stopwatcher.Start();
               string runningMessage = $"Running process \"{fileName}{argumentsPart}\" from folder {currentDirectoryPath}";
               _consoleWriter.WriteProgramNameTimestampedLine(runningMessage);
            }
            process.StartInfo = new ProcessStartInfo
            {
               FileName = fileName,
               Arguments = arguments,
               UseShellExecute = false,
               WorkingDirectory = Directory.GetCurrentDirectory()
            };
            bool didStartNewProcess = process.Start();
            if (!didStartNewProcess)
            {
               throw new InvalidOperationException($"process.Start() returned false. File name and arguments: {fileName} {arguments}");
            }
            process.WaitForExit();
            int exitCode = process.ExitCode;
            if (printRunningMessage)
            {
               string elapsedSeconds = stopwatcher.StopAndGetSeconds();
               string completedMessage = $"Completed: \"{fileName}{argumentsPart}\" with exit code {exitCode} [{elapsedSeconds} seconds]";
               _consoleWriter.WriteProgramNameTimestampedLine(completedMessage);
            }
            return exitCode;
         }
      }

      public virtual ProcessResult RunAndSubscribeToLineByLineStandardOutputAndStandardError<StateType>(
         string fileName,
         string arguments,
         Action<object, string, StateType> outputOrErrorDataReceived,
         StateType state)
      {
         using (var process = new Process())
         using (var doneReceivingOutputData = new ManualResetEvent(false))
         using (var doneReceivingErrorData = new ManualResetEvent(false))
         {
            var standardOutputBuilder = new StringBuilder();
            var standardErrorBuilder = new StringBuilder();
            process.StartInfo = new ProcessStartInfo
            {
               FileName = fileName,
               Arguments = arguments,
               RedirectStandardOutput = true,
               RedirectStandardError = true,
               UseShellExecute = false,
               WorkingDirectory = Directory.GetCurrentDirectory()
            };
            process.OutputDataReceived += (object sender, DataReceivedEventArgs e) =>
            {
               if (e.Data == null)
               {
                  doneReceivingOutputData.Set();
               }
               else
               {
                  standardOutputBuilder.AppendLine(e.Data);
                  outputOrErrorDataReceived(sender, e.Data, state);
               }
            };
            process.ErrorDataReceived += (object sender, DataReceivedEventArgs e) =>
            {
               if (e.Data == null)
               {
                  doneReceivingErrorData.Set();
               }
               else
               {
                  standardErrorBuilder.AppendLine(e.Data);
                  outputOrErrorDataReceived(sender, e.Data, state);
               }
            };
            bool didStartNewProcess = process.Start();
            if (!didStartNewProcess)
            {
               throw new InvalidOperationException($"process.Start() returned false. File name and arguments: {fileName} {arguments}");
            }
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            doneReceivingOutputData.WaitOne();
            doneReceivingErrorData.WaitOne();
            process.WaitForExit(int.MaxValue);
            DateTime endTime = DateTime.Now;
            string standardOutput = standardOutputBuilder.ToString();
            string standardError = standardErrorBuilder.ToString();
            var processResult = new ProcessResult
            {
               processStartInfo = process.StartInfo,
               exitCode = process.ExitCode,
               standardOutput = standardOutput,
               standardError = standardError,
               startTime = process.StartTime,
               endTime = endTime
            };
            return processResult;
         }
      }
   }
}
