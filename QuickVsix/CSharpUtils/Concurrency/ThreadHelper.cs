using System.Diagnostics.CodeAnalysis;

namespace CSharpUtils
{
   [ExcludeFromCodeCoverage]
   public class ThreadHelper
   {
      public virtual int CurrentThreadId()
      {
         return Thread.CurrentThread.ManagedThreadId;
      }

      public virtual void SleepMilliseconds(int millisecondsTimeout)
      {
         Thread.Sleep(millisecondsTimeout);
      }
   }
}
