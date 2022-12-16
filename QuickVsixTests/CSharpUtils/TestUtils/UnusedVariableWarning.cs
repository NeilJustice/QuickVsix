namespace CSharpUtils
{
   public static class UnusedVariableWarning
   {
      public static void Suppress<T>(ref T variable)
      {
         if (variable != null)
         {
            variable.GetType();
         }
      }
   }
}
