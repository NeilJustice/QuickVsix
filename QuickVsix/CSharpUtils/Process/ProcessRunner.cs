using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace CSharpUtils
{
   [ExcludeFromCodeCoverage]
   public class ProcessRunner
   {
      private ConsoleWriter _consoleWriter;

      public ProcessRunner(string programName)
      {
         _consoleWriter = new ConsoleWriter(programName);
      }

      public string GetConsoleWriterProgramName()
      {
         string consoleWriterProgramName = Reflect.Get<string>(_consoleWriter, "_programName");
         return consoleWriterProgramName;
      }

      public virtual void RunAndDoNotWaitForProcessToExit(string fileName, string arguments)
      {
         using (var process = new Process())
         {
            process.StartInfo = new ProcessStartInfo
            {
               FileName = fileName,
               Arguments = arguments
            };
            bool didStartNewProcess = process.Start();
            if (!didStartNewProcess)
            {
               throw new InvalidOperationException($"process.Start() returned false. File name and arguments: {fileName} {arguments}");
            }
         }
      }

      public virtual ProcessResult RunWithStandardOutputNotPrinted(string fileName, string arguments)
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
            _consoleWriter.WriteProgramNameTimestampThreadIdLine($"Running '{fileName}{argumentsPart}' from folder {currentDirectoryPath}");
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
            var processResult = new ProcessResult
            {
               processStartInfo = process.StartInfo,
               exitCode = process.ExitCode,
               standardOutput = standardOutput,
               standardError = standardError == "\n" ? "" : standardError,
               startTime = process.StartTime,
               endTime = DateTime.Now
            };
            return processResult;
         }
      }

      public virtual void FailFastRunWithStandardOutputPrinted(string fileName, string arguments)
      {
         ProcessResult processResult = RunWithStandardOutputPrinted(fileName, arguments, true);
         if (processResult.exitCode != 0)
         {
            throw new InvalidOperationException($"Process '{fileName} {arguments}' exited with code {processResult.exitCode}");
         }
      }

      public virtual ProcessResult RunWithStandardOutputPrinted(string fileName, string arguments, bool printRunningMessage)
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
               RedirectStandardError = false,
               UseShellExecute = false,
               WorkingDirectory = Directory.GetCurrentDirectory()
            };
            if (printRunningMessage)
            {
               string currentDirectoryPath = Directory.GetCurrentDirectory();
               string argumentsPart = string.IsNullOrEmpty(arguments) ? "" : " " + arguments;
               _consoleWriter.WriteProgramNameTimestampedLine($"Running process '{fileName}{argumentsPart}' from folder {currentDirectoryPath}");
            }
            bool didStartNewProcess = process.Start();
            if (!didStartNewProcess)
            {
               throw new InvalidOperationException($"process.Start() returned false. File name and arguments: {fileName} {arguments}");
            }
            while (!process.StandardOutput.EndOfStream)
            {
               string standardOutputLine = process.StandardOutput.ReadLine();
               standardOutputBuilder.AppendLine(standardOutputLine);
               Console.WriteLine(standardOutputLine);
            }
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
