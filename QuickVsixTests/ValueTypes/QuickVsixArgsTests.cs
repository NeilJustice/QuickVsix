using CSharpUtils;
using DocoptPlus;
using NUnit.Framework;
using System.Collections.Generic;

[TestFixture]
public class QuickVsixArgsTests
{
   QuickVsixArgs _quickVsixArgs;

   [SetUp]
   public void SetUp()
   {
      _quickVsixArgs = new QuickVsixArgs();
   }

   [Test]
   public static void DefaultConstructor_SetsFieldsToDefaultValues()
   {
      var defaultQuickVsixArgs = new QuickVsixArgs();
      Assert.AreEqual("QuickVsix", defaultQuickVsixArgs.ProgramName);
      Assert.AreEqual("1.1.0", defaultQuickVsixArgs.Version);
      var expectedDefaultQuickVsixArgs = new QuickVsixArgs
      {
         programMode = ProgramMode.Unset,
         vsixFilePath = null,
         waitForAnyKey = false
      };
      Assert.AreEqual(expectedDefaultQuickVsixArgs, defaultQuickVsixArgs);
   }

   [Test]
   public void Usage_ReturnsExpectedProgramUsageText()
   {
      Assert.AreEqual(@"QuickVsix - Quickly and non-interactively installs, uninstalls, or reinstalls Visual Studio extensions.

Usage:
   QuickVsix.exe install-vsix --vsix-file=<FilePath> [--wait-for-any-key]
   QuickVsix.exe uninstall-vsix --vsix-file=<FilePath> [--wait-for-any-key]
   QuickVsix.exe reinstall-vsix --vsix-file=<FilePath> [--wait-for-any-key]
   QuickVsix.exe --help
   QuickVsix.exe --version", _quickVsixArgs.Usage);
   }

   [Test]
   public static void ProgramModesDictionary_IsExpectedDictionary()
   {
      var expectedProgramModesDictionary = new Dictionary<string, ProgramMode>
      {
         { "install-vsix", ProgramMode.InstallVsix },
         { "uninstall-vsix", ProgramMode.UninstallVsix },
         { "reinstall-vsix", ProgramMode.ReinstallVsix }
      };
      Assert.AreEqual(expectedProgramModesDictionary, QuickVsixArgs.programModesDictionary);
   }

   [Test]
   public void Populate_ThrowsNotSupportedException()
   {
      var docoptArguments = new Dictionary<string, DocoptValueObject>().ToReadOnlyDictionary();
      //
      Assert2.ThrowsNotSupportedException(() => _quickVsixArgs.Populate(docoptArguments));
   }

   [Test]
   public static void Equals_ReturnsTrueIfAllFieldsAreEqualOtherwiseThrows()
   {
      var expected = QuickVsixTestRandom.Args();
      var actual = QuickVsixTestRandom.Args();
      Assert2.EqualsThrowsInvalidCastExceptionWhenComparedWithANewObject(actual);

      var nunitAsserterMock = Mock.Component<NUnitAsserter>(expected, "_nunitAsserter");
      Mock.Expect(() => nunitAsserterMock.AreEqual(default(string), default(string)));
      Mock.Expect(() => nunitAsserterMock.AreEqual(default(ProgramMode), default(ProgramMode)));
      Mock.Expect(() => nunitAsserterMock.AreEqual(default(bool), default(bool)));
      //
      bool areEqual = expected.Equals(actual);
      //
      Called.NumberOfTimes(2, () => nunitAsserterMock.AreEqual(default(string), default(string)));
      Called.WasCalled(() => nunitAsserterMock.AreEqual(expected.CommandLine, actual.CommandLine)).Then(
      Called.Once(() => nunitAsserterMock.AreEqual(expected.programMode, actual.programMode))).Then(
      Called.WasCalled(() => nunitAsserterMock.AreEqual(expected.vsixFilePath, actual.vsixFilePath))).Then(
      Called.Once(() => nunitAsserterMock.AreEqual(expected.waitForAnyKey, actual.waitForAnyKey)));
      Assert.IsTrue(areEqual);
   }

   [Test]
   public static void GetHashCode_ThrowsThrowsNotImplementException()
   {
      Assert2.ThrowsNotSupportedException(() => new QuickVsixArgs().GetHashCode());
   }
}
