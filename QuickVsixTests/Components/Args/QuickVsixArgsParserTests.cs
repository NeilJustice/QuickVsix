using System.Collections.ObjectModel;
using CSharpUtils;
using DocoptPlus;
using NUnit.Framework;

[TestFixture]
public class QuickVsixArgsParserTests
{
   QuickVsixArgsParser _quickVsixArgsParser;
   DocoptParser _docoptParserMock;
   ProgramModeSpecificArgsParserFactory _programModeSpecificArgsParserFactoryMock;

   [SetUp]
   public void SetUp()
   {
      _quickVsixArgsParser = new QuickVsixArgsParser();
      _docoptParserMock = Mock.Component<DocoptParser>(_quickVsixArgsParser, "_docoptParser");
      _programModeSpecificArgsParserFactoryMock = Mock.Component<ProgramModeSpecificArgsParserFactory>(_quickVsixArgsParser, "_programModeSpecificArgsParserFactory");
   }

   [Test]
   public static void ProgramModesDictionary_IsExpectedReadOnlyDictionary()
   {
      var expectedProgramModesDictionary = new Dictionary<string, ProgramMode>
      {
         { "install-vsix", ProgramMode.InstallVsix },
         { "uninstall-vsix", ProgramMode.UninstallVsix },
         { "reinstall-vsix", ProgramMode.ReinstallVsix }
      }.ToReadOnlyDictionary();
      Assert.AreEqual(expectedProgramModesDictionary, QuickVsixArgsParser.s_programModesDictionary);
   }

   [Test]
   public void ParseStringArgs_ParsesStringArgsForDocoptDictionary_GetsProgramMode_NewsProgramModeSpecificArgsParser_ParsesArgs_ReturnsArg()
   {
      ReadOnlyDictionary<string, DocoptValueObject> docoptDictionary = new Dictionary<string, DocoptValueObject>().ToReadOnlyDictionary();
      Mock.Return(() => _docoptParserMock.ParseStringArgsForDocoptDictionary<QuickVsixArgs>(null), docoptDictionary);

      ProgramMode programMode = Mock.ReturnRandomEnum(() => _docoptParserMock.GetProgramMode<ProgramMode>(null, null));

      ProgramModeSpecificArgsParser programModeSpecificArgsParserMock = Mock.Strict<ProgramModeSpecificArgsParser>();
      Mock.Return(() => _programModeSpecificArgsParserFactoryMock.New(default), programModeSpecificArgsParserMock);

      QuickVsixArgs args = QuickVsixTestRandom.Args();
      Mock.Return(() => programModeSpecificArgsParserMock.ParseDocoptDictionary(null, default), args);

      string[] stringArgs = TestRandom.StringArray();
      //
      QuickVsixArgs returnedArgs = _quickVsixArgsParser.ParseStringArgs(stringArgs);
      //
      Called.Once(() => _docoptParserMock.ParseStringArgsForDocoptDictionary<QuickVsixArgs>(stringArgs)).Then(
      Called.Once(() => _docoptParserMock.GetProgramMode(docoptDictionary, QuickVsixArgs.programModesDictionary))).Then(
      Called.Once(() => _programModeSpecificArgsParserFactoryMock.New(programMode))).Then(
      Called.Once(() => programModeSpecificArgsParserMock.ParseDocoptDictionary(docoptDictionary, programMode)));
      Assert.AreEqual(args, returnedArgs);
   }
}
