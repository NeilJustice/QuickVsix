namespace CSharpUtils
{
   public class OneArgTryCatchCaller
   {
      private readonly Environmentalist _environmentalist = new Environmentalist();

      public virtual ReturnType FailFastTryCatchCall<ArgumentType, ReturnType>(
         Func<ArgumentType, ReturnType> function, ArgumentType functionArgument, Func<Exception, ArgumentType, int> exceptionHandler)
      {
         try
         {
            ReturnType returnValue = function(functionArgument);
            return returnValue;
         }
         catch (Exception ex)
         {
            int exitCode = exceptionHandler(ex, functionArgument);
            _environmentalist.Exit(exitCode);
         }
         return default;
      }

      public virtual ReturnType TryCatchCall<ArgumentType, ReturnType>(
         Func<ArgumentType, ReturnType> function, ArgumentType functionArgument, Func<Exception, ArgumentType, ReturnType> exceptionHandler)
      {
         try
         {
            ReturnType functionReturnValue = function(functionArgument);
            return functionReturnValue;
         }
         catch (Exception ex)
         {
            ReturnType exceptionHandlerReturnValue = exceptionHandler(ex, functionArgument);
            return exceptionHandlerReturnValue;
         }
      }
   }
}
