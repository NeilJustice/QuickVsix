using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace CSharpUtils
{
   public static class TestRandom
   {
      private static readonly Random random = new Func<Random>(delegate
      {
         int seed = Math.Abs((int)System.DateTime.Now.Ticks);
         Console.WriteLine($"CSharpUtils.TestRandom seed = {seed}");
         var random = new Random(seed);
         return random;
      })();

      public static bool Bool()
      {
         bool randomBool = random.Next() % 2 == 0;
         return randomBool;
      }

      [ExcludeFromCodeCoverage]
      private static void FillArray<T>(T[] array, Func<T> fillFunction)
      {
         for (int i = 0; i < array.Length; ++i)
         {
            array[i] = fillFunction();
         }
      }

      public static int Int()
      {
         int randomInt = random.Next(-10000, 10001);
         return randomInt;
      }

      public static int IntBetween(int inclusiveMin, int inclusiveMax)
      {
         int nonOverflowInclusiveMaxValue = inclusiveMax == int.MaxValue ? int.MaxValue : inclusiveMax + 1;
         int constrainedRandomInt = random.Next(inclusiveMin, nonOverflowInclusiveMaxValue);
         return constrainedRandomInt;
      }

      [ExcludeFromCodeCoverage]
      public static int Non0Int()
      {
         int randomNon0Int = Int();
         if (randomNon0Int == 0)
         {
            randomNon0Int = 1;
         }
         return randomNon0Int;
      }

      public static ReadOnlyCollection<T> ReadOnlyCollection<T>() where T : new()
      {
         return ReadOnlyCollectionWithCountBetween<T>(0, 3);
      }

      public static ReadOnlyCollection<T> ReadOnlyCollectionWithCount<T>(int count) where T : new()
      {
         var randomArray = new T[count];
         FillArray(randomArray, New<T>);
         var randomReadOnlyCollection = new ReadOnlyCollection<T>(randomArray);
         return randomReadOnlyCollection;
      }

      public static ReadOnlyCollection<T> ReadOnlyCollectionWithCountBetween<T>(int minSize, int maxSize) where T : new()
      {
         int randomSize = IntBetween(minSize, maxSize);
         var randomArray = new T[randomSize];
         FillArray(randomArray, New<T>);
         var randomReadOnlyCollection = new ReadOnlyCollection<T>(randomArray);
         return randomReadOnlyCollection;
      }

      public static ReadOnlyCollection<string> ReadOnlyStringCollection()
      {
         int randomSize = IntBetween(0, 3);
         var randomStringArray = new string[randomSize];
         FillArray(randomStringArray, TestRandom.String);
         var randomReadOnlyStringCollection = new ReadOnlyCollection<string>(randomStringArray);
         return randomReadOnlyStringCollection;
      }

      public static ReadOnlyCollection<string> ReadOnlyStringCollectionWithCount(int count)
      {
         var randomStringArray = new string[count];
         FillArray(randomStringArray, TestRandom.String);
         var readOnlyStringCollectionWithCount = new ReadOnlyCollection<string>(randomStringArray);
         return readOnlyStringCollectionWithCount;
      }

      private static T New<T>() where T : new()
      {
         return new T();
      }

      public static string String()
      {
         string randomString = "RandomString" + random.Next(1, 1001);
         return randomString;
      }

      public static EnumType Enum<EnumType>()
      {
         Type enumType = typeof(EnumType);
         EnumType[] enumValues = enumType.GetEnumValues().Cast<EnumType>().ToArray();
         int randomIndex = IntBetween(0, enumValues.Length - 1);
         EnumType randomEnum = enumValues[randomIndex];
         return randomEnum;
      }

      public static DateTime DateTime()
      {
         int randomYear = IntBetween(1, 9990); // 9990 instead of 9999 to allow for adding 1 to 9 days for not-equal-to testing
         int randomMonth = IntBetween(1, 12);
         int randomDay = IntBetween(1, 28);
         int randomHour = IntBetween(0, 23);
         int randomMinute = IntBetween(0, 59);
         int randomSecond = IntBetween(0, 59);
         int randomMillisecond = IntBetween(0, 999);
         var randomDateTime = new DateTime(
             randomYear, randomMonth, randomDay, randomHour, randomMinute, randomSecond, randomMillisecond);
         return randomDateTime;
      }

      public static DateTime DateTimeBetween(DateTime inclusiveMin, DateTime inclusiveMax)
      {
         TimeSpan diff = inclusiveMax - inclusiveMin;
         int randomDays = IntBetween(0, diff.Days);
         int randomHours = IntBetween(0, diff.Hours);
         int randomMinutes = IntBetween(0, diff.Minutes);
         int randomSeconds = IntBetween(0, diff.Seconds);
         int randomMilliseconds = IntBetween(0, diff.Milliseconds);
         DateTime dateTimeBetween = inclusiveMin.AddDays(randomDays).AddHours(randomHours).
            AddMinutes(randomMinutes).AddSeconds(randomSeconds).AddMilliseconds(randomMilliseconds);
         return dateTimeBetween;
      }

      public static HashSet<T> HashSet<T>() where T : new()
      {
         ReadOnlyCollection<T> randomReadOnlyCollectionWithCountBetween0And3 = ReadOnlyCollectionWithCountBetween<T>(0, 3);
         var randomHashSet = new HashSet<T>(randomReadOnlyCollectionWithCountBetween0And3);
         return randomHashSet;
      }

      public static HashSet<string> StringHashSet()
      {
         ReadOnlyCollection<string> randomReadOnlyStringCollection = ReadOnlyStringCollection();
         var randomStringHashSet = new HashSet<string>(randomReadOnlyStringCollection);
         return randomStringHashSet;
      }

      public static TimeSpan TimeSpan()
      {
         int randomDays = IntBetween(0, 10000);
         int randomHours = IntBetween(0, 23);
         int randomMinutes = IntBetween(0, 59);
         int randomSeconds = IntBetween(0, 59);
         int randomMilliseconds = IntBetween(0, 999);
         var randomTimeSpan = new TimeSpan(randomDays, randomHours, randomMinutes, randomSeconds, randomMilliseconds);
         return randomTimeSpan;
      }

      [ExcludeFromCodeCoverage]
      public static T[] Array<T>() where T : new()
      {
         int randomArraySize = IntBetween(0, 2);
         var randomArray = new T[randomArraySize];
         for (int i = 0; i < randomArraySize; ++i)
         {
            randomArray[i] = new T();
         }
         return randomArray;
      }

      [ExcludeFromCodeCoverage]
      public static string[] StringArray()
      {
         int randomArrayLength = IntBetween(0, 2);
         var randomStringArray = new string[randomArrayLength];
         for (int i = 0; i < randomArrayLength; ++i)
         {
            string randomString = TestRandom.String();
            randomStringArray[i] = randomString;
         }
         return randomStringArray;
      }

      public static ProcessResult ProcessResultWithRandomExitCode()
      {
         int randomExitCode = TestRandom.Int();
         return TestableProcessResultWithExitCode(randomExitCode, RandomGenerator.Instance);
      }

      public static ProcessResult ProcessResultWithExitCode(int exitCode)
      {
         return TestableProcessResultWithExitCode(exitCode, RandomGenerator.Instance);
      }

      public static ProcessResult TestableProcessResultWithExitCode(int exitCode, RandomGenerator randomGenerator)
      {
         var randomProcessResult = new ProcessResult();
         string fileName = randomGenerator.String();
         string arguments = randomGenerator.String();
         randomProcessResult.processStartInfo = new ProcessStartInfo(fileName, arguments);
         randomProcessResult.exitCode = exitCode;
         randomProcessResult.standardOutput = randomGenerator.String();
         randomProcessResult.standardError = randomGenerator.String();
         randomProcessResult.startTime = randomGenerator.DateTime();
         randomProcessResult.endTime = randomGenerator.DateTime();
         return randomProcessResult;
      }
   }
}
