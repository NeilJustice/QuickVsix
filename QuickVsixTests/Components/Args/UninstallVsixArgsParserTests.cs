using CSharpUtils;
using DocoptPlus;
using NUnit.Framework;

[TestFixture]
public class UninstallVsixArgsParserTests
{
   UninstallVsixArgsParser _uninstallVsixArgsParser;
   DocoptParser p_docoptParserMock;

   [SetUp]
   public void SetUp()
   {
      _uninstallVsixArgsParser = new UninstallVsixArgsParser();
      p_docoptParserMock = Mock.Component<DocoptParser>(_uninstallVsixArgsParser, "p_docoptParser");
   }

   [Test]
   public void ParseDocoptDictionary_ParsesArgs_ReturnsQuickVsixArgs()
   {
      string vsixFilePath = Mock.ReturnRandomString(() => p_docoptParserMock.GetRequiredString(null, null));

      bool waitForAnyKey = Mock.ReturnRandomBool(() => p_docoptParserMock.GetOptionalBool(null, null));

      var docoptDictionary = DocoptPlusTestRandom.DocoptDictionary();
      //
      QuickVsixArgs args = _uninstallVsixArgsParser.ParseDocoptDictionary(docoptDictionary);
      //
      Called.Once(() => p_docoptParserMock.GetRequiredString(docoptDictionary, "--vsix-file")).Then(
      Called.Once(() => p_docoptParserMock.GetOptionalBool(docoptDictionary, "--wait-for-any-key")));
      var expectedArgs = new QuickVsixArgs
      {
         programMode = ProgramMode.UninstallVsix,
         vsixFilePath = vsixFilePath,
         waitForAnyKey = waitForAnyKey
      };
      Assert.AreEqual(expectedArgs, args);
   }
}
