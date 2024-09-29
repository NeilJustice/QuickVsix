using CSharpUtils;
using NUnit.Framework;
using System;
using System.Collections.ObjectModel;

[TestFixture]
public class ReadOnlyCollectionMakerTests
{
   ReadOnlyCollectionMaker _readOnlyCollectionMaker;

   [SetUp]
   public void SetUp()
   {
      _readOnlyCollectionMaker = new ReadOnlyCollectionMaker();
   }

   [Test]
   public void MakeReadOnlyCollection_EmptyElements_ReturnsEmptyReadOnlyCollection__IntTestCase()
   {
      int[] emptyElements = Array.Empty<int>();
      //
      ReadOnlyCollection<int> readOnlyCollection = _readOnlyCollectionMaker.MakeReadOnlyCollection(emptyElements);
      //
      ReadOnlyCollection<int> expectedReturnValue = Array.Empty<int>().ToReadOnlyCollection();
      Assert.AreEqual(expectedReturnValue, readOnlyCollection);
   }

   [Test]
   public void MakeReadOnlyCollection_NonEmptyElements_ReturnsReadOnlyCollectionOfElements__IntTestCase()
   {
      int[] nonEmptyElements = new int[] { 1, 2, 3 };
      //
      ReadOnlyCollection<int> readOnlyCollection = _readOnlyCollectionMaker.MakeReadOnlyCollection(nonEmptyElements);
      //
      ReadOnlyCollection<int> expectedReturnValue = new int[] { 1, 2, 3 }.ToReadOnlyCollection();
      Assert.AreEqual(expectedReturnValue, readOnlyCollection);
   }

   [Test]
   public void MakeReadOnlyCollection_EmptyElements_ReturnsEmptyReadOnlyCollection__StringTestCase()
   {
      string[] emptyElements = Array.Empty<string>();
      //
      ReadOnlyCollection<string> readOnlyCollection = _readOnlyCollectionMaker.MakeReadOnlyCollection(emptyElements);
      //
      ReadOnlyCollection<string> expectedReturnValue = Array.Empty<string>().ToReadOnlyCollection();
      Assert.AreEqual(expectedReturnValue, readOnlyCollection);
   }

   [Test]
   public void MakeReadOnlyCollection_NonEmptyElements_ReturnsReadOnlyCollectionOfElements__StringTestCase()
   {
      string[] nonEmptyElements = new string[] { "1", "2", "3" };
      //
      ReadOnlyCollection<string> readOnlyCollection = _readOnlyCollectionMaker.MakeReadOnlyCollection(nonEmptyElements);
      //
      ReadOnlyCollection<string> expectedReturnValue = new string[] { "1", "2", "3" }.ToReadOnlyCollection();
      Assert.AreEqual(expectedReturnValue, readOnlyCollection);
   }
}
