using CSharpUtils;
using NUnit.Framework;

[TestFixture]
public static class StringExtensionsTests
{
   [TestCase("", "")]
   [TestCase("\n", "\n")]
   [TestCase("\r", "")]
   [TestCase("\r\n", "\n")]
   [TestCase("\r\r\n\r\r", "\n")]
   [TestCase("abc\r\ndef\r\n", "abc\ndef\n")]
   public static void RemoveCarriageReturns_ReturnStringWithAllBackslashRCharactersRemoved(
       string str, string expectedReturnValue)
   {
      string stringWithCarriageReturnsRemoved = str.RemoveCarriageReturns();
      Assert.AreEqual(expectedReturnValue, stringWithCarriageReturnsRemoved);
   }

   [TestCase("", 100, "")]
   [TestCase("123", 0, "")]
   [TestCase(" ", 5, "     ")]
   [TestCase("abc", 3, "abcabcabc")]
   public static void Repeat_ReturnsStringWithSpecifiedStringRepeatedNTimes(string stringToRepeat, int numTimesToRepeat, string expected)
   {
      string actual = stringToRepeat.Repeat(numTimesToRepeat);
      Assert.AreEqual(expected, actual);
   }
}
