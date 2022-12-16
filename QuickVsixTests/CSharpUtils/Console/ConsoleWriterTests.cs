using CSharpUtils;
using NUnit.Framework;
using System;

[TestFixture]
public class ConsoleWriterTests
{
   ConsoleWriter _consoleWriter;
   Asserter _asserterMock;
   MethodCaller _methodCallerMock;
   Watch _watchMock;

   [SetUp]
   public void SetUp()
   {
      _consoleWriter = new ConsoleWriter();
      _asserterMock = Mock.Component<Asserter>(_consoleWriter, "_asserter");
      _methodCallerMock = Mock.Component<MethodCaller>(_consoleWriter, "_methodCaller");
      _watchMock = Mock.Component<Watch>(_consoleWriter, "_watch");
   }

   [Test]
   public static void DefaultConstructor_SetsProgramNameToNull()
   {
      var consoleWriter = new ConsoleWriter();
      Assert2.PrivateFieldSameAs(null, consoleWriter, "_programName");
   }

   [Test]
   public static void OneArgConstructor_SetsProgramName()
   {
      string programName = TestRandom.String();
      //
      var consoleWriter = new ConsoleWriter(programName);
      //
      Assert2.PrivateFieldSameAs(programName, consoleWriter, "_programName");
   }

   [Test]
   public void OptionallyPressAnyKeyToContinue_WaitForAnyKeyIsFalse_DoesNothing()
   {
      Assert.DoesNotThrow(() => _consoleWriter.OptionallyPressAnyKeyToContinue(false));
   }

   [Test]
   public void OptionallyPressAnyKeyToContinue_WaitForAnyKeyIsTrue_PrintsPressAnyKeyToContinue_CallsConsoleReadLine()
   {
      Mock.Expect(() => _methodCallerMock.CallAction(default(Action<string>), null));
      Mock.Expect(() => _methodCallerMock.CallFunction(default(Func<string>)));
      //
      _consoleWriter.OptionallyPressAnyKeyToContinue(true);
      //
      Called.Once(() => _methodCallerMock.CallAction(_consoleWriter.WriteProgramNameTimestampedLine, "Press any key to continue . . .")).Then(
      Called.Once(() => _methodCallerMock.CallFunction(Console.ReadLine)));
   }

   [Test]
   public void SetProgramName_SetsProgramName()
   {
      string programName = TestRandom.String();
      //
      _consoleWriter.SetProgramName(programName);
      //
      Assert.AreEqual(programName, Reflect.Get<string>(_consoleWriter, "_programName"));
   }

   [Test]
   public void Write_CallsConsoleWriteWithMessage()
   {
      Mock.Expect(() => _methodCallerMock.CallAction(default(Action<string>), null));
      string message = TestRandom.String();
      //
      _consoleWriter.Write(message);
      //
      Called.Once(() => _methodCallerMock.CallAction(Console.Write, message));
   }

   [Test]
   public void WriteLine_WriteLinesMessageToConsole()
   {
      Mock.Expect(() => _methodCallerMock.CallAction(default(Action<string>), null));
      string message = TestRandom.String();
      //
      _consoleWriter.WriteLine(message);
      //
      Called.Once(() => _methodCallerMock.CallAction(Console.WriteLine, message));
   }

   [Test]
   public void WriteLineColor_WritesLineWithColor()
   {
      ConsoleColor initialForegroundColor = Mock.ReturnRandomEnum(
         () => _methodCallerMock.CallFunction(default(Func<ConsoleColor, ConsoleColor>), default));

      Mock.Expect(() => _methodCallerMock.CallAction(default(Action<string>), null));

      string message = TestRandom.String();
      ConsoleColor textColor = TestRandom.Enum<ConsoleColor>();
      //
      _consoleWriter.WriteLineWithColor(message, textColor);
      //
      Called.NumberOfTimes(2, () => _methodCallerMock.CallFunction(_consoleWriter.SetForegroundColor, textColor));
      Called.WasCalled(() => _methodCallerMock.CallFunction(_consoleWriter.SetForegroundColor, textColor)).Then(
      Called.Once(() => _methodCallerMock.CallAction(Console.WriteLine, message))).Then(
      Called.WasCalled(() => _methodCallerMock.CallFunction(_consoleWriter.SetForegroundColor, initialForegroundColor)));
   }

   [Test]
   public void WriteNewLine_CallsConsoleWriteLine()
   {
      Mock.Expect(() => _methodCallerMock.CallAction(default));
      //
      _consoleWriter.WriteNewLine();
      //
      Called.Once(() => _methodCallerMock.CallAction(Console.WriteLine));
   }

   [Test]
   public void WriteProgramNameTimestampedLine_WritesProgramNameTimestampedLineToConsole()
   {
      string dateTimeNow = Mock.ReturnRandomString(() => _watchMock.DateTimeNowString());

      Mock.Expect(() => _asserterMock.ThrowIfNull(default(string), default(string)));

      Mock.Expect(() => _methodCallerMock.CallAction(default(Action<string>), null));

      string programName = TestRandom.String();
      Reflect.Set(_consoleWriter, "_programName", programName);
      string message = TestRandom.String();
      //
      _consoleWriter.WriteProgramNameTimestampedLine(message);
      //
      string expectedProgramNameTimestampedMessage = $"[{programName} {dateTimeNow}] {message}";
      Called.Once(() => _watchMock.DateTimeNowString()).Then(
      Called.Once(() => _asserterMock.ThrowIfNull(programName, "_programName"))).Then(
      Called.Once(() => _methodCallerMock.CallAction(Console.WriteLine, expectedProgramNameTimestampedMessage)));
   }

   [Test]
   public void WriteProgramNameTimestampedLineWithColor_WritesProgramNameTimestampedLineWithColor()
   {
      ConsoleColor initialForegroundColor = Mock.ReturnRandomEnum(() => _methodCallerMock.CallFunction(default(Func<ConsoleColor, ConsoleColor>), default));
      Mock.Expect(() => _methodCallerMock.CallAction(default(Action<string>), null));

      string message = TestRandom.String();
      ConsoleColor textColor = TestRandom.Enum<ConsoleColor>();
      //
      _consoleWriter.WriteProgramNameTimestampedLineWithColor(message, textColor);
      //
      Called.NumberOfTimes(2, () => _methodCallerMock.CallFunction(_consoleWriter.SetForegroundColor, textColor));
      Called.WasCalled(() => _methodCallerMock.CallFunction(_consoleWriter.SetForegroundColor, textColor)).Then(
      Called.Once(() => _methodCallerMock.CallAction(_consoleWriter.WriteProgramNameTimestampedLine, message))).Then(
      Called.WasCalled(() => _methodCallerMock.CallFunction(_consoleWriter.SetForegroundColor, initialForegroundColor)));
   }
}
