namespace CSharpUtils
{
   public class MethodCaller
   {
      public virtual void CallAction(Action action)
      {
         action();
      }

      public virtual void CallAction<Arg1Type>(
         Action<Arg1Type> action, Arg1Type arg1)
      {
         action(arg1);
      }

      public virtual void CallAction<Arg1Type, Arg2Type>(
         Action<Arg1Type, Arg2Type> action, Arg1Type arg1, Arg2Type arg2)
      {
         action(arg1, arg2);
      }

      public virtual void CallAction<Arg1Type, Arg2Type, Arg3Type>(
         Action<Arg1Type, Arg2Type, Arg3Type> action, Arg1Type arg1, Arg2Type arg2, Arg3Type arg3)
      {
         action(arg1, arg2, arg3);
      }

      public virtual void CallAction<Arg1Type, Arg2Type, Arg3Type, Arg4Type>(
         Action<Arg1Type, Arg2Type, Arg3Type, Arg4Type> action, Arg1Type arg1, Arg2Type arg2, Arg3Type arg3, Arg4Type arg4)
      {
         action(arg1, arg2, arg3, arg4);
      }

      public virtual void CallAction<Arg1Type, Arg2Type, Arg3Type, Arg4Type, Arg5Type>(
         Action<Arg1Type, Arg2Type, Arg3Type, Arg4Type, Arg5Type> action, Arg1Type arg1, Arg2Type arg2, Arg3Type arg3, Arg4Type arg4, Arg5Type arg5)
      {
         action(arg1, arg2, arg3, arg4, arg5);
      }

      public virtual void CallAction<Arg1Type, Arg2Type, Arg3Type, Arg4Type, Arg5Type, Arg6Type>(
         Action<Arg1Type, Arg2Type, Arg3Type, Arg4Type, Arg5Type, Arg6Type> action, Arg1Type arg1, Arg2Type arg2, Arg3Type arg3, Arg4Type arg4, Arg5Type arg5, Arg6Type arg6)
      {
         action(arg1, arg2, arg3, arg4, arg5, arg6);
      }

      public virtual void CallAction<Arg1Type, Arg2Type, Arg3Type, Arg4Type, Arg5Type, Arg6Type, Arg7Type>(
         Action<Arg1Type, Arg2Type, Arg3Type, Arg4Type, Arg5Type, Arg6Type, Arg7Type> action, Arg1Type arg1, Arg2Type arg2, Arg3Type arg3, Arg4Type arg4, Arg5Type arg5, Arg6Type arg6, Arg7Type arg7)
      {
         action(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
      }

      public virtual void CallAction<Arg1Type, Arg2Type, Arg3Type, Arg4Type, Arg5Type, Arg6Type, Arg7Type, Arg8Type>(
         Action<Arg1Type, Arg2Type, Arg3Type, Arg4Type, Arg5Type, Arg6Type, Arg7Type, Arg8Type> action, Arg1Type arg1, Arg2Type arg2, Arg3Type arg3, Arg4Type arg4, Arg5Type arg5, Arg6Type arg6, Arg7Type arg7, Arg8Type arg8)
      {
         action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
      }

      public virtual void CallAction<Arg1Type, Arg2Type, Arg3Type, Arg4Type, Arg5Type, Arg6Type, Arg7Type, Arg8Type, Arg9Type>(
         Action<Arg1Type, Arg2Type, Arg3Type, Arg4Type, Arg5Type, Arg6Type, Arg7Type, Arg8Type, Arg9Type> action, Arg1Type arg1, Arg2Type arg2, Arg3Type arg3, Arg4Type arg4, Arg5Type arg5, Arg6Type arg6, Arg7Type arg7, Arg8Type arg8, Arg9Type arg9)
      {
         action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
      }

      public virtual void CallAction<Arg1Type, Arg2Type, Arg3Type, Arg4Type, Arg5Type, Arg6Type, Arg7Type, Arg8Type, Arg9Type, Arg10Type>(
         Action<Arg1Type, Arg2Type, Arg3Type, Arg4Type, Arg5Type, Arg6Type, Arg7Type, Arg8Type, Arg9Type, Arg10Type> action, Arg1Type arg1, Arg2Type arg2, Arg3Type arg3, Arg4Type arg4, Arg5Type arg5, Arg6Type arg6, Arg7Type arg7, Arg8Type arg8, Arg9Type arg9, Arg10Type arg10)
      {
         action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
      }

      public virtual void CallAction<Arg1Type, Arg2Type, Arg3Type, Arg4Type, Arg5Type, Arg6Type, Arg7Type, Arg8Type, Arg9Type, Arg10Type, Arg11Type>(
         Action<Arg1Type, Arg2Type, Arg3Type, Arg4Type, Arg5Type, Arg6Type, Arg7Type, Arg8Type, Arg9Type, Arg10Type, Arg11Type> action, Arg1Type arg1, Arg2Type arg2, Arg3Type arg3, Arg4Type arg4, Arg5Type arg5, Arg6Type arg6, Arg7Type arg7, Arg8Type arg8, Arg9Type arg9, Arg10Type arg10, Arg11Type arg11)
      {
         action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
      }

      public virtual void CallAction<Arg1Type, Arg2Type, Arg3Type, Arg4Type, Arg5Type, Arg6Type, Arg7Type, Arg8Type, Arg9Type, Arg10Type, Arg11Type, Arg12Type>(
         Action<Arg1Type, Arg2Type, Arg3Type, Arg4Type, Arg5Type, Arg6Type, Arg7Type, Arg8Type, Arg9Type, Arg10Type, Arg11Type, Arg12Type> action, Arg1Type arg1, Arg2Type arg2, Arg3Type arg3, Arg4Type arg4, Arg5Type arg5, Arg6Type arg6, Arg7Type arg7, Arg8Type arg8, Arg9Type arg9, Arg10Type arg10, Arg11Type arg11, Arg12Type arg12)
      {
         action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);
      }


      public virtual ReturnType CallFunction<ReturnType>(
         Func<ReturnType> function)
      {
         ReturnType returnValue = function();
         return returnValue;
      }

      public virtual ReturnType CallFunction<Arg1Type, ReturnType>(
         Func<Arg1Type, ReturnType> function, Arg1Type arg1)
      {
         ReturnType returnValue = function(arg1);
         return returnValue;
      }

      public virtual ReturnType CallFunction<Arg1Type, Arg2Type, ReturnType>(
         Func<Arg1Type, Arg2Type, ReturnType> function, Arg1Type arg1, Arg2Type arg2)
      {
         ReturnType returnValue = function(arg1, arg2);
         return returnValue;
      }

      public virtual ReturnType CallFunction<Arg1Type, Arg2Type, Arg3Type, ReturnType>(
         Func<Arg1Type, Arg2Type, Arg3Type, ReturnType> function, Arg1Type arg1, Arg2Type arg2, Arg3Type arg3)
      {
         ReturnType returnValue = function(arg1, arg2, arg3);
         return returnValue;
      }

      public virtual ReturnType CallFunction<Arg1Type, Arg2Type, Arg3Type, Arg4Type, ReturnType>(
         Func<Arg1Type, Arg2Type, Arg3Type, Arg4Type, ReturnType> function, Arg1Type arg1, Arg2Type arg2, Arg3Type arg3, Arg4Type arg4)
      {
         ReturnType returnValue = function(arg1, arg2, arg3, arg4);
         return returnValue;
      }

      public virtual ReturnType CallFunction<Arg1Type, Arg2Type, Arg3Type, Arg4Type, Arg5Type, ReturnType>(
         Func<Arg1Type, Arg2Type, Arg3Type, Arg4Type, Arg5Type, ReturnType> function, Arg1Type arg1, Arg2Type arg2, Arg3Type arg3, Arg4Type arg4, Arg5Type arg5)
      {
         ReturnType returnValue = function(arg1, arg2, arg3, arg4, arg5);
         return returnValue;
      }

      public virtual ReturnType CallFunction<Arg1Type, Arg2Type, Arg3Type, Arg4Type, Arg5Type, Arg6Type, ReturnType>(
         Func<Arg1Type, Arg2Type, Arg3Type, Arg4Type, Arg5Type, Arg6Type, ReturnType> function, Arg1Type arg1, Arg2Type arg2, Arg3Type arg3, Arg4Type arg4, Arg5Type arg5, Arg6Type arg6)
      {
         ReturnType returnValue = function(arg1, arg2, arg3, arg4, arg5, arg6);
         return returnValue;
      }

      public virtual ReturnType CallFunction<Arg1Type, Arg2Type, Arg3Type, Arg4Type, Arg5Type, Arg6Type, Arg7Type, ReturnType>(
         Func<Arg1Type, Arg2Type, Arg3Type, Arg4Type, Arg5Type, Arg6Type, Arg7Type, ReturnType> function, Arg1Type arg1, Arg2Type arg2, Arg3Type arg3, Arg4Type arg4, Arg5Type arg5, Arg6Type arg6, Arg7Type arg7)
      {
         ReturnType returnValue = function(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
         return returnValue;
      }

      public virtual ReturnType CallFunction<Arg1Type, Arg2Type, Arg3Type, Arg4Type, Arg5Type, Arg6Type, Arg7Type, Arg8Type, ReturnType>(
         Func<Arg1Type, Arg2Type, Arg3Type, Arg4Type, Arg5Type, Arg6Type, Arg7Type, Arg8Type, ReturnType> function, Arg1Type arg1, Arg2Type arg2, Arg3Type arg3, Arg4Type arg4, Arg5Type arg5, Arg6Type arg6, Arg7Type arg7, Arg8Type arg8)
      {
         ReturnType returnValue = function(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
         return returnValue;
      }

      public virtual ReturnType CallFunction<Arg1Type, Arg2Type, Arg3Type, Arg4Type, Arg5Type, Arg6Type, Arg7Type, Arg8Type, Arg9Type, ReturnType>(
         Func<Arg1Type, Arg2Type, Arg3Type, Arg4Type, Arg5Type, Arg6Type, Arg7Type, Arg8Type, Arg9Type, ReturnType> function, Arg1Type arg1, Arg2Type arg2, Arg3Type arg3, Arg4Type arg4, Arg5Type arg5, Arg6Type arg6, Arg7Type arg7, Arg8Type arg8, Arg9Type arg9)
      {
         ReturnType returnValue = function(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
         return returnValue;
      }

      public virtual ReturnType CallFunction<Arg1Type, Arg2Type, Arg3Type, Arg4Type, Arg5Type, Arg6Type, Arg7Type, Arg8Type, Arg9Type, Arg10Type, ReturnType>(
         Func<Arg1Type, Arg2Type, Arg3Type, Arg4Type, Arg5Type, Arg6Type, Arg7Type, Arg8Type, Arg9Type, Arg10Type, ReturnType> function, Arg1Type arg1, Arg2Type arg2, Arg3Type arg3, Arg4Type arg4, Arg5Type arg5, Arg6Type arg6, Arg7Type arg7, Arg8Type arg8, Arg9Type arg9, Arg10Type arg10)
      {
         ReturnType returnValue = function(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
         return returnValue;
      }

      public virtual ReturnType CallFunction<Arg1Type, Arg2Type, Arg3Type, Arg4Type, Arg5Type, Arg6Type, Arg7Type, Arg8Type, Arg9Type, Arg10Type, Arg11Type, ReturnType>(
         Func<Arg1Type, Arg2Type, Arg3Type, Arg4Type, Arg5Type, Arg6Type, Arg7Type, Arg8Type, Arg9Type, Arg10Type, Arg11Type, ReturnType> function, Arg1Type arg1, Arg2Type arg2, Arg3Type arg3, Arg4Type arg4, Arg5Type arg5, Arg6Type arg6, Arg7Type arg7, Arg8Type arg8, Arg9Type arg9, Arg10Type arg10, Arg11Type arg11)
      {
         ReturnType returnValue = function(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
         return returnValue;
      }

      public virtual ReturnType CallFunction<Arg1Type, Arg2Type, Arg3Type, Arg4Type, Arg5Type, Arg6Type, Arg7Type, Arg8Type, Arg9Type, Arg10Type, Arg11Type, Arg12Type, ReturnType>(
         Func<Arg1Type, Arg2Type, Arg3Type, Arg4Type, Arg5Type, Arg6Type, Arg7Type, Arg8Type, Arg9Type, Arg10Type, Arg11Type, Arg12Type, ReturnType> function, Arg1Type arg1, Arg2Type arg2, Arg3Type arg3, Arg4Type arg4, Arg5Type arg5, Arg6Type arg6, Arg7Type arg7, Arg8Type arg8, Arg9Type arg9, Arg10Type arg10, Arg11Type arg11, Arg12Type arg12)
      {
         ReturnType returnValue = function(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);
         return returnValue;
      }
   }
}
