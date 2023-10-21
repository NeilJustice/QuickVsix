using CSharpUtils;
using NUnit.Framework;

[TestFixture]
public class ProgramModeSpecificArgsParserFactoryTests
{
   ProgramModeSpecificArgsParserFactory _programModeSpecificArgsParserFactory;

   [SetUp]
   public void SetUp()
   {
      _programModeSpecificArgsParserFactory = new ProgramModeSpecificArgsParserFactory();
   }

   [Test]
   public void New_ProgramModeIsInstallVsix_ReturnsNewInstallVsixArgsParser()
   {
      Assert.IsInstanceOf<InstallVsixArgsParser>(
         _programModeSpecificArgsParserFactory.New(ProgramMode.InstallVsix));
   }

   [Test]
   public void New_ProgramModeIsUninstallVsix_ReturnsNewUninstallVsixArgsParser()
   {
      Assert.IsInstanceOf<UninstallVsixArgsParser>(
         _programModeSpecificArgsParserFactory.New(ProgramMode.UninstallVsix));
   }

   [Test]
   public void New_ProgramModeIsReinstallVsix_ReturnsNewReinstallVsixArgsParser()
   {
      Assert.IsInstanceOf<ReinstallVsixArgsParser>(
         _programModeSpecificArgsParserFactory.New(ProgramMode.ReinstallVsix));
   }

   [Test]
   public void New_ProgramModeIsUnset_ThrowsArgumentException()
   {
      Assert2.Throws<ArgumentException>(() => _programModeSpecificArgsParserFactory.New(ProgramMode.Unset),
         $"Invalid ProgramMode: Unset");
   }
}
