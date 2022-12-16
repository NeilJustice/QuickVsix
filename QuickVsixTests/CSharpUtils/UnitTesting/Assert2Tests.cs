using CSharpUtils;
using NUnit.Framework;
using System.Diagnostics.CodeAnalysis;

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
   public static void IsNotNullOrEmpty_StringIsNull_ThrowsAssertionException()
   {
      Assert2.Throws<AssertionException>(() => Assert2.IsNotNullOrEmpty(null),
@"  Expected: not null
  But was:  null
");
   }

   [Test]
   public static void IsNotNullOrEmpty_StringIsEmpty_ThrowsAssertionException()
   {
      Assert2.Throws<AssertionException>(() => Assert2.IsNotNullOrEmpty(""),
@"  Expected: not <empty>
  But was:  <string.Empty>
");
   }

   [Test]
   public static void IsNotNullOrEmpty_StringIsNotEmpty_DoesNotThrowException()
   {
      Assert.DoesNotThrow(() => Assert2.IsNotNullOrEmpty(TestRandom.String()));
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
@"  Expected: <Assert2Tests+BaseClass>
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
   [ExcludeFromCodeCoverage]
   public static void Throws_ActionDoesNotThrow_Throws__ExceptionTestCase()
   {
      try
      {
         Assert2.Throws<System.Exception>(() => { }, "");
         Assert.Fail("Did not throw as expected"); // Non-coverable line
      }
      catch (AssertionException e)
      {
         Assert.AreEqual("Action did not throw. Exact expected exception: System.Exception", e.Message);
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
      catch (AssertionException e)
      {
         Assert.AreEqual("Action did not throw. Exact expected exception: System.IO.IOException", e.Message);
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
      catch (AssertionException e)
      {
         Assert.AreEqual("Action threw System.IO.IOException. Exact expected exception: System.ArgumentException", e.Message);
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
      catch (AssertionException e)
      {
         Assert.AreEqual("Action threw FakeItEasy.Configuration.FakeConfigurationException. Exact expected exception: FakeItEasy.Core.FakeCreationException", e.Message);
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
      catch (AssertionException e)
      {
         Assert.AreEqual("Action threw System.InvalidOperationException. Exact expected exception: System.Exception", e.Message);
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
      catch (AssertionException e)
      {
         Assert.AreEqual("Action threw System.IO.InvalidDataException. Exact expected exception: System.SystemException", e.Message);
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
      catch (AssertionException e)
      {
         string expectedAssertionMessage = @"Action threw exactly System.Exception as expected but with an unexpected Message.
Expected message: ""abc""
  Actual message: ""def""";
         Assert.AreEqual(expectedAssertionMessage, e.Message);
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
      catch (AssertionException e)
      {
         string expectedAssertionMessage = @"Action threw exactly System.IO.IOException as expected but with an unexpected Message.
Expected message: ""message""
  Actual message: ""Message""";
         Assert.AreEqual(expectedAssertionMessage, e.Message);
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
      catch (AssertionException e)
      {
         string expectedAssertionMessage = @"Action threw exactly System.Text.RegularExpressions.RegexMatchTimeoutException as expected but with an unexpected Message.
Expected message: ""abc message""
  Actual message: ""message""";
         Assert.AreEqual(expectedAssertionMessage, e.Message);
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
   public static void PrivateFieldSame_FieldDoesNotExistOnObject_ThrowsArgumentException()
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
}
