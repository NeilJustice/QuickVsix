using CSharpUtils;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;

[TestFixture]
public class RandomGeneratorTests
{
   RandomGenerator _randomGenerator;

   [SetUp]
   public void SetUp()
   {
      _randomGenerator = new RandomGenerator();
   }

   [Test]
   [ExcludeFromCodeCoverage]
   public void Bool_ReturnsTrueAndFalseWithin100Calls()
   {
      bool haveSeenTrue = false;
      bool haveSeenFalse = false;
      for (int i = 0; i < 100; ++i)
      {
         bool randomBool = _randomGenerator.Bool();
         if (randomBool)
         {
            haveSeenTrue = true;
         }
         else
         {
            haveSeenFalse = true;
         }
         if (haveSeenTrue && haveSeenFalse)
         {
            break;
         }
      }
      if (!haveSeenTrue || !haveSeenFalse)
      {
         Assert.Fail("RandomGenerator.RandomBool() failed to return true and false within 100 calls");
      }
   }

   [Test]
   [ExcludeFromCodeCoverage]
   public void Int_ReturnsFiveDifferentIntsWithin100Calls()
   {
      var uniqueInts = new HashSet<int>();
      for (int i = 0; i < 100; ++i)
      {
         int randomInt = _randomGenerator.Int();
         uniqueInts.Add(randomInt);
         if (uniqueInts.Count == 5)
         {
            return;
         }
      }
      Assert.Fail("RandomGenerator.RandomInt() failed to return five different ints within 100 calls");
   }

   [Test]
   [ExcludeFromCodeCoverage]
   public void String_ReturnsFiveDifferentRandomStringsWithin100Calls()
   {
      var uniqueStrings = new HashSet<string>();
      for (int i = 0; i < 100; ++i)
      {
         string randomString = _randomGenerator.String();
         uniqueStrings.Add(randomString);
         if (uniqueStrings.Count == 5)
         {
            return;
         }
      }
      Assert.Fail("RandomGenerator.RandomString() failed to return five different strings within 100 calls");
   }

   enum TestingEnumType
   {
      A,
      B,
      C
   }

   [Test]
   [ExcludeFromCodeCoverage]
   public void Enum_ReturnsTwoDifferentEnumValuesWithin100Calls()
   {
      var uniqueEnumValues = new HashSet<TestingEnumType>();
      for (int i = 0; i < 100; ++i)
      {
         var randomEnumValue = _randomGenerator.Enum<TestingEnumType>();
         uniqueEnumValues.Add(randomEnumValue);
         if (uniqueEnumValues.Count == 2)
         {
            return;
         }
      }
      Assert.Fail("RandomGenerator.RandomEnum<TestingEnumType>() failed to return two different enum values within 100 calls");
   }

   [Test]
   [ExcludeFromCodeCoverage]
   public void DateTime_ReturnsTwoDifferentDateTimesWithin100Calls()
   {
      var uniqueDateTimeValues = new HashSet<DateTime>();
      for (int i = 0; i < 100; ++i)
      {
         var randomDateTime = _randomGenerator.DateTime();
         uniqueDateTimeValues.Add(randomDateTime);
         if (uniqueDateTimeValues.Count == 2)
         {
            return;
         }
      }
      Assert.Fail("RandomGenerator.RandomDateTime() failed to return two different DateTime values within 100 calls");
   }

   [Test]
   public void ReadOnlyCollection_ReturnsRandomReadOnlyCollectionWithCountLTE3()
   {
      ReadOnlyCollection<int> randomInts = _randomGenerator.ReadOnlyCollection<int>();
      Assert.IsTrue(randomInts.Count <= 3);
   }

   [Test]
   public void ReadOnlyStringCollection_ReturnsRandomReadOnlyStringCollectionWithCountLTE3()
   {
      ReadOnlyCollection<string> randomString = _randomGenerator.ReadOnlyStringCollection();
      Assert.IsTrue(randomString.Count <= 3);
   }
}
