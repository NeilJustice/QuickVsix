using CSharpUtils;
using NUnit.Framework;

[TestFixture]
public static class NUnitEquatableTests
{
   [Test]
   public static void Test()
   {
      var nunitEquatable = new NUnitEquatable();
      Assert2.FieldIsNonNullAndExactType<NUnitAsserter>(nunitEquatable, "_nunitAsserter");
   }
}
