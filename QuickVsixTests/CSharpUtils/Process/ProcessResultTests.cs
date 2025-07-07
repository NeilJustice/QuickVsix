using CSharpUtils;
using NUnit.Framework;

[TestFixture]
public static class ProcessResultTests
{
   [Test]
   public static void DefaultConstructor_SetsFieldsToDefaultValues()
   {
      var processResult = new ProcessResult();
      Assert.IsNull(processResult.processStartInfo);
      Assert.AreEqual(0, processResult.exitCode);
      Assert.IsNull(processResult.standardOutput);
      Assert.IsNull(processResult.standardError);
      Assert.AreEqual(DateTime.MinValue, processResult.startTime);
      Assert.AreEqual(DateTime.MinValue, processResult.endTime);
      Assert.AreEqual(DateTime.MinValue - DateTime.MinValue, processResult.Duration);
   }

   [Test]
   public static void Duration_ReturnsEndTimeMinusStartTime()
   {
      var processResult = new ProcessResult();
      processResult.startTime = TestRandom.DateTime();
      processResult.endTime = TestRandom.DateTime();
      //
      TimeSpan duration = processResult.Duration;
      //
      TimeSpan expectedDuration = processResult.endTime - processResult.startTime;
      Assert.AreEqual(expectedDuration, duration);
   }

   [Test]
   public static void Equals_ThrowsIfAnyFieldNotEqual_OtherwiseReturnsTrue()
   {
      ProcessResult expected = TestRandom.ProcessResultWithRandomExitCode();
      ProcessResult actual = TestRandom.ProcessResultWithRandomExitCode();
      Assert2.EqualsThrowsInvalidCastExceptionWhenComparedWithANewObject(expected);

      NUnitAsserter nunitAsserterMock = Mock.Strict<NUnitAsserter>();
      Mock.Expect(() => nunitAsserterMock.AreEqual((string)default, (string)default));
      Mock.Expect(() => nunitAsserterMock.AreEqual(0, 0));
      Mock.Expect(() => nunitAsserterMock.AreEqual(default(DateTime), default(DateTime)));
      Reflect.Set(expected, "_nunitAsserter", nunitAsserterMock);
      //
      bool areEqual = expected.Equals(actual);
      //
      Called.Once(() => nunitAsserterMock.AreEqual(expected.processStartInfo.FileName, actual.processStartInfo.FileName)).Then(
      Called.Once(() => nunitAsserterMock.AreEqual(expected.processStartInfo.Arguments, actual.processStartInfo.Arguments))).Then(
      Called.Once(() => nunitAsserterMock.AreEqual(expected.exitCode, actual.exitCode))).Then(
      Called.Once(() => nunitAsserterMock.AreEqual(expected.standardOutput, actual.standardOutput))).Then(
      Called.Once(() => nunitAsserterMock.AreEqual(expected.standardError, actual.standardError))).Then(
      Called.Once(() => nunitAsserterMock.AreEqual(expected.startTime, actual.startTime))).Then(
      Called.Once(() => nunitAsserterMock.AreEqual(expected.endTime, actual.endTime)));
      Assert.IsTrue(areEqual);
   }

   [Test]
   public static void GetHashCode_ThrowsNotSupportedException()
   {
      Assert2.ThrowsNotSupportedException(() => new ProcessResult().GetHashCode());
   }
}
