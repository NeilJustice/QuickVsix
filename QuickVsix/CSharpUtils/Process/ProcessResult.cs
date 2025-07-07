using System.Diagnostics;

namespace CSharpUtils
{
   [DebuggerDisplay("exitCode: {exitCode}")]
   public class ProcessResult : NUnitEquatable
   {
      public ProcessStartInfo processStartInfo;
      public int exitCode;
      public string standardOutput;
      public string standardError;
      public DateTime startTime;
      public DateTime endTime;
      public TimeSpan Duration { get { return endTime - startTime; } }

      public override bool Equals(object obj)
      {
         ProcessResult actual = (ProcessResult)obj;
         _nunitAsserter.AreEqual(processStartInfo.FileName, actual.processStartInfo.FileName);
         _nunitAsserter.AreEqual(processStartInfo.Arguments, actual.processStartInfo.Arguments);
         _nunitAsserter.AreEqual(exitCode, actual.exitCode);
         _nunitAsserter.AreEqual(standardOutput, actual.standardOutput);
         _nunitAsserter.AreEqual(standardError, actual.standardError);
         _nunitAsserter.AreEqual(startTime, actual.startTime);
         _nunitAsserter.AreEqual(endTime, actual.endTime);
         return true;
      }

      public override int GetHashCode()
      {
         throw new NotSupportedException();
      }
   }
}
