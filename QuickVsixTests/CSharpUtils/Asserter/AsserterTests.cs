using CSharpUtils;
using NUnit.Framework;
using System;
using System.Collections.Generic;

struct EqualIfValuesEqualObject : IEquatable<EqualIfValuesEqualObject>
{
   private readonly int value;

   public EqualIfValuesEqualObject(int value)
   {
      this.value = value;
   }

   public override bool Equals(object obj)
   {
      if (((EqualIfValuesEqualObject)obj).value == value)
      {
         return true;
      }
      return false;
   }

   public override string ToString()
   {
      string toString = $"EqualIfValuesEqualObject({value})";
      return toString;
   }

   public override int GetHashCode()
   {
      throw new NotSupportedException();
   }

   public bool Equals(EqualIfValuesEqualObject other)
   {
      throw new NotSupportedException();
   }
}

[TestFixture]
public class AsserterTests
{
   Asserter _asserter;

   [SetUp]
   public void SetUp()
   {
      _asserter = new Asserter();
   }

   [Test]
   public void ThrowIfEqual_ValuesAreNotEqual_DoesNothing()
   {
      string realValueArgumentText = TestRandom.String();
      string additionalMessage = TestRandom.String();
      _asserter.ThrowIfEqual(false, true, realValueArgumentText, additionalMessage);

      int valueA = TestRandom.Int();
      int valueB = valueA + 1;
      var objectA = new EqualIfValuesEqualObject(valueA);
      var objectB = new EqualIfValuesEqualObject(valueB);
      Assert.DoesNotThrow(() => _asserter.ThrowIfEqual(objectA, objectB, realValueArgumentText, additionalMessage));
   }

   [Test]
   public void ThrowIfEqual_ValuesAreEqual_AdditionalMessageIsNonNull_ThrowsWithExpectedExceptionMessage()
   {
      string realValueArgumentText = TestRandom.String();
      string additionalMessage = TestRandom.String();

      Assert2.Throws<InvalidOperationException>(
         () => _asserter.ThrowIfEqual(true, true, realValueArgumentText, additionalMessage),
          $@"CSharpUtils.Asserter.ThrowIfEqual() failed: realValue.Equals(unexpectedValue) returned true.
unexpectedValue.ToString()=[True]
      realValue.ToString()=[True]
     realValueArgumentText=[{realValueArgumentText}]
         additionalMessage=[{additionalMessage}]
");

      Assert2.Throws<InvalidOperationException>(
         () => _asserter.ThrowIfEqual(false, false, realValueArgumentText, additionalMessage),
          $@"CSharpUtils.Asserter.ThrowIfEqual() failed: realValue.Equals(unexpectedValue) returned true.
unexpectedValue.ToString()=[False]
      realValue.ToString()=[False]
     realValueArgumentText=[{realValueArgumentText}]
         additionalMessage=[{additionalMessage}]
");

      int value = TestRandom.Int();
      var objectA = new EqualIfValuesEqualObject(value);
      var objectB = new EqualIfValuesEqualObject(value);
      Assert2.Throws<InvalidOperationException>(
         () => _asserter.ThrowIfEqual(objectA, objectB, realValueArgumentText, additionalMessage),
          $@"CSharpUtils.Asserter.ThrowIfEqual() failed: realValue.Equals(unexpectedValue) returned true.
unexpectedValue.ToString()=[EqualIfValuesEqualObject({value})]
      realValue.ToString()=[EqualIfValuesEqualObject({value})]
     realValueArgumentText=[{realValueArgumentText}]
         additionalMessage=[{additionalMessage}]
");
   }

   [Test]
   public void ThrowIfNotEqual_ValuesAreEqual_DoesNothing()
   {
      string realValueArgumentText = TestRandom.String();
      string additionalMessage = TestRandom.String();
      _asserter.ThrowIfNotEqual(true, true, realValueArgumentText, additionalMessage);

      int value = TestRandom.Int();
      var objectA = new EqualIfValuesEqualObject(value);
      var objectB = new EqualIfValuesEqualObject(value);
      Assert.DoesNotThrow(() => _asserter.ThrowIfNotEqual(objectA, objectB, realValueArgumentText, additionalMessage));
   }

   [Test]
   public void ThrowIfNotEqual_ValuesAreNotEqual_AdditionalMessageIsNonNull_ThrowsWithExpectedExceptionMessage()
   {
      string realValueArgumentText = TestRandom.String();
      string additionalMessage = TestRandom.String();

      Assert2.Throws<InvalidOperationException>(
         () => _asserter.ThrowIfNotEqual(true, false, realValueArgumentText, additionalMessage),
          $@"CSharpUtils.Asserter.ThrowIfNotEqual() failed: realValue.Equals(expectedValue) returned false.
expectedValue.ToString()=[True]
    realValue.ToString()=[False]
   realValueArgumentText=[{realValueArgumentText}]
       additionalMessage=[{additionalMessage}]
");

      Assert2.Throws<InvalidOperationException>(
         () => _asserter.ThrowIfNotEqual(false, true, realValueArgumentText, additionalMessage),
          $@"CSharpUtils.Asserter.ThrowIfNotEqual() failed: realValue.Equals(expectedValue) returned false.
expectedValue.ToString()=[False]
    realValue.ToString()=[True]
   realValueArgumentText=[{realValueArgumentText}]
       additionalMessage=[{additionalMessage}]
");

      int value = TestRandom.Int();
      var objectA = new EqualIfValuesEqualObject(value);
      var objectB = new EqualIfValuesEqualObject(value + 1);
      Assert2.Throws<InvalidOperationException>(
         () => _asserter.ThrowIfNotEqual(objectA, objectB, realValueArgumentText, additionalMessage),
          $@"CSharpUtils.Asserter.ThrowIfNotEqual() failed: realValue.Equals(expectedValue) returned false.
expectedValue.ToString()=[EqualIfValuesEqualObject({value})]
    realValue.ToString()=[EqualIfValuesEqualObject({value + 1})]
   realValueArgumentText=[{realValueArgumentText}]
       additionalMessage=[{additionalMessage}]
");
   }

   [Test]
   public void ThrowIfNull_ValueIsNotNull_DoesNothing__StringTestCase()
   {
      string nonNullValue = TestRandom.String();
      string variableName = TestRandom.String();
      //
      Assert.DoesNotThrow(() => _asserter.ThrowIfNull(nonNullValue, variableName));
   }

   [Test]
   public void ThrowIfNull_ValueIsNull_ThrowsInvalidOperationException__StringTestCase()
   {
      string nullValue = null;
      string variableName = "nullValue";
      //
      string expectedExceptionMessage = $"ThrowIfNull failed for variableName=\"{variableName}\"";
      Assert2.Throws<InvalidOperationException>(() => _asserter.ThrowIfNull(nullValue, variableName),
         expectedExceptionMessage);
   }

   [Test]
   public void ThrowIfNull_ValueIsNotNull_DoesNothing__HashSetTestCase()
   {
      var nonNullInts = new HashSet<int>();
      string variableName = TestRandom.String();
      //
      Assert.DoesNotThrow(() => _asserter.ThrowIfNull(nonNullInts, variableName));
   }

   [Test]
   public void ThrowIfNull_ValueIsNull_ThrowsInvalidOperationException__HashSetTestCase()
   {
      HashSet<int> nullHashSet = null;
      string variableName = TestRandom.String();
      //
      string expectedExceptionMessage = $"ThrowIfNull failed for variableName=\"{variableName}\"";
      Assert2.Throws<InvalidOperationException>(() => _asserter.ThrowIfNull(nullHashSet, variableName),
         expectedExceptionMessage);
   }

   [Test]
   public static void GetHashCode_ThrowsNotSupportedException()
   {
      Assert2.ThrowsNotSupportedException(() => new EqualIfValuesEqualObject().GetHashCode());
   }

   [Test]
   public static void Equals_ThrowsInvalidCastException()
   {
      Assert2.ThrowsNotSupportedException(() => new EqualIfValuesEqualObject().Equals(new EqualIfValuesEqualObject()));
   }
}
