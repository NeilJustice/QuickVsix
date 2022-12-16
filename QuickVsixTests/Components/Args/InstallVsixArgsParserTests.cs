using CSharpUtils;
using DocoptPlus;
using NUnit.Framework;
using System.Collections.Generic;

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
      string vsixFilePath = Mock.ReturnRandomString(() => p_docoptParserMock.GetRequiredFilePathWhichMustAlreadyExist(null, null));

      bool waitForAnyKey = Mock.ReturnRandomBool(() => p_docoptParserMock.GetOptionalBool(null, null));

      var docoptDictionary = new Dictionary<string, DocoptValueObject>().ToReadOnlyDictionary();
      ProgramMode programMode = TestRandom.Enum<ProgramMode>();
      //
      QuickVsixArgs args = _installVsixArgsParser.ParseDocoptDictionary(docoptDictionary, programMode);
      //
      Called.Once(() => p_docoptParserMock.GetRequiredFilePathWhichMustAlreadyExist(docoptDictionary, "--vsix-file")).Then(
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

