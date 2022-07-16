using System;

namespace CSharpUtils
{
   public class TwoArgTryCatchCaller
   {
      public virtual void TryCatchCallVoidMethod<Arg1Type, Arg2Type>(
         Action<Arg1Type, Arg2Type> action, Arg1Type arg1, Arg2Type arg2,
         Action<Exception, Arg1Type, Arg2Type> exceptionHandler)
      {
         try
         {
            action(arg1, arg2);
         }
         catch (Exception ex)
         {
            exceptionHandler(ex, arg1, arg2);
         }
      }
   }
}
