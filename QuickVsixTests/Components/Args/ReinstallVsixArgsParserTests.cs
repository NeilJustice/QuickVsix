using CSharpUtils;
using DocoptPlus;
using NUnit.Framework;

[TestFixture]
public class ReinstallVsixArgsParserTests
{
   ReinstallVsixArgsParser _reinstallVsixArgsParser;
   DocoptParser p_docoptParserMock;

   [SetUp]
   public void SetUp()
   {
      _reinstallVsixArgsParser = new ReinstallVsixArgsParser();
      p_docoptParserMock = Mock.Component<DocoptParser>(_reinstallVsixArgsParser, "p_docoptParser");
   }

   [Test]
   public void ParseDocoptDictionary_ParsesArgs_ReturnsQuickVsixArgs()
   {
      string vsixFilePath = Mock.ReturnRandomString(() => p_docoptParserMock.GetRequiredString(null, null));

      bool waitForAnyKey = Mock.ReturnRandomBool(() => p_docoptParserMock.GetOptionalBool(null, null));

      var docoptDictionary = new Dictionary<string, DocoptValueObject>().ToReadOnlyDictionary();
      ProgramMode programMode = TestRandom.Enum<ProgramMode>();
      //
      QuickVsixArgs args = _reinstallVsixArgsParser.ParseDocoptDictionary(docoptDictionary, programMode);
      //
      Called.Once(() => p_docoptParserMock.GetRequiredString(docoptDictionary, "--vsix-file")).Then(
      Called.Once(() => p_docoptParserMock.GetOptionalBool(docoptDictionary, "--wait-for-any-key")));
      var expectedArgs = new QuickVsixArgs
      {
         programMode = programMode,
         vsixFilePath = vsixFilePath,
         waitForAnyKey = waitForAnyKey
      };
      Assert.AreEqual(expectedArgs, args);
   }
}
