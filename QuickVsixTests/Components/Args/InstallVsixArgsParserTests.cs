using CSharpUtils;
using DocoptPlus;
using NUnit.Framework;

[TestFixture]
public class InstallVsixArgsParserTests
{
   InstallVsixArgsParser _installVsixArgsParser;
   DocoptParser p_docoptParserMock;

   [SetUp]
   public void SetUp()
   {
      _installVsixArgsParser = new InstallVsixArgsParser();
      p_docoptParserMock = Mock.Component<DocoptParser>(_installVsixArgsParser, "p_docoptParser");
   }

   [Test]
   public void ParseDocoptDictionary_ParsesArgs_ReturnsQuickVsixArgs()
   {
      string vsixFilePath = Mock.ReturnRandomString(() => p_docoptParserMock.GetRequiredFilePathWhichMustExist(null, null));

      bool waitForAnyKey = Mock.ReturnRandomBool(() => p_docoptParserMock.GetOptionalBool(null, null));

      var docoptDictionary = DocoptPlusTestRandom.DocoptDictionary();
      //
      QuickVsixArgs args = _installVsixArgsParser.ParseDocoptDictionary(docoptDictionary);
      //
      Called.Once(() => p_docoptParserMock.GetRequiredFilePathWhichMustExist(docoptDictionary, "--vsix-file")).Then(
      Called.Once(() => p_docoptParserMock.GetOptionalBool(docoptDictionary, "--wait-for-any-key")));
      var expectedArgs = new QuickVsixArgs
      {
         programMode = ProgramMode.InstallVsix,
         vsixFilePath = vsixFilePath,
         waitForAnyKey = waitForAnyKey
      };
      Assert.AreEqual(expectedArgs, args);
   }
}

