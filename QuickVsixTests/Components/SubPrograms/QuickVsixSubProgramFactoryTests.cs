using CSharpUtils;
using NUnit.Framework;

[TestFixture]
public class QuickVsixSubProgramFactoryTests
{
   QuickVsixSubProgramFactory _quickVsixSubProgramFactory;

   [SetUp]
   public void SetUp()
   {
      _quickVsixSubProgramFactory = new QuickVsixSubProgramFactory();
   }

   [TestCase(ProgramMode.InstallVsix, typeof(InstallVsixSubProgram))]
   [TestCase(ProgramMode.UninstallVsix, typeof(UninstallVsixSubProgram))]
   [TestCase(ProgramMode.ReinstallVsix, typeof(ReinstallVsixSubProgram))]
   public void New_ValidProgramMode_ReturnsExpectedSubProgramType(ProgramMode programMode, Type expectedSubProgramType)
   {
      QuickVsixSubProgram subProgram = _quickVsixSubProgramFactory.New(programMode);
      //
      Type actualSubProgramType = subProgram.GetType();
      Assert.AreEqual(expectedSubProgramType, actualSubProgramType);
   }

   [TestCase(ProgramMode.Unset)]
   [TestCase((ProgramMode)(-1))]
   public void New_InvalidProgramMode_ThrowsArgumentException(ProgramMode invalidProgramMode)
   {
      Assert2.Throws<ArgumentException>(() => _quickVsixSubProgramFactory.New(invalidProgramMode),
         $"Invalid ProgramMode: {invalidProgramMode}");
   }
}
