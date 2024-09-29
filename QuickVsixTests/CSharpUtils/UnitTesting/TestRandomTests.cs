using CSharpUtils;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.RegularExpressions;

struct UserType
{
}

[TestFixture]
public static class TestRandomTests
{
   [Test]
   public static void Bool_ReturnsTrueAndFalseIn100Iterations()
   {
      var uniqueBools = new HashSet<bool>();
      for (int i = 0; i < 100; ++i)
      {
         bool randomBool = TestRandom.Bool();
         uniqueBools.Add(randomBool);
      }
      Assert.AreEqual(2, uniqueBools.Count);
   }

   [Test]
   public static void Int_ReturnsAtLeast3UniqueIntsIn100Iterations()
   {
      var uniqueInts = new HashSet<int>();
      for (int i = 0; i < 100; ++i)
      {
         int randomInt = TestRandom.Int();
         uniqueInts.Add(randomInt);
      }
      Assert.GreaterOrEqual(uniqueInts.Count, 3);
   }

   [Test]
   [ExcludeFromCodeCoverage]
   public static void IntBetween_ReturnsIntBetweenInclusiveMinAndInclusiveMax()
   {
      int inclusiveMin = 0;
      int inclusiveMax = 2;
      bool didSee0 = false;
      bool didSee1 = false;
      bool didSee2 = false;
      for (int i = 0; i < 100; ++i)
      {
         int randomInt = TestRandom.IntBetween(inclusiveMin, inclusiveMax);
         if (randomInt == 0)
         {
            didSee0 = true;
         }
         else if (randomInt == 1)
         {
            didSee1 = true;
         }
         else if (randomInt == 2)
         {
            didSee2 = true;
         }
         else
         {
            throw new ArgumentException("TestRandom.IntBetween(inclusiveMin, int.MaxValue) returned unexpected int: " + randomInt);
         }
         if (didSee0 && didSee1 && didSee2)
         {
            return;
         }
      }
      Assert.Fail("TestRandom.IntBetween(0, 2) did not return 0, 1, and 2 within 100 iterations");
   }

   [Test]
   [ExcludeFromCodeCoverage]
   public static void IntBetween_InclusiveMaxIsIntMaxValue_ReturnsIntBetweenMinValueAndIntMaxValueMinus1()
   {
      bool didSeeIntMaxValueMinus2 = false;
      bool didSeeIntMaxValueMinus1 = false;
      for (int i = 0; i < 100; ++i)
      {
         int randomInt = TestRandom.IntBetween(int.MaxValue - 2, int.MaxValue);
         if (randomInt == int.MaxValue - 2)
         {
            didSeeIntMaxValueMinus2 = true;
         }
         else if (randomInt == int.MaxValue - 1)
         {
            didSeeIntMaxValueMinus1 = true;
         }
         else
         {
            throw new ArgumentException("TestRandom.IntBetween(int.MaxValue - 2, int.MaxValue) returned unexpected int: " + randomInt);
         }
         if (didSeeIntMaxValueMinus2 && didSeeIntMaxValueMinus1)
         {
            return;
         }
      }
      Assert.Fail("TestRandom.IntBetween(int.MaxValue - 2, int.MaxValue) did not return int.MaxValue - 2 and int.MaxValue - 1 within 100 iterations");
   }

   [Test]
   public static void Non0Int_Returns3UniqueNon0IntsIn100Calls_RandomNon0IntIsBetweenNegative10000And10000NotIncluding0()
   {
      var uniqueNon0Ints = new HashSet<int>();
      for (int i = 0; i < 100; ++i)
      {
         int randomNon0Int = TestRandom.Non0Int();
         Assert.AreNotEqual(0, randomNon0Int);
         Assert.GreaterOrEqual(randomNon0Int, -10000);
         Assert.LessOrEqual(randomNon0Int, 10000);
         uniqueNon0Ints.Add(randomNon0Int);
      }
      Assert.GreaterOrEqual(uniqueNon0Ints.Count, 3);
   }

   [Test]
   public static void ReadOnlyCollection_Int_ReturnsReadOnlyIntCollectionWithCountBetween0And3()
   {
      ReadOnlyCollection<int> randomReadOnlyIntCollection = TestRandom.ReadOnlyCollection<int>();
      Assert.IsTrue(randomReadOnlyIntCollection.Count <= 3);
   }

   [Test]
   public static void ReadOnlyCollection_UserType_ReturnsReadOnlyIntCollectionWithCountBetween0And3()
   {
      ReadOnlyCollection<UserType> randomReadOnlyIntCollection = TestRandom.ReadOnlyCollection<UserType>();
      Assert.IsTrue(randomReadOnlyIntCollection.Count <= 3);
   }

   [TestCase(1)]
   [TestCase(2)]
   public static void ReadOnlyCollectionWithCount_ReturnsReadOnlyCollectionWithCount(int count)
   {
      ReadOnlyCollection<int> randomReadOnlyIntCollection = TestRandom.ReadOnlyCollectionWithCount<int>(count);
      Assert.AreEqual(count, randomReadOnlyIntCollection.Count);
   }

   [Test]
   [ExcludeFromCodeCoverage]
   public static void ReadOnlyCollectionWithCountBetween_ReturnsReadOnlyCollectionWithCountBetweenMinSizeAndMaxSize__IntTestCase()
   {
      ReadOnlyCollection<int> randomReadOnlyIntCollection = TestRandom.ReadOnlyCollectionWithCountBetween<int>(1, 2);
      Assert.IsTrue(randomReadOnlyIntCollection.Count == 1 || randomReadOnlyIntCollection.Count == 2);
   }

   [Test]
   [ExcludeFromCodeCoverage]
   public static void ReadOnlyCollectionWithCountBetween_ReturnsReadOnlyCollectionWithCountBetweenMinSizeAndMaxSize__UserTypeTestCase()
   {
      ReadOnlyCollection<UserType> randomReadOnlyUserTypeCollection = TestRandom.ReadOnlyCollectionWithCountBetween<UserType>(3, 5);
      Assert.IsTrue(randomReadOnlyUserTypeCollection.Count >= 3 && randomReadOnlyUserTypeCollection.Count <= 5);
   }

   [Test]
   public static void ReadOnlyStringCollection_ReturnsReadOnlyStringCollectionWithCountBetween0And3()
   {
      ReadOnlyCollection<string> randomReadOnlyStringCollection = TestRandom.ReadOnlyStringCollection();
      //
      Assert.IsTrue(randomReadOnlyStringCollection.Count <= 3);
      Array.ForEach(randomReadOnlyStringCollection.ToArray(), Assert2.IsNotNullOrEmpty);
   }

   [Test]
   public static void ReadOnlyStringCollectionWithCount_ReturnsReadOnlyStringCollectionWithSpecifiedCount()
   {
      int count = TestRandom.IntBetween(0, 10);
      //
      ReadOnlyCollection<string> randomReadOnlyStringCollection = TestRandom.ReadOnlyStringCollectionWithCount(count);
      //
      Assert.AreEqual(count, randomReadOnlyStringCollection.Count);
      Array.ForEach(randomReadOnlyStringCollection.ToArray(), Assert2.IsNotNullOrEmpty);
   }

   [Test]
   public static void String_ReturnsAtLeast3UniqueStringsIn100Calls()
   {
      var uniqueStrings = new HashSet<string>();
      for (int i = 0; i < 100; ++i)
      {
         string randomString = TestRandom.String();
         Assert.IsTrue(Regex.IsMatch(randomString, @"^RandomString\d{1,4}$"));
         uniqueStrings.Add(randomString);
      }
      Assert.GreaterOrEqual(uniqueStrings.Count, 3);
   }

   enum TestEnum
   {
      A,
      B,
      C
   }

   [Test]
   [ExcludeFromCodeCoverage]
   public static void Enum_ReturnsRandomEnum()
   {
      TestEnum randomEnum = TestRandom.Enum<TestEnum>();
      Assert.IsTrue(randomEnum == TestEnum.A || randomEnum == TestEnum.B || randomEnum == TestEnum.C);
   }

   [Test]
   public static void DateTime_DoesNotThrowException()
   {
      var uniqueDateTimes = new HashSet<DateTime>();
      for (int i = 0; i < 100; ++i)
      {
         DateTime randomDateTime = TestRandom.DateTime();
         uniqueDateTimes.Add(randomDateTime);
      }
      Assert.GreaterOrEqual(uniqueDateTimes.Count, 3);
   }

   [Test]
   public static void DateTimeBetween_ReturnsRandomDateBetweenInclusiveMinAndInclusiveMax()
   {
      var a = TestRandom.DateTimeBetween(new DateTime(1, 1, 1), new DateTime(1, 1, 1));
      Assert.AreEqual(new DateTime(1, 1, 1), a);

      var b = TestRandom.DateTimeBetween(new DateTime(2, 1, 1), new DateTime(3, 1, 1));
      Assert.GreaterOrEqual(b, new DateTime(2, 1, 1));
      Assert.LessOrEqual(b, new DateTime(3, 1, 1));
   }

   [Test]
   public static void HashSet_ReturnsHashSetOfSizeBetween0And3__IntTestCase()
   {
      HashSet<int> randomIntHashSet = TestRandom.HashSet<int>();
      Assert.IsTrue(randomIntHashSet.Count <= 3);
   }

   [Test]
   public static void HashSet_ReturnsHashSetOfSizeBetween0And3__ByteTestCase()
   {
      HashSet<byte> randomByteHashSet = TestRandom.HashSet<byte>();
      Assert.IsTrue(randomByteHashSet.Count <= 3);
   }

   [Test]
   public static void StringHashSet_ReturnsHashSetOfSizeBetween0And3()
   {
      HashSet<string> randomStringHashSet = TestRandom.StringHashSet();
      Assert.IsTrue(randomStringHashSet.Count <= 3);
   }

   [Test]
   public static void TimeSpan_ReturnsAtLeast3UniqueTimeSpansIn100Calls()
   {
      var uniqueTimeSpans = new HashSet<TimeSpan>();
      for (int i = 0; i < 100; ++i)
      {
         TimeSpan randomTimeSpan = TestRandom.TimeSpan();
         uniqueTimeSpans.Add(randomTimeSpan);
      }
      Assert.GreaterOrEqual(uniqueTimeSpans.Count, 3);
   }

   [Test]
   [ExcludeFromCodeCoverage]
   public static void Array_ReturnsArrayOfTWithSizeBetween0And2WithEachElementANewDefaultConstructedT__IntTestCase()
   {
      int[] randomIntArray = TestRandom.Array<int>();
      //
      Assert.IsTrue(randomIntArray.Length >= 0 && randomIntArray.Length <= 2);
      foreach (int randomInt in randomIntArray)
      {
         Assert.AreEqual(0, randomInt);
      }
   }

   [Test]
   [ExcludeFromCodeCoverage]
   public static void Array_ReturnsArrayOfTWithSizeBetween0And2WithEachElementANewDefaultConstructedT__DecimalTestCase()
   {
      decimal[] randomDecimalArray = TestRandom.Array<decimal>();
      //
      Assert.IsTrue(randomDecimalArray.Length >= 0 && randomDecimalArray.Length <= 2);
      foreach (decimal randomDecimal in randomDecimalArray)
      {
         Assert.AreEqual(0, randomDecimal);
      }
   }

   [Test]
   [ExcludeFromCodeCoverage]
   public static void StringArray_ReturnsAStringArrayWithLengthBetween0And2PopulatedWithRandomStrings()
   {
      string[] randomStringArray = TestRandom.StringArray();
      Assert.IsTrue(randomStringArray.Length >= 0 && randomStringArray.Length <= 2);
      foreach (string randomString in randomStringArray)
      {
         Assert.IsTrue(randomString.StartsWith("RandomString", StringComparison.Ordinal));
      }
   }

   [Test]
   public static void ProcessResultWithExitCode_ReturnsNewProcessResultWithAllRandomFields()
   {
      var randomGeneratorMock = Mock.Strict<RandomGenerator>();
      string fileName = TestRandom.String();
      string arguments = TestRandom.String();
      string standardOutput = TestRandom.String();
      string standardError = TestRandom.String();
      Mock.ReturnValues(() => randomGeneratorMock.String(),
         fileName,
         arguments,
         standardOutput,
         standardError);

      int exitCode = Mock.ReturnRandomInt(() => randomGeneratorMock.Int());

      DateTime startTime = TestRandom.DateTime();
      DateTime endTime = TestRandom.DateTime();
      Mock.ReturnValues(() => randomGeneratorMock.DateTime(), startTime, endTime);
      //
      ProcessResult randomProcessResult = TestRandom.TestableProcessResultWithExitCode(exitCode, randomGeneratorMock);
      //
      Called.NumberOfTimes(4, () => randomGeneratorMock.String());
      Called.NumberOfTimes(2, () => randomGeneratorMock.DateTime());
      Called.WasCalled(() => randomGeneratorMock.String());
      Called.WasCalled(() => randomGeneratorMock.String());
      Called.WasCalled(() => randomGeneratorMock.String());
      Called.WasCalled(() => randomGeneratorMock.String());
      Called.WasCalled(() => randomGeneratorMock.DateTime());
      Called.WasCalled(() => randomGeneratorMock.DateTime());
      var expectedRandomProcessResult = new ProcessResult
      {
         processStartInfo = new ProcessStartInfo(fileName, arguments),
         exitCode = exitCode,
         standardOutput = standardOutput,
         standardError = standardError,
         startTime = startTime,
         endTime = endTime
      };
      Assert.AreEqual(expectedRandomProcessResult, randomProcessResult);
   }
}
