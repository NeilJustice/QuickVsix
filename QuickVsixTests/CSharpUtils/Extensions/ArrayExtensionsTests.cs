using CSharpUtils;
using NUnit.Framework;
using System;
using System.Collections.ObjectModel;

[TestFixture]
public static class ArrayExtensionsTests
{
   public static void EmptyAction(string s)
   {
   }

   [Test]
   public static void ForEach_EmptyElements_DoesNotCallAction()
   {
      var calls = new Collection<string>();
      //
      Array.Empty<string>().ForEach(EmptyAction);
      //
      Assert.IsEmpty(calls);
      EmptyAction(""); // 100% code coverage
   }

   [Test]
   public static void ForEach_OneElement_CallsActionOnce()
   {
      var calls = new Collection<string>();
      //
      new string[] { "a" }.ForEach((string s) => { calls.Add(s); });
      //
      Assert.AreEqual(new Collection<string> { "a" }, calls);
   }

   [Test]
   public static void ForEach_TwoElements_CallsActionTwice()
   {
      var calls = new Collection<int>();
      //
      new int[] { 0, 1 }.ForEach((int i) => { calls.Add(i); });
      //
      Assert.AreEqual(new Collection<int> { 0, 1 }, calls);
   }
}
