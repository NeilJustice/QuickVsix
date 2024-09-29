using CSharpUtils;
using NUnit.Framework;
using System;

[TestFixture]
public class EnvironmentalistTests
{
   Environmentalist _environmentalist;
   MethodCaller _methodCallerMock;

   [SetUp]
   public void SetUp()
   {
      _environmentalist = new Environmentalist();
      _methodCallerMock = Mock.Component<MethodCaller>(_environmentalist, "_methodCaller");
   }

   [Test]
   public void GetUserEnvironmentVariable_EnvironmentVariableIsNotDefined_ThrowsArgumentException()
   {
      Mock.Return(() => _methodCallerMock.CallFunction(default(Func<string, EnvironmentVariableTarget, string>), null, default), null);
      string environmentVariableName = TestRandom.String();
      //
      Assert2.Throws<ArgumentException>(() => _environmentalist.GetUserEnvironmentVariable(environmentVariableName),
         $"User environment variable {environmentVariableName} is not defined");
      //
      Called.Once(() => _methodCallerMock.CallFunction(
         Environment.GetEnvironmentVariable, environmentVariableName, EnvironmentVariableTarget.User));
   }

   [Test]
   public void GetUserEnvironmentVariable_EnvironmentVariableIsDefined_ReturnsValue()
   {
      string environmentVariableValue = Mock.ReturnRandomString(
         () => _methodCallerMock.CallFunction(default(Func<string, EnvironmentVariableTarget, string>), null, default));
      string environmentVariableName = TestRandom.String();
      //
      string returnedEnvironmentVariableValue = _environmentalist.GetUserEnvironmentVariable(environmentVariableName);
      //
      Called.Once(() => _methodCallerMock.CallFunction(
         Environment.GetEnvironmentVariable, environmentVariableName, EnvironmentVariableTarget.User));
      Assert.AreEqual(environmentVariableValue, returnedEnvironmentVariableValue);
   }

   [Test]
   public void Exit_CallsEnvironmentExitWithExitCode()
   {
      Mock.Expect(() => _methodCallerMock.CallAction(default, 0));
      int exitCode = TestRandom.Int();
      //
      _environmentalist.Exit(exitCode);
      //
      Called.Once(() => _methodCallerMock.CallAction(Environment.Exit, exitCode));
   }
}
