using CSharpUtils;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

[TestFixture]
public static class IEnumerableExtensionsTests
{
   [Test]
   public static void ToReadOnlyCollection_ReturnsReadOnlyCollection__IntArrayTestCase()
   {
      int[] ints = new[] { 1, 2, 3 };
      //
      ReadOnlyCollection<int> readonlyInts = ints.ToReadOnlyCollection();
      //
      var expectedReadonlyInts = new ReadOnlyCollection<int>(new[] { 1, 2, 3 });
      Assert.AreEqual(expectedReadonlyInts, readonlyInts);
   }

   [Test]
   public static void ToReadOnlyCollection_ReturnsReadOnlyCollection__StringListTestCase()
   {
      List<string> strings = new List<string>(new string[] { "ab", "cd", "ef" });
      //
      ReadOnlyCollection<string> readonlyStrings = strings.ToReadOnlyCollection();
      //
      var expectedReadonlyStrings = new ReadOnlyCollection<string>(new[] { "ab", "cd", "ef" });
      Assert.AreEqual(expectedReadonlyStrings, readonlyStrings);
   }

   [Test]
   public static void SequenceToReadOnlyCollection_EmptySequence_ReturnsEmptyReadOnlyCollection()
   {
      int[] intArray = Array.Empty<int>();
      //
      ReadOnlyCollection<int> readOnlyIntCollection = IEnumerableExtensions.ToReadOnlyCollection(intArray);
      //
      Assert.IsEmpty(readOnlyIntCollection);
   }

   [Test]
   public static void SequenceToReadOnlyCollection_NonEmptySequenceReturnsNonEmptyReadOnlyCollection()
   {
      string[] stringArray = new[] { TestRandom.String(), TestRandom.String() };
      //
      ReadOnlyCollection<string> readOnlyStringCollection = IEnumerableExtensions.ToReadOnlyCollection(stringArray);
      //
      var expectedReadOnlyStringCollection = new ReadOnlyCollection<string>(new[]
      {
         stringArray[0],
         stringArray[1]
      });
      Assert.AreEqual(expectedReadOnlyStringCollection, readOnlyStringCollection);
   }

   class TestObject
   {
      public object RandomProperty;
   }

   static string GetRandomPropertyAsString(TestObject testObject)
   {
      string randomPropertyAsString = testObject.RandomProperty.ToString();
      return randomPropertyAsString;
   }

   [Test]
   public static void GetRandomPropertyAsString_CodeCoverage()
   {
      var testObject = new TestObject
      {
         RandomProperty = new object()
      };
      //
      string testObjectAsString = GetRandomPropertyAsString(testObject);
      //
      string expectedReturnValue = testObject.RandomProperty.ToString();
      Assert.AreEqual(expectedReturnValue, testObjectAsString);
   }
}
