using CSharpUtils;
using NUnit.Framework;
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
      int[] emptyElements = new int[0];
      //
      ReadOnlyCollection<int> readOnlyCollection = _readOnlyCollectionMaker.MakeReadOnlyCollection(emptyElements);
      //
      ReadOnlyCollection<int> expectedReturnValue = new int[0].ToReadOnlyCollection();
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
      string[] emptyElements = new string[0];
      //
      ReadOnlyCollection<string> readOnlyCollection = _readOnlyCollectionMaker.MakeReadOnlyCollection(emptyElements);
      //
      ReadOnlyCollection<string> expectedReturnValue = new string[0].ToReadOnlyCollection();
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
