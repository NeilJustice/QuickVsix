using CSharpUtils;
using NUnit.Framework;
using System;

[TestFixture]
public class MethodCallerTests
{
   MethodCaller _methodCaller;

   [SetUp]
   public void SetUp()
   {
      _methodCaller = new MethodCaller();
   }

   [Test]
   public void CallAction_0Args_CallsActionOnce()
   {
      int numberOfCalls = 0;
      void method()
      {
         ++numberOfCalls;
      }
      //
      _methodCaller.CallAction(method);
      //
      Assert.AreEqual(1, numberOfCalls);
   }

   [Test]
   public void CallAction_1Arg_CallsActionOnce()
   {
      int numberOfCalls = 0;
      int receivedArgument = 0;
      void action(int argument)
      {
         ++numberOfCalls;
         receivedArgument = argument;
      }
      int passedInArgument = TestRandom.Int();
      //
      _methodCaller.CallAction(action, passedInArgument);
      //
      Assert.AreEqual(1, numberOfCalls);
      Assert.AreEqual(passedInArgument, receivedArgument);
   }

   [Test]
   public void CallAction_2Args_CallsActionOnce()
   {
      int numberOfCalls = 0;
      int receivedArgument1 = 0;
      int receivedArgument2 = 0;
      void action(int argument1, int argument2)
      {
         ++numberOfCalls;
         receivedArgument1 = argument1;
         receivedArgument2 = argument2;
      }
      int passedInArgument1 = TestRandom.Int();
      int passedInArgument2 = TestRandom.Int();
      //
      _methodCaller.CallAction(action, passedInArgument1, passedInArgument2);
      //
      Assert.AreEqual(1, numberOfCalls);
      Assert.AreEqual(passedInArgument1, receivedArgument1);
      Assert.AreEqual(passedInArgument2, receivedArgument2);
   }

   [Test]
   public void CallAction_3Args_CallsActionOnce()
   {
      int numberOfCalls = 0;
      int receivedArgument1 = 0;
      int receivedArgument2 = 0;
      int receivedArgument3 = 0;
      void action(int argument1, int argument2, int argument3)
      {
         ++numberOfCalls;
         receivedArgument1 = argument1;
         receivedArgument2 = argument2;
         receivedArgument3 = argument3;
      }
      int passedInArgument1 = TestRandom.Int();
      int passedInArgument2 = TestRandom.Int();
      int passedInArgument3 = TestRandom.Int();
      //
      _methodCaller.CallAction(action, passedInArgument1, passedInArgument2, passedInArgument3);
      //
      Assert.AreEqual(1, numberOfCalls);
      Assert.AreEqual(passedInArgument1, receivedArgument1);
      Assert.AreEqual(passedInArgument2, receivedArgument2);
      Assert.AreEqual(passedInArgument3, receivedArgument3);
   }

   [Test]
   public void CallAction_4Args_CallsActionOnce()
   {
      int numberOfCalls = 0;
      int receivedArgument1 = 0;
      int receivedArgument2 = 0;
      int receivedArgument3 = 0;
      int receivedArgument4 = 0;
      void action(int argument1, int argument2, int argument3, int argument4)
      {
         ++numberOfCalls;
         receivedArgument1 = argument1;
         receivedArgument2 = argument2;
         receivedArgument3 = argument3;
         receivedArgument4 = argument4;
      }
      int passedInArgument1 = TestRandom.Int();
      int passedInArgument2 = TestRandom.Int();
      int passedInArgument3 = TestRandom.Int();
      int passedInArgument4 = TestRandom.Int();
      //
      _methodCaller.CallAction(action, passedInArgument1, passedInArgument2, passedInArgument3, passedInArgument4);
      //
      Assert.AreEqual(1, numberOfCalls);
      Assert.AreEqual(passedInArgument1, receivedArgument1);
      Assert.AreEqual(passedInArgument2, receivedArgument2);
      Assert.AreEqual(passedInArgument3, receivedArgument3);
      Assert.AreEqual(passedInArgument4, receivedArgument4);
   }

   [Test]
   public void CallAction_5Args_CallsActionOnce()
   {
      int numberOfCalls = 0;
      int receivedArgument1 = 0;
      int receivedArgument2 = 0;
      int receivedArgument3 = 0;
      int receivedArgument4 = 0;
      int receivedArgument5 = 0;
      void action(int argument1, int argument2, int argument3, int argument4, int argument5)
      {
         ++numberOfCalls;
         receivedArgument1 = argument1;
         receivedArgument2 = argument2;
         receivedArgument3 = argument3;
         receivedArgument4 = argument4;
         receivedArgument5 = argument5;
      }
      int passedInArgument1 = TestRandom.Int();
      int passedInArgument2 = TestRandom.Int();
      int passedInArgument3 = TestRandom.Int();
      int passedInArgument4 = TestRandom.Int();
      int passedInArgument5 = TestRandom.Int();
      //
      _methodCaller.CallAction(action, passedInArgument1, passedInArgument2, passedInArgument3, passedInArgument4, passedInArgument5);
      //
      Assert.AreEqual(1, numberOfCalls);
      Assert.AreEqual(passedInArgument1, receivedArgument1);
      Assert.AreEqual(passedInArgument2, receivedArgument2);
      Assert.AreEqual(passedInArgument3, receivedArgument3);
      Assert.AreEqual(passedInArgument4, receivedArgument4);
      Assert.AreEqual(passedInArgument5, receivedArgument5);
   }

   [Test]
   public void CallAction_6Args_CallsActionOnce()
   {
      int numberOfCalls = 0;
      int receivedArgument1 = 0;
      int receivedArgument2 = 0;
      int receivedArgument3 = 0;
      int receivedArgument4 = 0;
      int receivedArgument5 = 0;
      int receivedArgument6 = 0;
      void action(int argument1, int argument2, int argument3, int argument4, int argument5, int argument6)
      {
         ++numberOfCalls;
         receivedArgument1 = argument1;
         receivedArgument2 = argument2;
         receivedArgument3 = argument3;
         receivedArgument4 = argument4;
         receivedArgument5 = argument5;
         receivedArgument6 = argument6;
      }
      int passedInArgument1 = TestRandom.Int();
      int passedInArgument2 = TestRandom.Int();
      int passedInArgument3 = TestRandom.Int();
      int passedInArgument4 = TestRandom.Int();
      int passedInArgument5 = TestRandom.Int();
      int passedInArgument6 = TestRandom.Int();
      //
      _methodCaller.CallAction(action, passedInArgument1, passedInArgument2, passedInArgument3, passedInArgument4, passedInArgument5, passedInArgument6);
      //
      Assert.AreEqual(1, numberOfCalls);
      Assert.AreEqual(passedInArgument1, receivedArgument1);
      Assert.AreEqual(passedInArgument2, receivedArgument2);
      Assert.AreEqual(passedInArgument3, receivedArgument3);
      Assert.AreEqual(passedInArgument4, receivedArgument4);
      Assert.AreEqual(passedInArgument5, receivedArgument5);
      Assert.AreEqual(passedInArgument6, receivedArgument6);
   }

   [Test]
   public void CallAction_7Args_CallsActionOnce()
   {
      int numberOfCalls = 0;
      int receivedArgument1 = 0;
      int receivedArgument2 = 0;
      int receivedArgument3 = 0;
      int receivedArgument4 = 0;
      int receivedArgument5 = 0;
      int receivedArgument6 = 0;
      int receivedArgument7 = 0;
      void action(int argument1, int argument2, int argument3, int argument4, int argument5, int argument6, int argument7)
      {
         ++numberOfCalls;
         receivedArgument1 = argument1;
         receivedArgument2 = argument2;
         receivedArgument3 = argument3;
         receivedArgument4 = argument4;
         receivedArgument5 = argument5;
         receivedArgument6 = argument6;
         receivedArgument7 = argument7;
      }
      int passedInArgument1 = TestRandom.Int();
      int passedInArgument2 = TestRandom.Int();
      int passedInArgument3 = TestRandom.Int();
      int passedInArgument4 = TestRandom.Int();
      int passedInArgument5 = TestRandom.Int();
      int passedInArgument6 = TestRandom.Int();
      int passedInArgument7 = TestRandom.Int();
      //
      _methodCaller.CallAction(action, passedInArgument1, passedInArgument2, passedInArgument3, passedInArgument4, passedInArgument5, passedInArgument6, passedInArgument7);
      //
      Assert.AreEqual(1, numberOfCalls);
      Assert.AreEqual(passedInArgument1, receivedArgument1);
      Assert.AreEqual(passedInArgument2, receivedArgument2);
      Assert.AreEqual(passedInArgument3, receivedArgument3);
      Assert.AreEqual(passedInArgument4, receivedArgument4);
      Assert.AreEqual(passedInArgument5, receivedArgument5);
      Assert.AreEqual(passedInArgument6, receivedArgument6);
      Assert.AreEqual(passedInArgument7, receivedArgument7);
   }

   [Test]
   public void CallAction_8Args_CallsActionOnce()
   {
      int numberOfCalls = 0;
      int receivedArgument1 = 0;
      int receivedArgument2 = 0;
      int receivedArgument3 = 0;
      int receivedArgument4 = 0;
      int receivedArgument5 = 0;
      int receivedArgument6 = 0;
      int receivedArgument7 = 0;
      int receivedArgument8 = 0;
      void action(int argument1, int argument2, int argument3, int argument4, int argument5, int argument6, int argument7, int argument8)
      {
         ++numberOfCalls;
         receivedArgument1 = argument1;
         receivedArgument2 = argument2;
         receivedArgument3 = argument3;
         receivedArgument4 = argument4;
         receivedArgument5 = argument5;
         receivedArgument6 = argument6;
         receivedArgument7 = argument7;
         receivedArgument8 = argument8;
      }
      int passedInArgument1 = TestRandom.Int();
      int passedInArgument2 = TestRandom.Int();
      int passedInArgument3 = TestRandom.Int();
      int passedInArgument4 = TestRandom.Int();
      int passedInArgument5 = TestRandom.Int();
      int passedInArgument6 = TestRandom.Int();
      int passedInArgument7 = TestRandom.Int();
      int passedInArgument8 = TestRandom.Int();
      //
      _methodCaller.CallAction(action, passedInArgument1, passedInArgument2, passedInArgument3, passedInArgument4, passedInArgument5, passedInArgument6, passedInArgument7, passedInArgument8);
      //
      Assert.AreEqual(1, numberOfCalls);
      Assert.AreEqual(passedInArgument1, receivedArgument1);
      Assert.AreEqual(passedInArgument2, receivedArgument2);
      Assert.AreEqual(passedInArgument3, receivedArgument3);
      Assert.AreEqual(passedInArgument4, receivedArgument4);
      Assert.AreEqual(passedInArgument5, receivedArgument5);
      Assert.AreEqual(passedInArgument6, receivedArgument6);
      Assert.AreEqual(passedInArgument7, receivedArgument7);
      Assert.AreEqual(passedInArgument8, receivedArgument8);
   }

   [Test]
   public void CallAction_9Args_CallsActionOnce()
   {
      int numberOfCalls = 0;
      int receivedArgument1 = 0;
      int receivedArgument2 = 0;
      int receivedArgument3 = 0;
      int receivedArgument4 = 0;
      int receivedArgument5 = 0;
      int receivedArgument6 = 0;
      int receivedArgument7 = 0;
      int receivedArgument8 = 0;
      int receivedArgument9 = 0;
      void action(int argument1, int argument2, int argument3, int argument4, int argument5, int argument6, int argument7, int argument8, int argument9)
      {
         ++numberOfCalls;
         receivedArgument1 = argument1;
         receivedArgument2 = argument2;
         receivedArgument3 = argument3;
         receivedArgument4 = argument4;
         receivedArgument5 = argument5;
         receivedArgument6 = argument6;
         receivedArgument7 = argument7;
         receivedArgument8 = argument8;
         receivedArgument9 = argument9;
      }
      int passedInArgument1 = TestRandom.Int();
      int passedInArgument2 = TestRandom.Int();
      int passedInArgument3 = TestRandom.Int();
      int passedInArgument4 = TestRandom.Int();
      int passedInArgument5 = TestRandom.Int();
      int passedInArgument6 = TestRandom.Int();
      int passedInArgument7 = TestRandom.Int();
      int passedInArgument8 = TestRandom.Int();
      int passedInArgument9 = TestRandom.Int();
      //
      _methodCaller.CallAction(action, passedInArgument1, passedInArgument2, passedInArgument3, passedInArgument4, passedInArgument5, passedInArgument6, passedInArgument7, passedInArgument8, passedInArgument9);
      //
      Assert.AreEqual(1, numberOfCalls);
      Assert.AreEqual(passedInArgument1, receivedArgument1);
      Assert.AreEqual(passedInArgument2, receivedArgument2);
      Assert.AreEqual(passedInArgument3, receivedArgument3);
      Assert.AreEqual(passedInArgument4, receivedArgument4);
      Assert.AreEqual(passedInArgument5, receivedArgument5);
      Assert.AreEqual(passedInArgument6, receivedArgument6);
      Assert.AreEqual(passedInArgument7, receivedArgument7);
      Assert.AreEqual(passedInArgument8, receivedArgument8);
      Assert.AreEqual(passedInArgument9, receivedArgument9);
   }

   [Test]
   public void CallAction_10Args_CallsActionOnce()
   {
      int numberOfCalls = 0;
      int receivedArgument1 = 0;
      int receivedArgument2 = 0;
      int receivedArgument3 = 0;
      int receivedArgument4 = 0;
      int receivedArgument5 = 0;
      int receivedArgument6 = 0;
      int receivedArgument7 = 0;
      int receivedArgument8 = 0;
      int receivedArgument9 = 0;
      int receivedArgument10 = 0;
      void action(int argument1, int argument2, int argument3, int argument4, int argument5, int argument6, int argument7, int argument8, int argument9, int argument10)
      {
         ++numberOfCalls;
         receivedArgument1 = argument1;
         receivedArgument2 = argument2;
         receivedArgument3 = argument3;
         receivedArgument4 = argument4;
         receivedArgument5 = argument5;
         receivedArgument6 = argument6;
         receivedArgument7 = argument7;
         receivedArgument8 = argument8;
         receivedArgument9 = argument9;
         receivedArgument10 = argument10;
      }
      int passedInArgument1 = TestRandom.Int();
      int passedInArgument2 = TestRandom.Int();
      int passedInArgument3 = TestRandom.Int();
      int passedInArgument4 = TestRandom.Int();
      int passedInArgument5 = TestRandom.Int();
      int passedInArgument6 = TestRandom.Int();
      int passedInArgument7 = TestRandom.Int();
      int passedInArgument8 = TestRandom.Int();
      int passedInArgument9 = TestRandom.Int();
      int passedInArgument10 = TestRandom.Int();
      //
      _methodCaller.CallAction(action, passedInArgument1, passedInArgument2, passedInArgument3, passedInArgument4, passedInArgument5, passedInArgument6, passedInArgument7, passedInArgument8, passedInArgument9, passedInArgument10);
      //
      Assert.AreEqual(1, numberOfCalls);
      Assert.AreEqual(passedInArgument1, receivedArgument1);
      Assert.AreEqual(passedInArgument2, receivedArgument2);
      Assert.AreEqual(passedInArgument3, receivedArgument3);
      Assert.AreEqual(passedInArgument4, receivedArgument4);
      Assert.AreEqual(passedInArgument5, receivedArgument5);
      Assert.AreEqual(passedInArgument6, receivedArgument6);
      Assert.AreEqual(passedInArgument7, receivedArgument7);
      Assert.AreEqual(passedInArgument8, receivedArgument8);
      Assert.AreEqual(passedInArgument9, receivedArgument9);
      Assert.AreEqual(passedInArgument10, receivedArgument10);
   }

   [Test]
   public void CallAction_11Args_CallsActionOnce()
   {
      int numberOfCalls = 0;
      int receivedArgument1 = 0;
      int receivedArgument2 = 0;
      int receivedArgument3 = 0;
      int receivedArgument4 = 0;
      int receivedArgument5 = 0;
      int receivedArgument6 = 0;
      int receivedArgument7 = 0;
      int receivedArgument8 = 0;
      int receivedArgument9 = 0;
      int receivedArgument10 = 0;
      int receivedArgument11 = 0;
      void action(int argument1, int argument2, int argument3, int argument4, int argument5, int argument6, int argument7, int argument8, int argument9, int argument10, int argument11)
      {
         ++numberOfCalls;
         receivedArgument1 = argument1;
         receivedArgument2 = argument2;
         receivedArgument3 = argument3;
         receivedArgument4 = argument4;
         receivedArgument5 = argument5;
         receivedArgument6 = argument6;
         receivedArgument7 = argument7;
         receivedArgument8 = argument8;
         receivedArgument9 = argument9;
         receivedArgument10 = argument10;
         receivedArgument11 = argument11;
      }
      int passedInArgument1 = TestRandom.Int();
      int passedInArgument2 = TestRandom.Int();
      int passedInArgument3 = TestRandom.Int();
      int passedInArgument4 = TestRandom.Int();
      int passedInArgument5 = TestRandom.Int();
      int passedInArgument6 = TestRandom.Int();
      int passedInArgument7 = TestRandom.Int();
      int passedInArgument8 = TestRandom.Int();
      int passedInArgument9 = TestRandom.Int();
      int passedInArgument10 = TestRandom.Int();
      int passedInArgument11 = TestRandom.Int();
      //
      _methodCaller.CallAction(
         action,
         passedInArgument1,
         passedInArgument2,
         passedInArgument3,
         passedInArgument4,
         passedInArgument5,
         passedInArgument6,
         passedInArgument7,
         passedInArgument8,
         passedInArgument9,
         passedInArgument10,
         passedInArgument11);
      //
      Assert.AreEqual(1, numberOfCalls);
      Assert.AreEqual(passedInArgument1, receivedArgument1);
      Assert.AreEqual(passedInArgument2, receivedArgument2);
      Assert.AreEqual(passedInArgument3, receivedArgument3);
      Assert.AreEqual(passedInArgument4, receivedArgument4);
      Assert.AreEqual(passedInArgument5, receivedArgument5);
      Assert.AreEqual(passedInArgument6, receivedArgument6);
      Assert.AreEqual(passedInArgument7, receivedArgument7);
      Assert.AreEqual(passedInArgument8, receivedArgument8);
      Assert.AreEqual(passedInArgument9, receivedArgument9);
      Assert.AreEqual(passedInArgument10, receivedArgument10);
      Assert.AreEqual(passedInArgument11, receivedArgument11);
   }

   [Test]
   public void CallAction_12Args_CallsActionOnce()
   {
      int numberOfCalls = 0;
      int receivedArgument1 = 0;
      int receivedArgument2 = 0;
      int receivedArgument3 = 0;
      int receivedArgument4 = 0;
      int receivedArgument5 = 0;
      int receivedArgument6 = 0;
      int receivedArgument7 = 0;
      int receivedArgument8 = 0;
      int receivedArgument9 = 0;
      int receivedArgument10 = 0;
      int receivedArgument11 = 0;
      int receivedArgument12 = 0;
      void action(int argument1, int argument2, int argument3, int argument4, int argument5, int argument6, int argument7, int argument8, int argument9, int argument10, int argument11, int argument12)
      {
         ++numberOfCalls;
         receivedArgument1 = argument1;
         receivedArgument2 = argument2;
         receivedArgument3 = argument3;
         receivedArgument4 = argument4;
         receivedArgument5 = argument5;
         receivedArgument6 = argument6;
         receivedArgument7 = argument7;
         receivedArgument8 = argument8;
         receivedArgument9 = argument9;
         receivedArgument10 = argument10;
         receivedArgument11 = argument11;
         receivedArgument12 = argument12;
      }
      int passedInArgument1 = TestRandom.Int();
      int passedInArgument2 = TestRandom.Int();
      int passedInArgument3 = TestRandom.Int();
      int passedInArgument4 = TestRandom.Int();
      int passedInArgument5 = TestRandom.Int();
      int passedInArgument6 = TestRandom.Int();
      int passedInArgument7 = TestRandom.Int();
      int passedInArgument8 = TestRandom.Int();
      int passedInArgument9 = TestRandom.Int();
      int passedInArgument10 = TestRandom.Int();
      int passedInArgument11 = TestRandom.Int();
      int passedInArgument12 = TestRandom.Int();
      //
      _methodCaller.CallAction(
         action,
         passedInArgument1,
         passedInArgument2,
         passedInArgument3,
         passedInArgument4,
         passedInArgument5,
         passedInArgument6,
         passedInArgument7,
         passedInArgument8,
         passedInArgument9,
         passedInArgument10,
         passedInArgument11,
         passedInArgument12);
      //
      Assert.AreEqual(1, numberOfCalls);
      Assert.AreEqual(passedInArgument1, receivedArgument1);
      Assert.AreEqual(passedInArgument2, receivedArgument2);
      Assert.AreEqual(passedInArgument3, receivedArgument3);
      Assert.AreEqual(passedInArgument4, receivedArgument4);
      Assert.AreEqual(passedInArgument5, receivedArgument5);
      Assert.AreEqual(passedInArgument6, receivedArgument6);
      Assert.AreEqual(passedInArgument7, receivedArgument7);
      Assert.AreEqual(passedInArgument8, receivedArgument8);
      Assert.AreEqual(passedInArgument9, receivedArgument9);
      Assert.AreEqual(passedInArgument10, receivedArgument10);
      Assert.AreEqual(passedInArgument11, receivedArgument11);
      Assert.AreEqual(passedInArgument12, receivedArgument12);
   }


   [Test]
   public void CallFunction_0Args_CallsFunctionOnce_ReturnsReturnValue()
   {
      int numberOfCalls = 0;
      int returnValue = TestRandom.Int();
      int function()
      {
         ++numberOfCalls;
         return returnValue;
      }
      //
      int returnedReturnValue = _methodCaller.CallFunction(function);
      //
      Assert.AreEqual(1, numberOfCalls);
      Assert.AreEqual(returnValue, returnedReturnValue);
   }

   [Test]
   public void CallFunction_1Arg_CallsFunctionOnce_ReturnsReturnValue()
   {
      int numberOfCalls = 0;
      int receivedArgument = 0;
      int returnedReturnValue = TestRandom.Int();
      int method(int argument)
      {
         ++numberOfCalls;
         receivedArgument = argument;
         return returnedReturnValue;
      }
      int passedInArgument = TestRandom.Int();
      //
      int returnValue = _methodCaller.CallFunction(method, passedInArgument);
      //
      Assert.AreEqual(1, numberOfCalls);
      Assert.AreEqual(passedInArgument, receivedArgument);
      Assert.AreEqual(returnedReturnValue, returnValue);
   }

   [Test]
   public void CallFunction_2Args_CallsFunctionOnce_ReturnsReturnValue()
   {
      int numberOfCalls = 0;
      int receivedArgument1 = 0;
      int receivedArgument2 = 0;
      int returnedReturnValue = TestRandom.Int();
      int method(int argument1, int argument2)
      {
         ++numberOfCalls;
         receivedArgument1 = argument1;
         receivedArgument2 = argument2;
         return returnedReturnValue;
      }
      int passedInArgument1 = TestRandom.Int();
      int passedInArgument2 = TestRandom.Int();
      //
      int returnValue = _methodCaller.CallFunction(method, passedInArgument1, passedInArgument2);
      //
      Assert.AreEqual(1, numberOfCalls);
      Assert.AreEqual(passedInArgument1, receivedArgument1);
      Assert.AreEqual(passedInArgument2, receivedArgument2);
      Assert.AreEqual(returnedReturnValue, returnValue);
   }

   [Test]
   public void CallFunction_3Args_CallsFunctionOnce_ReturnsReturnValue()
   {
      int numberOfCalls = 0;
      int receivedArgument1 = 0;
      int receivedArgument2 = 0;
      int receivedArgument3 = 0;
      int returnedReturnValue = TestRandom.Int();
      int method(int argument1, int argument2, int argument3)
      {
         ++numberOfCalls;
         receivedArgument1 = argument1;
         receivedArgument2 = argument2;
         receivedArgument3 = argument3;
         return returnedReturnValue;
      }
      int passedInArgument1 = TestRandom.Int();
      int passedInArgument2 = TestRandom.Int();
      int passedInArgument3 = TestRandom.Int();
      //
      int returnValue = _methodCaller.CallFunction(
          method, passedInArgument1, passedInArgument2, passedInArgument3);
      //
      Assert.AreEqual(1, numberOfCalls);
      Assert.AreEqual(passedInArgument1, receivedArgument1);
      Assert.AreEqual(passedInArgument2, receivedArgument2);
      Assert.AreEqual(passedInArgument3, receivedArgument3);
      Assert.AreEqual(returnedReturnValue, returnValue);
   }

   [Test]
   public void CallFunction_4Args_CallsFunctionOnce_ReturnsReturnValue()
   {
      int numberOfCalls = 0;
      int receivedArgument1 = 0;
      int receivedArgument2 = 0;
      int receivedArgument3 = 0;
      int receivedArgument4 = 0;
      int returnedReturnValue = TestRandom.Int();
      int method(int argument1, int argument2, int argument3, int argument4)
      {
         ++numberOfCalls;
         receivedArgument1 = argument1;
         receivedArgument2 = argument2;
         receivedArgument3 = argument3;
         receivedArgument4 = argument4;
         return returnedReturnValue;
      }
      int passedInArgument1 = TestRandom.Int();
      int passedInArgument2 = TestRandom.Int();
      int passedInArgument3 = TestRandom.Int();
      int passedInArgument4 = TestRandom.Int();
      //
      int returnValue = _methodCaller.CallFunction(
          method, passedInArgument1, passedInArgument2, passedInArgument3, passedInArgument4);
      //
      Assert.AreEqual(1, numberOfCalls);
      Assert.AreEqual(passedInArgument1, receivedArgument1);
      Assert.AreEqual(passedInArgument2, receivedArgument2);
      Assert.AreEqual(passedInArgument3, receivedArgument3);
      Assert.AreEqual(passedInArgument4, receivedArgument4);
      Assert.AreEqual(returnedReturnValue, returnValue);
   }

   [Test]
   public void CallFunction_5Args_CallsFunctionOnce_ReturnsReturnValue()
   {
      int numberOfCalls = 0;
      int receivedArgument1 = 0;
      int receivedArgument2 = 0;
      int receivedArgument3 = 0;
      int receivedArgument4 = 0;
      int receivedArgument5 = 0;
      int returnedReturnValue = TestRandom.Int();
      int method(int argument1, int argument2, int argument3, int argument4, int argument5)
      {
         ++numberOfCalls;
         receivedArgument1 = argument1;
         receivedArgument2 = argument2;
         receivedArgument3 = argument3;
         receivedArgument4 = argument4;
         receivedArgument5 = argument5;
         return returnedReturnValue;
      }
      int passedInArgument1 = TestRandom.Int();
      int passedInArgument2 = TestRandom.Int();
      int passedInArgument3 = TestRandom.Int();
      int passedInArgument4 = TestRandom.Int();
      int passedInArgument5 = TestRandom.Int();
      //
      int returnValue = _methodCaller.CallFunction(
          method, passedInArgument1, passedInArgument2, passedInArgument3, passedInArgument4, passedInArgument5);
      //
      Assert.AreEqual(1, numberOfCalls);
      Assert.AreEqual(passedInArgument1, receivedArgument1);
      Assert.AreEqual(passedInArgument2, receivedArgument2);
      Assert.AreEqual(passedInArgument3, receivedArgument3);
      Assert.AreEqual(passedInArgument4, receivedArgument4);
      Assert.AreEqual(passedInArgument5, receivedArgument5);
      Assert.AreEqual(returnedReturnValue, returnValue);
   }

   [Test]
   public void CallFunction_6Args_CallsFunctionOnce_ReturnsReturnValue()
   {
      int numberOfCalls = 0;
      int receivedArgument1 = 0;
      int receivedArgument2 = 0;
      int receivedArgument3 = 0;
      int receivedArgument4 = 0;
      int receivedArgument5 = 0;
      int receivedArgument6 = 0;
      int returnedReturnValue = TestRandom.Int();
      int method(int argument1, int argument2, int argument3, int argument4, int argument5, int argument6)
      {
         ++numberOfCalls;
         receivedArgument1 = argument1;
         receivedArgument2 = argument2;
         receivedArgument3 = argument3;
         receivedArgument4 = argument4;
         receivedArgument5 = argument5;
         receivedArgument6 = argument6;
         return returnedReturnValue;
      }
      int passedInArgument1 = TestRandom.Int();
      int passedInArgument2 = TestRandom.Int();
      int passedInArgument3 = TestRandom.Int();
      int passedInArgument4 = TestRandom.Int();
      int passedInArgument5 = TestRandom.Int();
      int passedInArgument6 = TestRandom.Int();
      //
      int returnValue = _methodCaller.CallFunction(
          method, passedInArgument1, passedInArgument2, passedInArgument3, passedInArgument4, passedInArgument5, passedInArgument6);
      //
      Assert.AreEqual(1, numberOfCalls);
      Assert.AreEqual(passedInArgument1, receivedArgument1);
      Assert.AreEqual(passedInArgument2, receivedArgument2);
      Assert.AreEqual(passedInArgument3, receivedArgument3);
      Assert.AreEqual(passedInArgument4, receivedArgument4);
      Assert.AreEqual(passedInArgument5, receivedArgument5);
      Assert.AreEqual(passedInArgument6, receivedArgument6);
      Assert.AreEqual(returnedReturnValue, returnValue);
   }

   [Test]
   public void CallFunction_7Args_CallsFunctionOnce_ReturnsReturnValue()
   {
      int numberOfCalls = 0;
      int receivedArgument1 = 0;
      int receivedArgument2 = 0;
      int receivedArgument3 = 0;
      int receivedArgument4 = 0;
      int receivedArgument5 = 0;
      int receivedArgument6 = 0;
      int receivedArgument7 = 0;
      int returnedReturnValue = TestRandom.Int();
      int method(int argument1, int argument2, int argument3, int argument4, int argument5, int argument6, int argument7)
      {
         ++numberOfCalls;
         receivedArgument1 = argument1;
         receivedArgument2 = argument2;
         receivedArgument3 = argument3;
         receivedArgument4 = argument4;
         receivedArgument5 = argument5;
         receivedArgument6 = argument6;
         receivedArgument7 = argument7;
         return returnedReturnValue;
      }
      int passedInArgument1 = TestRandom.Int();
      int passedInArgument2 = TestRandom.Int();
      int passedInArgument3 = TestRandom.Int();
      int passedInArgument4 = TestRandom.Int();
      int passedInArgument5 = TestRandom.Int();
      int passedInArgument6 = TestRandom.Int();
      int passedInArgument7 = TestRandom.Int();
      //
      int returnValue = _methodCaller.CallFunction(
          method, passedInArgument1, passedInArgument2, passedInArgument3, passedInArgument4, passedInArgument5, passedInArgument6, passedInArgument7);
      //
      Assert.AreEqual(1, numberOfCalls);
      Assert.AreEqual(passedInArgument1, receivedArgument1);
      Assert.AreEqual(passedInArgument2, receivedArgument2);
      Assert.AreEqual(passedInArgument3, receivedArgument3);
      Assert.AreEqual(passedInArgument4, receivedArgument4);
      Assert.AreEqual(passedInArgument5, receivedArgument5);
      Assert.AreEqual(passedInArgument6, receivedArgument6);
      Assert.AreEqual(passedInArgument7, receivedArgument7);
      Assert.AreEqual(returnedReturnValue, returnValue);
   }

   [Test]
   public void CallFunction_8Args_CallsFunctionOnce_ReturnsReturnValue()
   {
      int numberOfCalls = 0;
      int receivedArgument1 = 0;
      int receivedArgument2 = 0;
      int receivedArgument3 = 0;
      int receivedArgument4 = 0;
      int receivedArgument5 = 0;
      int receivedArgument6 = 0;
      int receivedArgument7 = 0;
      int receivedArgument8 = 0;
      int returnedReturnValue = TestRandom.Int();
      int method(int argument1, int argument2, int argument3, int argument4, int argument5, int argument6, int argument7, int argument8)
      {
         ++numberOfCalls;
         receivedArgument1 = argument1;
         receivedArgument2 = argument2;
         receivedArgument3 = argument3;
         receivedArgument4 = argument4;
         receivedArgument5 = argument5;
         receivedArgument6 = argument6;
         receivedArgument7 = argument7;
         receivedArgument8 = argument8;
         return returnedReturnValue;
      }
      int passedInArgument1 = TestRandom.Int();
      int passedInArgument2 = TestRandom.Int();
      int passedInArgument3 = TestRandom.Int();
      int passedInArgument4 = TestRandom.Int();
      int passedInArgument5 = TestRandom.Int();
      int passedInArgument6 = TestRandom.Int();
      int passedInArgument7 = TestRandom.Int();
      int passedInArgument8 = TestRandom.Int();
      //
      int returnValue = _methodCaller.CallFunction(
          method, passedInArgument1, passedInArgument2, passedInArgument3, passedInArgument4, passedInArgument5, passedInArgument6, passedInArgument7, passedInArgument8);
      //
      Assert.AreEqual(1, numberOfCalls);
      Assert.AreEqual(passedInArgument1, receivedArgument1);
      Assert.AreEqual(passedInArgument2, receivedArgument2);
      Assert.AreEqual(passedInArgument3, receivedArgument3);
      Assert.AreEqual(passedInArgument4, receivedArgument4);
      Assert.AreEqual(passedInArgument5, receivedArgument5);
      Assert.AreEqual(passedInArgument6, receivedArgument6);
      Assert.AreEqual(passedInArgument7, receivedArgument7);
      Assert.AreEqual(passedInArgument8, receivedArgument8);
      Assert.AreEqual(returnedReturnValue, returnValue);
   }

   [Test]
   public void CallFunction_9Args_CallsFunctionOnce_ReturnsReturnValue()
   {
      int numberOfCalls = 0;
      int receivedArgument1 = 0;
      int receivedArgument2 = 0;
      int receivedArgument3 = 0;
      int receivedArgument4 = 0;
      int receivedArgument5 = 0;
      int receivedArgument6 = 0;
      int receivedArgument7 = 0;
      int receivedArgument8 = 0;
      int receivedArgument9 = 0;
      int returnedReturnValue = TestRandom.Int();
      int method(int argument1, int argument2, int argument3, int argument4, int argument5, int argument6, int argument7, int argument8, int argument9)
      {
         ++numberOfCalls;
         receivedArgument1 = argument1;
         receivedArgument2 = argument2;
         receivedArgument3 = argument3;
         receivedArgument4 = argument4;
         receivedArgument5 = argument5;
         receivedArgument6 = argument6;
         receivedArgument7 = argument7;
         receivedArgument8 = argument8;
         receivedArgument9 = argument9;
         return returnedReturnValue;
      }
      int passedInArgument1 = TestRandom.Int();
      int passedInArgument2 = TestRandom.Int();
      int passedInArgument3 = TestRandom.Int();
      int passedInArgument4 = TestRandom.Int();
      int passedInArgument5 = TestRandom.Int();
      int passedInArgument6 = TestRandom.Int();
      int passedInArgument7 = TestRandom.Int();
      int passedInArgument8 = TestRandom.Int();
      int passedInArgument9 = TestRandom.Int();
      //
      int returnValue = _methodCaller.CallFunction(
          method, passedInArgument1, passedInArgument2, passedInArgument3, passedInArgument4, passedInArgument5, passedInArgument6, passedInArgument7, passedInArgument8, passedInArgument9);
      //
      Assert.AreEqual(1, numberOfCalls);
      Assert.AreEqual(passedInArgument1, receivedArgument1);
      Assert.AreEqual(passedInArgument2, receivedArgument2);
      Assert.AreEqual(passedInArgument3, receivedArgument3);
      Assert.AreEqual(passedInArgument4, receivedArgument4);
      Assert.AreEqual(passedInArgument5, receivedArgument5);
      Assert.AreEqual(passedInArgument6, receivedArgument6);
      Assert.AreEqual(passedInArgument7, receivedArgument7);
      Assert.AreEqual(passedInArgument8, receivedArgument8);
      Assert.AreEqual(passedInArgument9, receivedArgument9);
      Assert.AreEqual(returnedReturnValue, returnValue);
   }

   [Test]
   public void CallFunction_10Args_CallsFunctionOnce_ReturnsReturnValue()
   {
      int numberOfCalls = 0;
      int receivedArgument1 = 0;
      int receivedArgument2 = 0;
      int receivedArgument3 = 0;
      int receivedArgument4 = 0;
      int receivedArgument5 = 0;
      int receivedArgument6 = 0;
      int receivedArgument7 = 0;
      int receivedArgument8 = 0;
      int receivedArgument9 = 0;
      int receivedArgument10 = 0;
      int returnedReturnValue = TestRandom.Int();
      int method(int argument1, int argument2, int argument3, int argument4, int argument5, int argument6, int argument7, int argument8, int argument9, int argument10)
      {
         ++numberOfCalls;
         receivedArgument1 = argument1;
         receivedArgument2 = argument2;
         receivedArgument3 = argument3;
         receivedArgument4 = argument4;
         receivedArgument5 = argument5;
         receivedArgument6 = argument6;
         receivedArgument7 = argument7;
         receivedArgument8 = argument8;
         receivedArgument9 = argument9;
         receivedArgument10 = argument10;
         return returnedReturnValue;
      }
      int passedInArgument1 = TestRandom.Int();
      int passedInArgument2 = TestRandom.Int();
      int passedInArgument3 = TestRandom.Int();
      int passedInArgument4 = TestRandom.Int();
      int passedInArgument5 = TestRandom.Int();
      int passedInArgument6 = TestRandom.Int();
      int passedInArgument7 = TestRandom.Int();
      int passedInArgument8 = TestRandom.Int();
      int passedInArgument9 = TestRandom.Int();
      int passedInArgument10 = TestRandom.Int();
      //
      int returnValue = _methodCaller.CallFunction(
          method, passedInArgument1, passedInArgument2, passedInArgument3, passedInArgument4, passedInArgument5, passedInArgument6, passedInArgument7, passedInArgument8, passedInArgument9, passedInArgument10);
      //
      Assert.AreEqual(1, numberOfCalls);
      Assert.AreEqual(passedInArgument1, receivedArgument1);
      Assert.AreEqual(passedInArgument2, receivedArgument2);
      Assert.AreEqual(passedInArgument3, receivedArgument3);
      Assert.AreEqual(passedInArgument4, receivedArgument4);
      Assert.AreEqual(passedInArgument5, receivedArgument5);
      Assert.AreEqual(passedInArgument6, receivedArgument6);
      Assert.AreEqual(passedInArgument7, receivedArgument7);
      Assert.AreEqual(passedInArgument8, receivedArgument8);
      Assert.AreEqual(passedInArgument9, receivedArgument9);
      Assert.AreEqual(passedInArgument10, receivedArgument10);
      Assert.AreEqual(returnedReturnValue, returnValue);
   }

   [Test]
   public void CallFunction_11Args_CallsFunctionOnce_ReturnsReturnValue()
   {
      int numberOfCalls = 0;
      int receivedArgument1 = 0;
      int receivedArgument2 = 0;
      int receivedArgument3 = 0;
      int receivedArgument4 = 0;
      int receivedArgument5 = 0;
      int receivedArgument6 = 0;
      int receivedArgument7 = 0;
      int receivedArgument8 = 0;
      int receivedArgument9 = 0;
      int receivedArgument10 = 0;
      int receivedArgument11 = 0;
      int returnedReturnValue = TestRandom.Int();
      int method(int argument1, int argument2, int argument3, int argument4, int argument5, int argument6, int argument7, int argument8, int argument9, int argument10, int argument11)
      {
         ++numberOfCalls;
         receivedArgument1 = argument1;
         receivedArgument2 = argument2;
         receivedArgument3 = argument3;
         receivedArgument4 = argument4;
         receivedArgument5 = argument5;
         receivedArgument6 = argument6;
         receivedArgument7 = argument7;
         receivedArgument8 = argument8;
         receivedArgument9 = argument9;
         receivedArgument10 = argument10;
         receivedArgument11 = argument11;
         return returnedReturnValue;
      }
      int passedInArgument1 = TestRandom.Int();
      int passedInArgument2 = TestRandom.Int();
      int passedInArgument3 = TestRandom.Int();
      int passedInArgument4 = TestRandom.Int();
      int passedInArgument5 = TestRandom.Int();
      int passedInArgument6 = TestRandom.Int();
      int passedInArgument7 = TestRandom.Int();
      int passedInArgument8 = TestRandom.Int();
      int passedInArgument9 = TestRandom.Int();
      int passedInArgument10 = TestRandom.Int();
      int passedInArgument11 = TestRandom.Int();
      //
      int returnValue = _methodCaller.CallFunction(
          method, passedInArgument1, passedInArgument2, passedInArgument3, passedInArgument4, passedInArgument5, passedInArgument6, passedInArgument7, passedInArgument8, passedInArgument9, passedInArgument10, passedInArgument11);
      //
      Assert.AreEqual(1, numberOfCalls);
      Assert.AreEqual(passedInArgument1, receivedArgument1);
      Assert.AreEqual(passedInArgument2, receivedArgument2);
      Assert.AreEqual(passedInArgument3, receivedArgument3);
      Assert.AreEqual(passedInArgument4, receivedArgument4);
      Assert.AreEqual(passedInArgument5, receivedArgument5);
      Assert.AreEqual(passedInArgument6, receivedArgument6);
      Assert.AreEqual(passedInArgument7, receivedArgument7);
      Assert.AreEqual(passedInArgument8, receivedArgument8);
      Assert.AreEqual(passedInArgument9, receivedArgument9);
      Assert.AreEqual(passedInArgument10, receivedArgument10);
      Assert.AreEqual(passedInArgument11, receivedArgument11);
      Assert.AreEqual(returnedReturnValue, returnValue);
   }

   [Test]
   public void CallFunction_12Args_CallsFunctionOnce_ReturnsReturnValue()
   {
      int numberOfCalls = 0;
      int receivedArgument1 = 0;
      int receivedArgument2 = 0;
      int receivedArgument3 = 0;
      int receivedArgument4 = 0;
      int receivedArgument5 = 0;
      int receivedArgument6 = 0;
      int receivedArgument7 = 0;
      int receivedArgument8 = 0;
      int receivedArgument9 = 0;
      int receivedArgument10 = 0;
      int receivedArgument11 = 0;
      int receivedArgument12 = 0;
      int returnedReturnValue = TestRandom.Int();
      int method(int argument1, int argument2, int argument3, int argument4, int argument5, int argument6, int argument7, int argument8, int argument9, int argument10, int argument11, int argument12)
      {
         ++numberOfCalls;
         receivedArgument1 = argument1;
         receivedArgument2 = argument2;
         receivedArgument3 = argument3;
         receivedArgument4 = argument4;
         receivedArgument5 = argument5;
         receivedArgument6 = argument6;
         receivedArgument7 = argument7;
         receivedArgument8 = argument8;
         receivedArgument9 = argument9;
         receivedArgument10 = argument10;
         receivedArgument11 = argument11;
         receivedArgument12 = argument12;
         return returnedReturnValue;
      }
      int passedInArgument1 = TestRandom.Int();
      int passedInArgument2 = TestRandom.Int();
      int passedInArgument3 = TestRandom.Int();
      int passedInArgument4 = TestRandom.Int();
      int passedInArgument5 = TestRandom.Int();
      int passedInArgument6 = TestRandom.Int();
      int passedInArgument7 = TestRandom.Int();
      int passedInArgument8 = TestRandom.Int();
      int passedInArgument9 = TestRandom.Int();
      int passedInArgument10 = TestRandom.Int();
      int passedInArgument11 = TestRandom.Int();
      int passedInArgument12 = TestRandom.Int();
      //
      int returnValue = _methodCaller.CallFunction(
          method, passedInArgument1, passedInArgument2, passedInArgument3, passedInArgument4, passedInArgument5, passedInArgument6, passedInArgument7, passedInArgument8, passedInArgument9, passedInArgument10, passedInArgument11, passedInArgument12);
      //
      Assert.AreEqual(1, numberOfCalls);
      Assert.AreEqual(passedInArgument1, receivedArgument1);
      Assert.AreEqual(passedInArgument2, receivedArgument2);
      Assert.AreEqual(passedInArgument3, receivedArgument3);
      Assert.AreEqual(passedInArgument4, receivedArgument4);
      Assert.AreEqual(passedInArgument5, receivedArgument5);
      Assert.AreEqual(passedInArgument6, receivedArgument6);
      Assert.AreEqual(passedInArgument7, receivedArgument7);
      Assert.AreEqual(passedInArgument8, receivedArgument8);
      Assert.AreEqual(passedInArgument9, receivedArgument9);
      Assert.AreEqual(passedInArgument10, receivedArgument10);
      Assert.AreEqual(passedInArgument11, receivedArgument11);
      Assert.AreEqual(passedInArgument12, receivedArgument12);
      Assert.AreEqual(returnedReturnValue, returnValue);
   }
}
