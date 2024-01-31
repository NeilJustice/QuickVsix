using CSharpUtils;
using NUnit.Framework;

[TestFixture]
public static class NUnitAsserterTests
{
   [Test]
   public static void AreEqual_CallsNUnitAssertAreEqual()
   {
      var nunitAsserter = new NUnitAsserter();

      nunitAsserter.AreEqual(0, 0);
      Assert2.Throws<AssertionException>(() => nunitAsserter.AreEqual(1, 0),
@"  Assert.That(actual, Is.EqualTo(expected))
  Expected: 1
  But was:  0
");

      nunitAsserter.AreEqual("hello", "hello");
      Assert2.Throws<AssertionException>(() => nunitAsserter.AreEqual("hello", "goodbye"),
@"  Assert.That(actual, Is.EqualTo(expected))
  Expected string length 5 but was 7. Strings differ at index 0.
  Expected: ""hello""
  But was:  ""goodbye""
  -----------^
");
   }
}
