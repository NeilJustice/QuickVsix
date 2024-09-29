using System;

namespace CSharpUtils
{
   public static class Repeatedly
   {
      public static void Call(int numberOfCalls, Action action)
      {
         for (int i = 0; i < numberOfCalls; ++i)
         {
            action();
         }
      }
   }
}
