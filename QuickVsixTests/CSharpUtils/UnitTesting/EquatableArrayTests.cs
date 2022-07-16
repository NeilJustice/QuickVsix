using CSharpUtils;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

[TestFixture]
public static class EquatableArrayTests
{
   [Test]
   public static void Equals_ActualObjectNotAnEquatableArrayOfT_ThrowsInvalidCastException()
   {
      Assert2.Throws<InvalidCastException>(() =>
      new EquatableArray<int>(Array.Empty<int>()).Equals(new EquatableArray<decimal>(Array.Empty<decimal>())),
         "EquatableArray<T>.Equals(actualObject) passed a non-EquatableArray<T> non-T[] actualObject.");
   }

   [Test]
   public static void Equals_ElementsNotEqual_ThrowsAssertionException()
   {
      var expectedEquatableArray = new EquatableArray<int>(new int[] { 100 });
      var actualEquatableArray = new EquatableArray<int>(new int[] { 101 });
      //
      Assert2.Throws<AssertionException>(() => expectedEquatableArray.Equals(actualEquatableArray),
         @"  Expected and actual are both <System.Int32[1]>
  Values differ at index [0]
  Expected: 100
  But was:  101
");
   }

   [Test]
   public static void Equals_ActualObjectIsIntArray_ElementsAreEqual_ReturnsTrue()
   {
      int elementA = TestRandom.Int();
      int elementB = TestRandom.Int();
      var expectedElements = new int[] { elementA, elementB };
      var actualElements = new int[] { elementA, elementB };
      var expectedEquatableArray = new EquatableArray<int>(expectedElements);
      //
      bool areEqual = expectedEquatableArray.Equals(actualElements);
      //
      Assert.IsTrue(areEqual);
   }

   [Test]
   public static void Equals_ActualObjectIsStringArray_ElementsAreEqual_ReturnsTrue()
   {
      string elementA = TestRandom.String();
      string elementB = TestRandom.String();
      var expectedElements = new string[] { elementA, elementB };
      var actualElements = new string[] { elementA, elementB };
      var expectedEquatableArray = new EquatableArray<string>(expectedElements);
      //
      bool areEqual = expectedEquatableArray.Equals(actualElements);
      //
      Assert.IsTrue(areEqual);
   }

   [Test]
   public static void Equals_ActualObjectIsEquatableIntArray_ElementsAreEqual_ReturnsTrue()
   {
      int elementA = TestRandom.Int();
      int elementB = TestRandom.Int();
      var expectedElements = new int[] { elementA, elementB };
      var actualElements = new int[] { elementA, elementB };
      var expectedEquatableArray = new EquatableArray<int>(expectedElements);
      var actualEquatableArray = new EquatableArray<int>(actualElements);
      //
      bool areEqual = expectedEquatableArray.Equals(actualEquatableArray);
      //
      Assert.IsTrue(areEqual);
   }

   [Test]
   public static void Equals_ActualObjectIsEquatableStringArray_ElementsAreEqual_ReturnsTrue()
   {
      string elementA = TestRandom.String();
      string elementB = TestRandom.String();
      var expectedElements = new string[] { elementA, elementB };
      var actualElements = new string[] { elementA, elementB };
      var expectedEquatableArray = new EquatableArray<string>(expectedElements);
      var actualEquatableArray = new EquatableArray<string>(actualElements);
      //
      bool areEqual = expectedEquatableArray.Equals(actualEquatableArray);
      //
      Assert.IsTrue(areEqual);
   }

   [Test]
   public static void GetHashCode_ThrowsNotSupportedException()
   {
      Assert2.ThrowsNotSupportedException(() => new EquatableArray<int>(Array.Empty<int>()).GetHashCode());
   }

   [Test]
   public static void GetGenericEnumerable_YieldReturnsEachElement()
   {
      var equatableArray = new EquatableArray<int>(new int[] { 1, 2, 3 });
      //
      int[] genericEnumeratorGeneratedArray = equatableArray.ToArray();
      //
      Assert.AreEqual(new int[] { 1, 2, 3 }, genericEnumeratorGeneratedArray);
   }

   [Test]
   public static void GetNonGenericEnumerator_YieldReturnsEqchElement()
   {
      var equatableArray = new EquatableArray<int>(new int[] { 1, 2, 3 });
      int index = 0;
      foreach (int element in equatableArray)
      {
         if (index == 0)
         {
            Assert.AreEqual(1, element);
         }
         else if (index == 1)
         {
            Assert.AreEqual(2, element);
         }
         else
         {
            Assert.AreEqual(3, element);
         }
         ++index;
      }
   }
}
