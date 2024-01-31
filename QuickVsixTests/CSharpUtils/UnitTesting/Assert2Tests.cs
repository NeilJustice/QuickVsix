using System.Diagnostics.CodeAnalysis;
using CSharpUtils;
using NUnit.Framework;

[ExcludeFromCodeCoverage]
class ClassThatDoesNotThrowWhenEqualsToAnObject
{
}

[ExcludeFromCodeCoverage]
class ClassThatDoesThrowWhenEqualsToAnObject
{
   public override bool Equals(object obj)
   {
      var actual = (ClassThatDoesThrowWhenEqualsToAnObject)obj;
      UnusedVariableWarning.Suppress(ref actual);
      return true;
   }

   public override int GetHashCode()
   {
      throw new NotSupportedException();
   }
}

[TestFixture]
public static class Assert2Tests
{
   class FieldIsNullTestClass
   {
      public string s;
   }

   [OneTimeSetUp]
   public static void OneTimeSetUp()
   {
      UnusedVariableWarning.Suppress(ref new FieldIsNullTestClass().s);
   }

   [Test]
   public static void FieldIsNull_FieldIsNull_DoesNotThrowException()
   {
      var instance = new FieldIsNullTestClass();
      Assert2.FieldIsNull(instance, "s");
   }

   [Test]
   public static void FieldIsNull_FieldIsNotNull_ThrowsAssertionException()
   {
      var instance = new FieldIsNullTestClass
      {
         s = "Value"
      };
      Assert2.Throws<AssertionException>(() => Assert2.FieldIsNull(instance, "s"),
          @"  Regarding field ""s""
Assert.That(anObject, Is.Null)
  Expected: null
  But was:  ""Value""
");
   }

   [Test]
   public static void FieldIsNull_FieldDoesNotExist_ThrowsArgumentException()
   {
      var instance = new FieldIsNullTestClass();
      Assert2.Throws<ArgumentException>(() => Assert2.FieldIsNull(instance, "not_exists"),
          "Field not found: Assert2Tests+FieldIsNullTestClass.not_exists");
   }

   public class BaseClass
   {
   }

   public class DerivedClass : BaseClass
   {
   }

   public class FieldIsExactTypeTester
   {
      public string publicField;
      private readonly string privateField;
      private readonly DerivedClass derivedClassField;
      private readonly object objectField;
      private readonly DerivedClass nullDerivedClassField;

      public FieldIsExactTypeTester()
      {
         publicField = "non-null string";
         privateField = "non-null string";
         UnusedVariableWarning.Suppress(ref privateField);
         derivedClassField = new DerivedClass();
         objectField = new DerivedClass();

         UnusedVariableWarning.Suppress(ref derivedClassField);
         UnusedVariableWarning.Suppress(ref objectField);
         UnusedVariableWarning.Suppress(ref nullDerivedClassField);
      }
   }

   [Test]
   public static void FieldIsNonNullAndExactType_ExpectedFieldNameDoesNotExistOnClassInstance_ThrowsAssertionException()
   {
      var classInstance = new FieldIsExactTypeTester();
      string nonExistentFieldName = TestRandom.String();
      Assert2.Throws<AssertionException>(() =>
         Assert2.FieldIsNonNullAndExactType<string>(classInstance, nonExistentFieldName),
         $"Field name not found on classInstance: {nonExistentFieldName}");
   }

   [Test]
   public static void FieldIsNonNullAndExactType_ExpectedFieldTypeEqualToActualDeclaredFieldType_WhichAreEqual_DoesNotThrowException()
   {
      var classInstance = new FieldIsExactTypeTester();
      Assert2.FieldIsNonNullAndExactType<string>(classInstance, "publicField");
      Assert2.FieldIsNonNullAndExactType<string>(classInstance, "privateField");
      Assert2.FieldIsNonNullAndExactType<DerivedClass>(classInstance, "derivedClassField");
   }

   [Test]
   public static void FieldIsNonNullAndExactType_ActualFieldIsNull_ThrowsAssertionException()
   {
      var classInstance = new FieldIsExactTypeTester
      {
         publicField = null
      };
      Assert2.Throws<AssertionException>(() =>
         Assert2.FieldIsNonNullAndExactType<string>(classInstance, "publicField"), @"Field is null: publicField");
   }

   [Test]
   public static void FieldIsNonNullAndExactType_ExpectedFieldTypeIsABaseClassOfActualDeclaredFieldType_ThrowsAssertionException()
   {
      var classInstance = new FieldIsExactTypeTester();
      Assert2.Throws<AssertionException>(() =>
         Assert2.FieldIsNonNullAndExactType<BaseClass>(classInstance, "derivedClassField"),
@"  Field ""derivedClassField""
Assert.That(actual, Is.EqualTo(expected))
  Expected: <Assert2Tests+BaseClass>
  But was:  <Assert2Tests+DerivedClass>
");
   }

   [Test]
   public static void FieldIsNonNullAndExactType_ActualFieldIsAnObjectWithRuntimeTypeExactlyEqualToExpectedType_DoesNotThrowException()
   {
      var classInstance = new FieldIsExactTypeTester();
      Assert2.FieldIsNonNullAndExactType<DerivedClass>(classInstance, "objectField");
   }

   [Test]
   public static void FieldIsNullAndExactType_FieldDoesNotExist_ThrowsAssertionException()
   {
      var fieldIsExactTypeTester = new FieldIsExactTypeTester();
      Assert2.Throws<AssertionException>(() =>
         Assert2.FieldIsNullAndExactType<object>(fieldIsExactTypeTester, "non_existent_field"),
         "Field name not found on classInstance: non_existent_field");
   }

   [Test]
   public static void FieldIsNullAndExactType_FieldExistsButIsNotNull_ThrowsAssertionException()
   {
      var fieldIsExactTypeTester = new FieldIsExactTypeTester();
      Assert2.Throws<AssertionException>(() =>
         Assert2.FieldIsNullAndExactType<string>(fieldIsExactTypeTester, "privateField"),
@"  Field: privateField
Assert.That(anObject, Is.Null)
  Expected: null
  But was:  ""non-null string""
");
   }

   [Test]
   public static void FieldIsNullAndExactType_FieldExistsAndIsNullButNotExactType_ThrowsAssertionException()
   {
      var fieldIsExactTypeTester = new FieldIsExactTypeTester();
      Assert2.Throws<AssertionException>(() =>
         Assert2.FieldIsNullAndExactType<BaseClass>(fieldIsExactTypeTester, "nullDerivedClassField"),
@"  Assert.That(actual, Is.EqualTo(expected))
  Expected: <Assert2Tests+BaseClass>
  But was:  <Assert2Tests+DerivedClass>
");
   }

   [Test]
   public static void FieldIsNullAndExactType_FieldExistsAndIsNullAndExactType_DoesNotThrowException()
   {
      var fieldIsExactTypeTester = new FieldIsExactTypeTester();
      Assert2.FieldIsNullAndExactType<DerivedClass>(fieldIsExactTypeTester, "nullDerivedClassField");
   }

   [Test]
   public static void EqualsThrowsInvalidCastExceptionWhenComparedWithANewObject_EqualsDoesNotThrow_ThrowsAssertionException()
   {
      var classInstanceThatDoesNotThrowWhenEqualsToAnObject = new ClassThatDoesNotThrowWhenEqualsToAnObject();
      Assert2.Throws<AssertionException>(() => Assert2.EqualsThrowsInvalidCastExceptionWhenComparedWithANewObject(
         classInstanceThatDoesNotThrowWhenEqualsToAnObject),
         "Action did not throw. Exact expected exception: System.InvalidCastException");
   }

   [Test]
   public static void EqualsThrowsInvalidCastExceptionWhenComparedWithANewObject_EqualsThrows_DoesNotThrowException()
   {
      var classInstanceThatDoesThrowWhenEqualsToAnObject = new ClassThatDoesThrowWhenEqualsToAnObject();
      Assert2.EqualsThrowsInvalidCastExceptionWhenComparedWithANewObject(classInstanceThatDoesThrowWhenEqualsToAnObject);
   }

   [Test]
   public static void EqualsThrowsInvalidCastExceptionWhenComparedWithANewObject_NonFullNameVersion_EqualsDoesNotThrow_ThrowsAssertionException()
   {
      var classInstanceThatDoesNotThrowWhenEqualsToAnObject = new ClassThatDoesNotThrowWhenEqualsToAnObject();
      Assert2.Throws<AssertionException>(() => Assert2.EqualsThrowsInvalidCastExceptionWhenComparedWithANewObject_NonFullNameVersion(
          classInstanceThatDoesNotThrowWhenEqualsToAnObject),
          "Action did not throw. Exact expected exception: System.InvalidCastException");
   }

   [Test]
   public static void EqualsThrowsInvalidCastExceptionWhenComparedWithANewObject_NonFullNameVersion_EqualsThrows_DoesNotThrowException()
   {
      var classInstanceThatDoesThrowWhenEqualsToAnObject = new ClassThatDoesThrowWhenEqualsToAnObject();
      Assert2.EqualsThrowsInvalidCastExceptionWhenComparedWithANewObject_NonFullNameVersion(classInstanceThatDoesThrowWhenEqualsToAnObject);
   }

   [Test]
   public static void IsNotNullOrEmpty_StringIsNull_ThrowsAssertionException()
   {
      Assert2.Throws<AssertionException>(() => Assert2.IsNotNullOrEmpty(null),
@"  Assert.That(anObject, Is.Not.Null)
  Expected: not null
  But was:  null
");
   }

   [Test]
   public static void IsNotNullOrEmpty_StringIsEmpty_ThrowsAssertionException()
   {
      Assert2.Throws<AssertionException>(() => Assert2.IsNotNullOrEmpty(""),
@"  Assert.That(aString, Is.Not.Empty)
  Expected: not <empty>
  But was:  <string.Empty>
");
   }

   [Test]
   public static void IsNotNullOrEmpty_StringIsNotEmpty_DoesNotThrowException()
   {
      Assert.DoesNotThrow(() => Assert2.IsNotNullOrEmpty(TestRandom.String()));
   }

   [TestCase("", "", false)]
   [TestCase("a", "a", false)]
   [TestCase("A", "a", true)]
   [TestCase(@"\d", "1", false)]
   [TestCase(@"\d+", "qqq 123", false)]
   [TestCase(@"\w \w", "1_3", true)]
   public static void StringMatchesRegex_ReturnsTrueIfStringMatchesRegexPattern(
      string expectedPattern, string str, bool expectException)
   {
      if (expectException)
      {
         string exceptionMessage = $@"  Regex.IsMatch(""{expectedPattern}"", ""{str}"")
Assert.That(condition, Is.True)
  Expected: True
  But was:  False
";
         Assert2.Throws<AssertionException>(() => Assert2.StringMatchesRegex(expectedPattern, str),
            exceptionMessage);
      }
      else
      {
         Assert2.StringMatchesRegex(expectedPattern, str);
      }
   }

   [Test]
   [ExcludeFromCodeCoverage]
   public static void Throws_ActionDoesNotThrow_Throws__ExceptionTestCase()
   {
      try
      {
         Assert2.Throws<System.Exception>(() => { }, "");
         Assert.Fail("Did not throw as expected"); // Non-coverable line
      }
      catch (AssertionException ex)
      {
         Assert.AreEqual("Action did not throw. Exact expected exception: System.Exception", ex.Message);
      }
   }

   [Test]
   [ExcludeFromCodeCoverage]
   public static void Throws_ActionDoesNotThrow_Throws__IOExceptionTestCase()
   {
      try
      {
         Assert2.Throws<System.IO.IOException>(() => { }, "");
         Assert.Fail("Did not throw as expected"); // Non-coverable line
      }
      catch (AssertionException ex)
      {
         Assert.AreEqual("Action did not throw. Exact expected exception: System.IO.IOException", ex.Message);
      }
   }

   [Test]
   [ExcludeFromCodeCoverage]
   public static void Throws_ActionThrowsUnrelatedExceptionType_Throws__IOExceptionTypeCase()
   {
      try
      {
         Assert2.Throws<System.ArgumentException>(() => { throw new System.IO.IOException(); }, "");
         Assert.Fail("Did not throw as expected"); // Non-coverable line
      }
      catch (AssertionException ex)
      {
         Assert.AreEqual("Action threw System.IO.IOException. Exact expected exception: System.ArgumentException", ex.Message);
      }
   }

   [Test]
   [ExcludeFromCodeCoverage]
   public static void Throws_ActionThrowsUnrelatedExceptionType_Throws__FakeItEasyTestCase()
   {
      try
      {
         Assert2.Throws<FakeItEasy.Core.FakeCreationException>(() => { throw new FakeItEasy.Configuration.FakeConfigurationException(); }, "");
         Assert.Fail("Did not throw as expected"); // Non-coverable line
      }
      catch (AssertionException ex)
      {
         Assert.AreEqual("Action threw FakeItEasy.Configuration.FakeConfigurationException. Exact expected exception: FakeItEasy.Core.FakeCreationException", ex.Message);
      }
   }

   [Test]
   [ExcludeFromCodeCoverage]
   public static void Throws_ActionThrowsSubclassOfExpectedExceptionType_Throws__TestCase1()
   {
      try
      {
         Assert2.Throws<System.Exception>(() => { throw new System.InvalidOperationException(); }, "");
         Assert.Fail("Did not throw"); // Non-coverable line
      }
      catch (AssertionException ex)
      {
         Assert.AreEqual("Action threw System.InvalidOperationException. Exact expected exception: System.Exception", ex.Message);
      }
   }

   [Test]
   [ExcludeFromCodeCoverage]
   public static void Throws_ActionThrowsSubclassOfExpectedExceptionType_Throws__TestCase2()
   {
      try
      {
         Assert2.Throws<System.SystemException>(() => { throw new System.IO.InvalidDataException(); }, "");
         Assert.Fail("Did not throw"); // Non-coverable line
      }
      catch (AssertionException ex)
      {
         Assert.AreEqual("Action threw System.IO.InvalidDataException. Exact expected exception: System.SystemException", ex.Message);
      }
   }

   [Test]
   [ExcludeFromCodeCoverage]
   public static void Throws_ActionThrowsExactExpectedExceptionType_ActualMessageDoesNotEqualExpectedExceptionMessage_ThrowsAssertionException()
   {
      try
      {
         Assert2.Throws<Exception>(() => { throw new Exception("def"); }, "abc");
         Assert.Fail("Did not throw"); // Non-coverable line
      }
      catch (AssertionException ex)
      {
         string expectedAssertionMessage = @"Action threw exactly System.Exception as expected but with an unexpected Message.
Expected message: ""abc""
  Actual message: ""def""";
         Assert.AreEqual(expectedAssertionMessage, ex.Message);
      }
   }

   [Test]
   [ExcludeFromCodeCoverage]
   public static void Throws_ActionThrowsExactExpectedExceptionType_MessagesAreEqualExceptForCasing_ThrowsAssertionException()
   {
      try
      {
         Assert2.Throws<System.IO.IOException>(() =>
         {
            throw new System.IO.IOException("Message");
         }, "message");
         Assert.Fail("Did not throw as expected"); // Non-coverable line
      }
      catch (AssertionException ex)
      {
         string expectedAssertionMessage = @"Action threw exactly System.IO.IOException as expected but with an unexpected Message.
Expected message: ""message""
  Actual message: ""Message""";
         Assert.AreEqual(expectedAssertionMessage, ex.Message);
      }
   }

   [Test]
   [ExcludeFromCodeCoverage]
   public static void Throws_ActionThrowsExactExpectedExceptionType_ActualMessageContainsExpectedExceptionMessageWithoutBeingEqual_ThrowsAssertionException()
   {
      try
      {
         Assert2.Throws<System.Text.RegularExpressions.RegexMatchTimeoutException>(() =>
         {
            throw new System.Text.RegularExpressions.RegexMatchTimeoutException("message");
         }, "abc message");
         Assert.Fail("Did not throw as expected"); // Non-coverable line
      }
      catch (AssertionException ex)
      {
         string expectedAssertionMessage = @"Action threw exactly System.Text.RegularExpressions.RegexMatchTimeoutException as expected but with an unexpected Message.
Expected message: ""abc message""
  Actual message: ""message""";
         Assert.AreEqual(expectedAssertionMessage, ex.Message);
      }
   }

   [Test]
   public static void Throws_ActionThrowsExactExpectedExceptionType_MessagesAreEqual_DoesNotThrowException()
   {
      Assert.DoesNotThrow(() => Assert2.Throws<Exception>(() => { throw new Exception(""); }, ""));
      Assert.DoesNotThrow(() => Assert2.Throws<Exception>(() => { throw new Exception("abc"); }, "abc"));
      Assert.DoesNotThrow(() => Assert2.Throws<IOException>(() =>
      {
         throw new IOException("message");
      }, "message"));
   }

   [Test]
   public static void ThrowsNotSupportedException_ActionDoesNotThrow_ThrowsAssertionException()
   {
      Assert2.Throws<AssertionException>(() => Assert2.ThrowsNotSupportedException(() => { }),
          "Action did not throw. Exact expected exception: System.NotSupportedException");
   }

   [Test]
   public static void ThrowsNotSupportedException_ActionThrowsExceptionThatIsNotNotSupportedException_ThrowsAssertionException()
   {
      Assert2.Throws<AssertionException>(() => Assert2.ThrowsNotSupportedException(
          () => throw new Exception("message")),
      "Action threw System.Exception. Exact expected exception: System.NotSupportedException");
   }

   [Test]
   public static void ThrowsNotSupportedException_ActionThrowsNotSupportedExceptionWithCustomMessage_ThrowsAssertionException()
   {
      Assert2.Throws<AssertionException>(() => Assert2.ThrowsNotSupportedException(() =>
          throw new NotSupportedException("message")),
@"Action threw exactly System.NotSupportedException as expected but with an unexpected Message.
Expected message: ""Specified method is not supported.""
  Actual message: ""message""");
   }

   [Test]
   public static void ThrowsNotSupportedException_ActionThrowsNotSupportedExceptionWithoutACustomMessage_DoesNotThrowException()
   {
      Assert2.ThrowsNotSupportedException(() => throw new NotSupportedException());
   }

   [Test]
   public static void MakeExpectedButWas_ReturnsExpected()
   {
      string fieldValue = TestRandom.String();
      //
      string expectedButWasString = Assert2.MakeExpectedButWas(fieldValue);
      //
      Assert.AreEqual(expectedButWasString, $@"  Expected: ""{fieldValue}""
  But was:  null
");
   }

   [Test]
   public static void PrivateFieldSameAs_FieldDoesNotExistOnObject_ThrowsArgumentException()
   {
      object expectedObject = new object();
      object obj = new object();
      Assert2.Throws<ArgumentException>(() =>
          Assert2.PrivateFieldSameAs(expectedObject, obj, "non_existent_field"),
          "Field not found: System.Object.non_existent_field");
   }

   class PrivateFieldSameTester
   {
      public object x;

      public PrivateFieldSameTester(object x)
      {
         this.x = x;
      }
   }

   [Test]
   public static void PrivateFieldSameAs_FieldDoesExistsOnObject_NotSameAsExpectedObject_ThrowsAssertionException()
   {
      object expectedObject = new object();
      object differentObject = new object();
      var obj = new PrivateFieldSameTester(differentObject);
      Assert2.Throws<AssertionException>(() =>
          Assert2.PrivateFieldSameAs(expectedObject, obj, "x"),
          $"Field \"x\" found but with a value not the same as the expected object.");

      UnusedVariableWarning.Suppress(ref obj.x);
   }

   [Test]
   public static void PrivateFieldSameAs_FieldDoesExistsOnObject_SameAsExpectedObject_DoesNotThrowException()
   {
      object expectedObject = new object();
      var obj = new PrivateFieldSameTester(expectedObject);
      Assert2.PrivateFieldSameAs(expectedObject, obj, "x");
   }

   enum TestingEnum
   {
      Unset,
      NonDefaultValue
   }

   class EqualsTester
   {
      public int intFieldAssertedEqual;
      public int intFieldNotAssertedEqual;
      public bool boolFieldAssertedEqual;
      public bool boolFieldNotAssertedEqual;
      public string jenkinsServerUrlAssertedEqual;
      public string jenkinsServerUrlNotAssertedEqual;
      public TestingEnum enumFieldAssertedEqual;
      public TestingEnum enumFieldNotAssertedEqual;

      public override bool Equals(object obj)
      {
         EqualsTester actual = (EqualsTester)obj;
         Assert.AreEqual(intFieldAssertedEqual, actual.intFieldAssertedEqual);
         Assert.AreEqual(boolFieldAssertedEqual, actual.boolFieldAssertedEqual);
         Assert.AreEqual(jenkinsServerUrlAssertedEqual, actual.jenkinsServerUrlAssertedEqual);
         Assert.AreEqual(enumFieldAssertedEqual, actual.enumFieldAssertedEqual);
         return true;
      }

      public override int GetHashCode()
      {
         throw new NotSupportedException();
      }
   }

   [Test]
   public static void GetHashCode_ThrowsNotSupportedException()
   {
      Assert2.ThrowsNotSupportedException(() => new EqualsTester().GetHashCode());
   }

   [Test]
   public static void EqualsThrowsWhenFieldNotEqual_EqualsDoesNotThrowWhenASpecificFieldIsNotEqual_ThrowsAssertionException()
   {
      var expected = new EqualsTester();
      var actual = new EqualsTester();
      Assert2.Throws<AssertionException>(() => Assert2.EqualsThrowsWhenFieldNotEqual(expected, actual,
          ref expected.jenkinsServerUrlNotAssertedEqual, ref actual.jenkinsServerUrlNotAssertedEqual, TestRandom.String(), TestRandom.String()),
          "Action did not throw. Exact expected exception: NUnit.Framework.AssertionException");
   }

   [Test]
   public static void EqualsThrowsWhenFieldNotEqual_EqualsThrowsWithExpectedExceptionMessage_DoesNotThrowException()
   {
      var expected = new EqualsTester();
      var actual = new EqualsTester();
      Assert2.EqualsThrowsWhenFieldNotEqual(expected, actual,
          ref expected.intFieldAssertedEqual, ref actual.intFieldAssertedEqual, 1,
          @"  Assert.That(actual, Is.EqualTo(expected))
  Expected: 1
  But was:  0
");
      Assert2.EqualsThrowsWhenFieldNotEqual(expected, actual,
          ref expected.jenkinsServerUrlAssertedEqual, ref actual.jenkinsServerUrlAssertedEqual, "Hello",
          @"  Assert.That(actual, Is.EqualTo(expected))
  Expected: ""Hello""
  But was:  null
");
   }

   [Test]
   public static void EqualsThrowsWhenStringFieldNotEqual_EqualsDoesNotThrowWhenStringFieldIsNotEqual_ThrowsAssertionException()
   {
      var expected = new EqualsTester();
      var actual = new EqualsTester();
      Assert2.Throws<AssertionException>(() => Assert2.EqualsThrowsWhenStringFieldNotEqual(expected, actual,
          ref expected.jenkinsServerUrlNotAssertedEqual, ref actual.jenkinsServerUrlNotAssertedEqual),
          "Action did not throw. Exact expected exception: NUnit.Framework.AssertionException");
   }

   [Test]
   public static void EqualsThrowsWhenStringFieldNotEqual_EqualsThrowsWhenStringFieldIsNotEqual_DoesNotThrowException()
   {
      var expected = new EqualsTester();
      var actual = new EqualsTester();
      Assert2.EqualsThrowsWhenStringFieldNotEqual(expected, actual, ref expected.jenkinsServerUrlAssertedEqual, ref actual.jenkinsServerUrlAssertedEqual);
   }

   [Test]
   public static void EqualsThrowsWhenBoolFieldNotEqual_EqualsDoesNotThrowWhenBoolFieldIsNotEqual_ThrowsAssertionException()
   {
      var expected = new EqualsTester();
      var actual = new EqualsTester();
      Assert2.Throws<AssertionException>(() => Assert2.EqualsThrowsWhenBoolFieldNotEqual(expected, actual,
          ref expected.boolFieldNotAssertedEqual, ref actual.boolFieldNotAssertedEqual),
          "Action did not throw. Exact expected exception: NUnit.Framework.AssertionException");
   }

   [Test]
   public static void EqualsThrowsWhenBoolFieldNotEqual_EqualsThrowsWhenBoolFieldIsNotEqual_DoesNotThrowException()
   {
      var expected = new EqualsTester();
      var actual = new EqualsTester();
      Assert2.EqualsThrowsWhenBoolFieldNotEqual(expected, actual, ref expected.boolFieldAssertedEqual, ref actual.boolFieldAssertedEqual);
   }

   [Test]
   public static void EqualsThrowsWhenIntFieldNotEqual_EqualsDoesNotThrowWhenIntFieldIsNotEqual_ThrowsAssertionException()
   {
      var expected = new EqualsTester();
      var actual = new EqualsTester();
      Assert2.Throws<AssertionException>(() => Assert2.EqualsThrowsWhenIntFieldNotEqual(expected, actual,
          ref expected.intFieldNotAssertedEqual, ref actual.intFieldNotAssertedEqual),
          "Action did not throw. Exact expected exception: NUnit.Framework.AssertionException");
   }

   [Test]
   public static void EqualsThrowsWhenIntFieldNotEqual_EqualsThrowsWhenIntFieldIsNotEqual_DoesNotThrowException()
   {
      var expected = new EqualsTester();
      var actual = new EqualsTester();
      Assert2.EqualsThrowsWhenIntFieldNotEqual(expected, actual, ref expected.intFieldAssertedEqual, ref actual.intFieldAssertedEqual);
   }

   [Test]
   public static void EqualsThrowsWhenEnumFieldNotEqual_EqualsDoesNotThrowWhenEnumFieldIsNotEqual_ThrowsAssertionException()
   {
      var expected = new EqualsTester();
      var actual = new EqualsTester();
      Assert2.Throws<AssertionException>(() => Assert2.EqualsThrowsWhenEnumFieldNotEqual(expected, actual,
          ref expected.enumFieldNotAssertedEqual, ref actual.enumFieldNotAssertedEqual,
          TestingEnum.Unset, TestingEnum.NonDefaultValue),
          "Action did not throw. Exact expected exception: NUnit.Framework.AssertionException");
   }

   [Test]
   public static void EqualsThrowsWhenEnumFieldNotEqual_EqualsThrowsWhenEnumFieldIsNotEqual_DoesNotThrowException()
   {
      var expected = new EqualsTester();
      var actual = new EqualsTester();
      Assert2.EqualsThrowsWhenEnumFieldNotEqual(expected, actual,
          ref expected.enumFieldAssertedEqual, ref actual.enumFieldAssertedEqual, TestingEnum.Unset, TestingEnum.NonDefaultValue);
   }
}
