using System;

namespace CSharpUtils
{
   public static class ArrayExtensions
   {
      // Identical to Array.ForEach but with single-step debuggability of each iteration
      public static void ForEach<T>(this T[] elements, Action<T> action)
      {
         foreach (T element in elements)
         {
            action(element);
         }
      }
   }
}
