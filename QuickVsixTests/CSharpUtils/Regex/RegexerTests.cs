using CSharpUtils;
using NUnit.Framework;

[TestFixture]
public class RegexerTests
{
   Regexer _regexer;

   [SetUp]
   public void SetUp()
   {
      _regexer = new Regexer();
   }

   [Test]
   public void MatchAndGetGroup_NoMatch_ThrowsArgumentException()
   {
      Assert2.Throws<ArgumentException>(() =>
         Assert.IsNull(_regexer.MatchAndGetGroup("input", "pattern", "groupName")),
         @"Regexer.MatchAndGetGroup(input: ""input"", pattern: ""pattern"", groupName: ""groupName"") did not match.");
   }

   [Test]
   public void MatchAndGetGroup_OneMatch_ReturnsIt()
   {
      Assert.AreEqual("input", _regexer.MatchAndGetGroup("input", "(?<GroupName>.*)", "GroupName"));
      Assert.AreEqual("123", _regexer.MatchAndGetGroup("abc123def", @"(?<ThreeNumbers>\d\d\d)", "ThreeNumbers"));
   }

   [Test]
   public void MatchAndGetGroup_TwoMatches_ReturnsFirstOne()
   {
      Assert.AreEqual(" \t ", _regexer.MatchAndGetGroup(
         " \t 123\t \t", @"(?<ThreeSpaceCharacters>\s\s\s)", "ThreeSpaceCharacters"));
   }

   [Test]
   public void MatchAndGetGroup_OneMatch_InvalidGroupName_ThrowsArgumentException()
   {
      string input = "abc123def";
      string pattern = @"(?<ThreeNumbers>\d\d\d)";
      string groupName = "threenumbers";
      Assert2.Throws<ArgumentException>(() => _regexer.MatchAndGetGroup(
         input, pattern, groupName),
         MakeExpectedExceptionMessage(input, pattern, groupName,
         "called with unrecognized group name."));
   }

   private static string MakeExpectedExceptionMessage(string input, string pattern, string groupName, string why)
   {
      string expectedExceptionMessage = $"Regexer.MatchAndGetGroup(input: \"{input}\", pattern: \"{pattern}\", groupName: \"{groupName}\") {why}";
      return expectedExceptionMessage;
   }
}
