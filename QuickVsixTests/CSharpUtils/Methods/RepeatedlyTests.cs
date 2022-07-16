using CSharpUtils;
using NUnit.Framework;
using System;

[TestFixture]
public static class RepeatedlyTests
{
   [TestCase(0, 0)]
   [TestCase(1, 1)]
   [TestCase(2, 2)]
   public static void Call_ZeroTimes_CallsActionZeroTimes(int numberOfCalls, int expectedNumberOfActionCalls)
   {
      int numberOfActionCalls = 0;
      void action() => ++numberOfActionCalls;
      //
      Repeatedly.Call(numberOfCalls, action);
      //
      Assert.AreEqual(expectedNumberOfActionCalls, numberOfActionCalls);
   }
}
