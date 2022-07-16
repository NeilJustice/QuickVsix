using CSharpUtils;
using DocoptPlus;
using NUnit.Framework;

[TestFixture]
public static class ProgramModeSpecificArgsParserTests
{
   [Test]
   public static void DefaultConstructor_NewsDocoptParser()
   {
      var programModeSpecificArgsParser = new ProgramModeSpecificArgsParser();
      Assert2.FieldIsNonNullAndExactType<ConsoleWriter>(programModeSpecificArgsParser, "p_consoleWriter");
      Assert2.AssertConsoleWriterHasProgramName("QuickVsix", programModeSpecificArgsParser, "p_consoleWriter");
      Assert2.FieldIsNonNullAndExactType<DocoptParser>(programModeSpecificArgsParser, "p_docoptParser");
      Assert2.FieldIsNonNullAndExactType<FileSystem>(programModeSpecificArgsParser, "p_fileSystem");
   }

   [Test]
   public static void ParseStringArgs_ThrowsNotSupportedException()
   {
      var programModeSpecificArgsParser = new ProgramModeSpecificArgsParser();
      Assert2.ThrowsNotSupportedException(() => programModeSpecificArgsParser.ParseDocoptDictionary(null, ProgramMode.Unset));
   }
}
