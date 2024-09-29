using CSharpUtils;
using NUnit.Framework;
using System.Collections.Generic;
using System.Collections.ObjectModel;

[TestFixture]
public static class IDictionaryExtensionsTests
{
   [Test]
   public static void ToReadOnlyDictionary_ReturnsReadOnlyDictionary()
   {
      int key = TestRandom.Int();
      string value = TestRandom.String();
      var mutableDictionary = new Dictionary<int, string>
      {
         { key, value }
      };
      //
      var readonlyDictionary = mutableDictionary.ToReadOnlyDictionary();
      //
      var expectedReadOnlyDictionary = new ReadOnlyDictionary<int, string>(new Dictionary<int, string>
      {
         { key, value }
      });
      Assert.AreEqual(expectedReadOnlyDictionary, readonlyDictionary);
   }
}
