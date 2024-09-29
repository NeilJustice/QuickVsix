using CSharpUtils;
using NUnit.Framework;
using System;
using System.IO;

[TestFixture]
public class OneArgOneArgTryCatchCallerTests
{
   OneArgTryCatchCaller _oneArgTryCatchCaller;
   Environmentalist _environmentalistMock;

   int _numberOfFunctionCalls;
   int _functionArgument;
   string _functionReturnValue;
   int _receivedFunctionArgument;

   int _numberOfExceptionHandlerCalls;
   int _receivedExceptionHandlerFunctionArgument;
   string _exceptionMessage;
   string _exceptionHandlerReturnValue;
   int _failFastExceptionHandlerReturnValue;
   Exception _receivedExceptionHandlerException;

   [SetUp]
   public void SetUp()
   {
      _oneArgTryCatchCaller = new OneArgTryCatchCaller();
      _environmentalistMock = Mock.Component<Environmentalist>(_oneArgTryCatchCaller, "_environmentalist");

      _numberOfFunctionCalls = 0;
      _functionArgument = TestRandom.Int();
      _functionReturnValue = null;
      _receivedFunctionArgument = 0;

      _numberOfExceptionHandlerCalls = 0;
      _receivedExceptionHandlerFunctionArgument = 0;
      _exceptionMessage = null;
      _exceptionHandlerReturnValue = null;
      _failFastExceptionHandlerReturnValue = 0;
      _receivedExceptionHandlerException = null;
   }

   [Test]
   public void FailFastTryCatchCall_CallsFunctionWhichDoesNotThrow_ReturnsFunctionReturnValue()
   {
      _functionReturnValue = TestRandom.String();
      //
      string returnValue = _oneArgTryCatchCaller.FailFastTryCatchCall((int functionArgument_function) =>
      {
         ++_numberOfFunctionCalls;
         _receivedFunctionArgument = functionArgument_function;
         return _functionReturnValue;
      },
      _functionArgument, FailFastExceptionHandler);
      //
      Assert.AreEqual(1, _numberOfFunctionCalls);
      Assert.AreEqual(_receivedFunctionArgument, _functionArgument);
      Assert.IsNull(_receivedExceptionHandlerException);
      Assert.AreEqual(0, _numberOfExceptionHandlerCalls);
      Assert.AreEqual(0, _receivedExceptionHandlerFunctionArgument);
      Assert.AreEqual(_functionReturnValue, returnValue);
   }

   int FailFastExceptionHandler(Exception exceptionHandlerException, int exceptionHandlerFunctionArgument)
   {
      ++_numberOfExceptionHandlerCalls;
      _receivedExceptionHandlerFunctionArgument = exceptionHandlerFunctionArgument;
      _receivedExceptionHandlerException = exceptionHandlerException;
      return _failFastExceptionHandlerReturnValue = TestRandom.Int();
   }

   [Test]
   public void FailFastTryCatchCall_CallsFunctionWhichsThrowsAnyException_CallsExceptionHandler_ExitsTheProgramWithExitCodeEqualToTheExceptionHandlersReturnValue()
   {
      Mock.Expect(() => _environmentalistMock.Exit(default));
      _functionArgument = TestRandom.Int();
      //
      string returnValue = _oneArgTryCatchCaller.FailFastTryCatchCall(
          ThrowingFunction, _functionArgument, FailFastExceptionHandler);
      //
      Assert.AreEqual(1, _numberOfFunctionCalls);
      Assert.AreEqual(_functionArgument, _receivedFunctionArgument);

      Assert.AreEqual(_exceptionMessage, _receivedExceptionHandlerException.Message);
      Assert.AreEqual(1, _numberOfExceptionHandlerCalls);
      Assert.AreEqual(_functionArgument, _receivedExceptionHandlerFunctionArgument);
      Assert.AreEqual(_exceptionHandlerReturnValue, returnValue);

      Called.Once(() => _environmentalistMock.Exit(_failFastExceptionHandlerReturnValue));
   }

   [Test]
   public void TryCatchCall_CallsFunctionWhichDoesNotThrow_ReturnsFunctionReturnValue()
   {
      _functionReturnValue = TestRandom.String();
      //
      string returnValue = _oneArgTryCatchCaller.TryCatchCall((int functionArgument_function) =>
      {
         ++_numberOfFunctionCalls;
         _receivedFunctionArgument = functionArgument_function;
         return _functionReturnValue;
      }, _functionArgument, ExceptionHandler);
      //
      Assert.AreEqual(1, _numberOfFunctionCalls);
      Assert.AreEqual(_receivedFunctionArgument, _functionArgument);
      Assert.IsNull(_receivedExceptionHandlerException);
      Assert.AreEqual(0, _numberOfExceptionHandlerCalls);
      Assert.AreEqual(0, _receivedExceptionHandlerFunctionArgument);
      Assert.AreEqual(_functionReturnValue, returnValue);
   }

   string ExceptionHandler(Exception exceptionHandlerException, int exceptionHandlerFunctionArgument)
   {
      ++_numberOfExceptionHandlerCalls;
      _receivedExceptionHandlerFunctionArgument = exceptionHandlerFunctionArgument;
      _receivedExceptionHandlerException = exceptionHandlerException;
      return _exceptionHandlerReturnValue = TestRandom.String();
   }

   private string ThrowingFunction(int functionArgument)
   {
      ++_numberOfFunctionCalls;
      _receivedFunctionArgument = functionArgument;
      throw new FileNotFoundException(_exceptionMessage = TestRandom.String());
   }

   [Test]
   public void TryCatchCall_CallsFunctionWhichsThrowsAnyException_CallsExceptionHandler_ReturnsExceptionHandlerReturnValue()
   {
      _functionArgument = TestRandom.Int();
      //
      string returnValue = _oneArgTryCatchCaller.TryCatchCall(ThrowingFunction, _functionArgument, ExceptionHandler);
      //
      Assert.AreEqual(1, _numberOfFunctionCalls);
      Assert.AreEqual(_functionArgument, _receivedFunctionArgument);

      Assert.AreEqual(_exceptionMessage, _receivedExceptionHandlerException.Message);
      Assert.AreEqual(1, _numberOfExceptionHandlerCalls);
      Assert.AreEqual(_functionArgument, _receivedExceptionHandlerFunctionArgument);
      Assert.AreEqual(_exceptionHandlerReturnValue, returnValue);
   }
}
