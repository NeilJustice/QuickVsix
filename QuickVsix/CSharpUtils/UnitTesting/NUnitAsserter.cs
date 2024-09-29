using NUnit.Framework;

namespace CSharpUtils
{
   public class NUnitAsserter
   {
      public virtual void AreEqual<T>(T expected, T actual)
      {
         Assert.AreEqual(expected, actual);
      }
   }
}
