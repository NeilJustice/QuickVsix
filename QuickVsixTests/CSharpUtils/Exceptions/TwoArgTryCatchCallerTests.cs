using CSharpUtils;
using NUnit.Framework;
using System;
using System.IO;

[TestFixture]
public class TwoArgOneArgTryCatchCallerTests
{
   TwoArgTryCatchCaller _twoArgTryCatchCaller;
   int _numberOfFunctionCalls;
   int _receivedFunctionArg1;
   int _receivedFunctionArg2;

   Exception _receivedExceptionHandlerException;
   int _numberOfExceptionHandlerCalls;
   int _receivedExceptionHandlerArg1;
   int _receivedExceptionHandlerArg2;
   string _exceptionMessage;

   [SetUp]
   public void SetUp()
   {
      _twoArgTryCatchCaller = new TwoArgTryCatchCaller();

      _numberOfFunctionCalls = 0;
      _receivedFunctionArg1 = 0;
      _receivedFunctionArg2 = 0;

      _receivedExceptionHandlerException = null;
      _numberOfExceptionHandlerCalls = 0;
      _receivedExceptionHandlerArg1 = 0;
      _receivedExceptionHandlerArg2 = 0;
      _exceptionMessage = null;
   }

   [Test]
   public void TryCatchCall_CallsMethodWhichDoesNotThrow()
   {
      int arg1 = TestRandom.Int();
      int arg2 = TestRandom.Int();
      //
      _twoArgTryCatchCaller.TryCatchCallVoidMethod((int functionArg1, int functionArg2) =>
      {
         ++_numberOfFunctionCalls;
         _receivedFunctionArg1 = functionArg1;
         _receivedFunctionArg2 = functionArg2;
      }, arg1, arg2, ExceptionHandler);
      //
      Assert.AreEqual(1, _numberOfFunctionCalls);
      Assert.AreEqual(arg1, _receivedFunctionArg1);
      Assert.AreEqual(arg2, _receivedFunctionArg2);

      Assert.IsNull(_receivedExceptionHandlerException);
      Assert.AreEqual(0, _numberOfExceptionHandlerCalls);
      Assert.AreEqual(0, _receivedExceptionHandlerArg1);
      Assert.AreEqual(0, _receivedExceptionHandlerArg2);
   }

   void ExceptionHandler(Exception exceptionHandlerException, int exceptionHandlerArg1, int exceptionHandlerArg2)
   {
      ++_numberOfExceptionHandlerCalls;
      _receivedExceptionHandlerArg1 = exceptionHandlerArg1;
      _receivedExceptionHandlerArg2 = exceptionHandlerArg2;
      _receivedExceptionHandlerException = exceptionHandlerException;
   }

   private void ThrowingMethod(int functionArg1, int functionArg2)
   {
      ++_numberOfFunctionCalls;
      _receivedFunctionArg1 = functionArg1;
      _receivedFunctionArg2 = functionArg2;
      throw new FileNotFoundException(_exceptionMessage = TestRandom.String());
   }

   [Test]
   public void TryCatchCall_CallsFunctionWhichsThrowsAnyException_CallsExceptionHandler_ReturnsExceptionHandlerReturnValue()
   {
      int arg1 = TestRandom.Int();
      int arg2 = TestRandom.Int();
      //
      _twoArgTryCatchCaller.TryCatchCallVoidMethod(ThrowingMethod, arg1, arg2, ExceptionHandler);
      //
      Assert.AreEqual(1, _numberOfFunctionCalls);
      Assert.AreEqual(arg1, _receivedFunctionArg1);
      Assert.AreEqual(arg2, _receivedFunctionArg2);

      Assert.AreEqual(_exceptionMessage, _receivedExceptionHandlerException.Message);
      Assert.AreEqual(1, _numberOfExceptionHandlerCalls);
      Assert.AreEqual(arg1, _receivedExceptionHandlerArg1);
      Assert.AreEqual(arg2, _receivedExceptionHandlerArg2);
   }
}
